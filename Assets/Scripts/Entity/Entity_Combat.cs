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

        [Header("Status Effect Details")]
        [SerializeField] private float defaultDuration = 3f;
        [SerializeField] private float chillSlowMultiplier = .2f;
        [SerializeField] private float electrifyChargeBuildUp = .4f;
        [Space]
        [SerializeField] private float fireScale = .8f;
        [SerializeField] private float lightningScale = 2.5f;


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

                var elementalDamage = stats.GetElementalDamage(out var element, .6f);
                var damage = stats.GetPhysicalDamage(out var isCrit);
                var targetGotHit = damegble.TakeDamage(damage, elementalDamage, element, damageDealer: transform);

                if (element != ElementType.None)
                    ApplyStatusEffect(target.transform, element);

                if (targetGotHit)
                {
                    // vfx.UpdateOnHitColor(element);
                    vfx.CreateOnHitVFX(target.transform, isCrit);
                }
            }
        }

        public void ApplyStatusEffect(Transform target, ElementType element, float scaleFactor = 1f)
        {
            var statusHandler = target.GetComponent<Entity_StatusHandler>();

            if (statusHandler == null)
                return;

            if (element == ElementType.Ice && statusHandler.CanBeApplied(ElementType.Ice))
                statusHandler.ApplyChillEffect(defaultDuration, chillSlowMultiplier);
            else if (element == ElementType.Fire && statusHandler.CanBeApplied(ElementType.Fire))
            {
                scaleFactor = fireScale;
                var fireDamage = stats.offense.fireDamage.GetValue() * scaleFactor;
                statusHandler.ApplyVBurnEffect(defaultDuration, fireDamage);
            }
            else if (element == ElementType.Lightning && statusHandler.CanBeApplied(ElementType.Lightning))
            {
                scaleFactor = lightningScale;
                var lightningDamage = stats.offense.lightningDamage.GetValue() * scaleFactor;
                statusHandler.ApplyElectrifyEffect(defaultDuration, lightningDamage, electrifyChargeBuildUp);
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