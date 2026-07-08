namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Entity_Health : MonoBehaviour
    {
        private Entity_VFX entityVfx;
        private Entity entity;

        [SerializeField] protected float currentHP;
        [SerializeField] protected float maxHP = 100f;
        [SerializeField] protected bool isDead;

        [Header("On Damage Knockback")]
        [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
        [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);
        [SerializeField] private float knockbackDuration = .2f;
        [SerializeField] private float heavyKnockbackDuration = .5f;
        [Header("On Heavy Damage")]
        [SerializeField] private float heavyDamageThreshold = .3f;

        private void Awake()
        {
            entityVfx = GetComponent<Entity_VFX>();
            entity = GetComponent<Entity>();

            currentHP = maxHP;
        }

        public virtual void TakeDamage(float damage, Transform damageDealer)
        {
            if (isDead) return;

            var knockback = CalculateKnockback(damage, damageDealer);
            var duration = CalculateDuration(damage);

            entity?.ReceiveKnockback(knockback, duration);
            entityVfx?.PlayOnDamageVfx();
            ReduceHp(damage);
        }

        protected void ReduceHp(float damage)
        {
            currentHP -= damage;

            if (currentHP <= 0)
                Die();
        }

        private void Die()
        {
            isDead = true;
            entity.EntityDeath();
        }

        private Vector2 CalculateKnockback(float damage, Transform damageDealer)
        {
            int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
            Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;

            knockback.x = knockback.x * direction;

            return knockback;
        }

        private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

        private bool IsHeavyDamage(float damage) => (damage / maxHP) > heavyDamageThreshold;
    }
}