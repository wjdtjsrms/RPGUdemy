namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Entity_Combat : MonoBehaviour
    {
        public float damage = 10f;

        [Header("Target Detection")]
        [SerializeField] private Transform targetCheck;
        [SerializeField] private float targetCheckRadius = 1f;
        [SerializeField] private LayerMask wahtIsTarget;

        public void PerformAttack()
        {
            foreach (var target in GetDetectionColliders())
            {
                var targetHealth = target.GetComponent<Entity_Health>();
                targetHealth?.TakeDamage(damage, damageDealer: transform);
            }
        }

        private Collider2D[] GetDetectionColliders()
        {
            return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, wahtIsTarget);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
        }
    }

}