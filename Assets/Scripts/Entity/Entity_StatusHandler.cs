namespace SSunSoft.RPGUdemy
{
    using System.Collections;
    using Mono.Cecil.Cil;
    using UnityEngine;

    public class Entity_StatusHandler : MonoBehaviour
    {
        private Entity entity;
        private Entity_VFX entityVFX;
        private Entity_Stats entityStats;
        private Entity_Health entityHealth;
        private ElementType currentEffect = ElementType.None;

        private void Awake()
        {
            entity = GetComponent<Entity>();
            entityVFX = GetComponent<Entity_VFX>();
            entityStats = GetComponent<Entity_Stats>();
            entityHealth = GetComponent<Entity_Health>();
        }

        public void ApplyVBurnEffect(float duration, float fireDamage)
        {
            var fireResistance = entityStats.GetElementalResistance(ElementType.Fire);
            var finalDamage = fireDamage * (1 - fireResistance);

            StartCoroutine(BurnEffectCo(duration, finalDamage));
        }

        private IEnumerator BurnEffectCo(float duration, float totalDamage)
        {
            currentEffect = ElementType.Fire;
            entityVFX.PlayOnStatusVfx(duration, ElementType.Fire);

            var ticksPerSecond = 2;
            var tickCount = Mathf.RoundToInt(ticksPerSecond * duration);

            var damagePerTick = totalDamage / tickCount;
            var tickInterval = 1f / ticksPerSecond;

            for (int i = 0; i < tickCount; i++)
            {
                entityHealth.ReduceHp(damagePerTick);
                yield return new WaitForSeconds(tickInterval);
            }

            currentEffect = ElementType.None;
        }

        public void ApplyChilledEffect(float duration, float slowMultiplier)
        {
            var iceResistance = entityStats.GetElementalResistance(ElementType.Ice);
            var finalDuration = duration * (1 - iceResistance);

            StartCoroutine(ChilledEffectCo(finalDuration, slowMultiplier));
        }

        private IEnumerator ChilledEffectCo(float duration, float slowMultiplier)
        {
            entity.SlowDownEntity(duration, slowMultiplier);
            currentEffect = ElementType.Ice;
            entityVFX.PlayOnStatusVfx(duration, ElementType.Ice);

            yield return new WaitForSeconds(duration);

            currentEffect = ElementType.None;
        }

        public bool CanBeApplied(ElementType element)
        {
            return currentEffect == ElementType.None;
        }
    }
}