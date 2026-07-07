namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Entity_Health : MonoBehaviour
    {
        [SerializeField] protected float maxHP = 100f;
        [SerializeField] protected bool isDead;

        public virtual void TakeDamage(float damage, Transform damageDealer)
        {
            if (isDead) return;

            ReduceHp(damage);
        }

        protected void ReduceHp(float damage)
        {
            maxHP -= damage;

            if (maxHP < 0)
                Die();
        }

        private void Die()
        {
            isDead = true;
            Debug.Log($"{gameObject.name} Die");
        }
    }
}