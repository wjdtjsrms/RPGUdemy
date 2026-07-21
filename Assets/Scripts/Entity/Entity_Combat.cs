namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Entity_Combat : MonoBehaviour
    {
        private Entity_VFX vfx;
        public float damage = 10f;

        [Header("Target Detection")]
        [SerializeField] private Transform targetCheck;
        [SerializeField] private float targetCheckRadius = 1f;
        [SerializeField] private LayerMask wahtIsTarget;

        private void Awake()
        {
            vfx = GetComponent<Entity_VFX>();
        }

        public void PerformAttack()
        {
            foreach (var target in GetDetectionColliders())
            {
                var damgable = target.GetComponent<IDamgable>();

                if (damgable == null)
                    continue;

                damgable.TakeDamage(damage, damageDealer: transform);
                vfx.CreateOnHitVFX(target.transform);
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