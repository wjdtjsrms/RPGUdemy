namespace SSunSoft.RPGUdemy
{
    using System.Collections;
    using UnityEngine;

    public class Entity_StatusHandler : MonoBehaviour
    {
        private Entity entity;
        private Entity_VFX entity_VFX;
        private Entity_Stats stats;
        private ElementType currentEffect = ElementType.None;

        private void Awake()
        {
            entity = GetComponent<Entity>();
            entity_VFX = GetComponent<Entity_VFX>();
            stats = GetComponent<Entity_Stats>();
        }

        public void ApplyChilledEffect(float duration, float slowMultiplier)
        {
            var iceResistance = stats.GetElementalResistance(ElementType.Ice);
            var reduceDuration = duration * (1 - iceResistance);

            StartCoroutine(ChilledEffectCo(reduceDuration, slowMultiplier));
        }

        private IEnumerator ChilledEffectCo(float duration, float slowMultiplier)
        {
            entity.SlowDownEntity(duration, slowMultiplier);
            currentEffect = ElementType.Ice;
            entity_VFX.PlayOnStatusVfx(duration, ElementType.Ice);

            yield return new WaitForSeconds(duration);

            currentEffect = ElementType.None;
        }

        public bool CanBeApplied(ElementType element)
        {
            return currentEffect == ElementType.None;
        }
    }
}