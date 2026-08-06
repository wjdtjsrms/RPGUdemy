namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Entity_Combat : MonoBehaviour
    {
        private Entity_VFX vfx;
        private Entity_Stats stats;

        [Header("Target Detection")]
        [SerializeField] private Transform targetCheck;
        [SerializeField] private float targetCheckRadius = 1f;
        [SerializeField] private LayerMask wahtIsTarget;

        private void Awake()
        {
            vfx = GetComponent<Entity_VFX>();
            stats = GetComponent<Entity_Stats>();
        }

        public void PerformAttack()
        {
            foreach (var target in GetDetectionColliders())
            {
                var damegble = target.GetComponent<IDamgable>();

                if (damegble == null)
                    continue;

                var damage = stats.GetPhysicalDamage(out var isCrit);
                var elementalDamage = stats.GetElementalDamage(out var element);
                var targetGotHit = damegble.TakeDamage(damage, elementalDamage, element, damageDealer: transform);

                if (targetGotHit)
                {
                    vfx.CreateOnHitVFX(target.transform, isCrit);
                }
            }
        }

        protected Collider2D[] GetDetectionColliders()
        {
            return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, wahtIsTarget);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
        }
    }

}