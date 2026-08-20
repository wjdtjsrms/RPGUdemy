namespace SSunSoft.RPGUdemy
{
    using NUnit.Framework;
    using UnityEngine;
    using UnityEngine.UI;

    public class Entity_Health : MonoBehaviour, IDamgable
    {
        private Slider healthBar;
        private Entity entity;
        private Entity_VFX entityVfx;
        private Entity_Stats entityStats;

        [SerializeField] protected float currentHealth;
        [SerializeField] protected bool isDead;

        [Header("Health regen")]
        [SerializeField] private float regenInterval = 1f;
        [SerializeField] private bool canRegenerateHealth = true;

        [Header("On Damage Knockback")]
        [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
        [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);
        [SerializeField] private float knockbackDuration = .2f;
        [SerializeField] private float heavyKnockbackDuration = .5f;
        [Header("On Heavy Damage")]
        [SerializeField] private float heavyDamageThreshold = .3f;

        private void Awake()
        {
            entity = GetComponent<Entity>();
            entityVfx = GetComponent<Entity_VFX>();
            entityStats = GetComponent<Entity_Stats>();

            healthBar = GetComponentInChildren<Slider>();

            currentHealth = entityStats.GetMaxHealth();
            UpdateHealthBar();

            InvokeRepeating(nameof(RegenerateHealth), 0, regenInterval);
        }

        public virtual bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
        {
            if (isDead) return false;

            if (AttackEvaded())
            {
                Debug.Log($"{gameObject.name} evaded the attack!");
                return false;
            }

            var attackerStats = damageDealer.GetComponent<Entity_Stats>();
            float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0f;

            var mitigation = entityStats.GetArmorMitigation(armorReduction);
            var physicalDamageTaken = damage * (1 - mitigation);

            var resistance = entityStats.GetElementalResistance(element);
            var elmentalDamageTaken = elementalDamage * (1 - resistance);

            TakeKnockBack(damageDealer, physicalDamageTaken);
            ReduceHealth(physicalDamageTaken + elmentalDamageTaken);

            return true;
        }

        private bool AttackEvaded() => Random.Range(0, 100) < entityStats.GetEvasion();

        private void RegenerateHealth()
        {
            if (canRegenerateHealth == false)
                return;

            var regenAmount = entityStats.resource.healthRegen.GetValue();
            IncreaseHealth(regenAmount);
        }

        public void IncreaseHealth(float healAmount)
        {
            if (isDead)
                return;

            var newHealth = currentHealth + healAmount;
            var maxHealth = entityStats.GetMaxHealth();

            currentHealth = Mathf.Min(newHealth, maxHealth);
            UpdateHealthBar();
        }

        public void ReduceHealth(float damage)
        {
            entityVfx?.PlayOnDamageVfx();
            currentHealth -= damage;
            UpdateHealthBar();

            if (currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            isDead = true;
            entity.EntityDeath();
        }

        private void UpdateHealthBar()
        {
            if (healthBar == null)
                return;
            healthBar.value = currentHealth / entityStats.GetMaxHealth();
        }

        private void TakeKnockBack(Transform damageDealer, float finalDamage)
        {
            var knockback = CalculateKnockback(finalDamage, damageDealer);
            var duration = CalculateDuration(finalDamage);

            entity?.ReceiveKnockback(knockback, duration);
        }

        private Vector2 CalculateKnockback(float damage, Transform damageDealer)
        {
            int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
            Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;

            knockback.x = knockback.x * direction;

            return knockback;
        }

        private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

        private bool IsHeavyDamage(float damage) => (damage / entityStats.GetMaxHealth()) > heavyDamageThreshold;
    }
}