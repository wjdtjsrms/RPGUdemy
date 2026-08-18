namespace SSunSoft.RPGUdemy
{
    using System.Collections;
    using UnityEngine;

    public class Entity_VFX : MonoBehaviour
    {
        private SpriteRenderer sr;
        private Entity entity;

        [Header("On Taking Damage VFX")]
        [SerializeField] private Material onDamageMaterial;
        [SerializeField] private float onDamageVfxDuration = .2f;
        private Material originalMaterial;
        private Coroutine onDamageVfxCoroutine;

        [Header("On Doing Damage VFX")]
        [SerializeField] private Color hitVfxColor = Color.white;
        [SerializeField] private GameObject hitVfx;
        [SerializeField] private GameObject criHitVfx;

        [Header("Element Colors")]
        [SerializeField] private Color chillVfx = Color.cyan;
        [SerializeField] private Color burnVfx = Color.red;
        private Color originalHitVfxColor;

        private void Awake()
        {
            entity = GetComponent<Entity>();
            sr = GetComponentInChildren<SpriteRenderer>();
            originalMaterial = sr.material;
            originalHitVfxColor = hitVfxColor;
        }

        public void PlayOnStatusVfx(float duration, ElementType element)
        {
            if (element == ElementType.Ice)
                StartCoroutine(PlayStatusVfxCo(duration, chillVfx));
            else if (element == ElementType.Fire)
                StartCoroutine(PlayStatusVfxCo(duration, burnVfx));
        }

        private IEnumerator PlayStatusVfxCo(float duration, Color effectColor)
        {
            var tickInterval = .25f;
            var timeHasPassed = 0f;

            var lightColor = effectColor * 1.2f;
            var darkColor = effectColor * .9f;

            bool toggle = false;

            while (timeHasPassed < duration)
            {
                sr.color = toggle ? lightColor : darkColor;
                toggle = !toggle;

                yield return new WaitForSeconds(tickInterval);
                timeHasPassed += tickInterval;
            }

            sr.color = Color.white;
        }

        public void CreateOnHitVFX(Transform target, bool isCrit)
        {
            var hitPrefabs = isCrit ? criHitVfx : hitVfx;
            var vfx = Instantiate(hitPrefabs, target.position, Quaternion.identity);

            if (isCrit == false)
                vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;

            if (entity.facingDir == -1 && isCrit)
                vfx.transform.Rotate(0, 180, 0);
        }

        public void UpdateOnHitColor(ElementType element)
        {
            if (element == ElementType.Ice)
                hitVfxColor = chillVfx;

            if (element == ElementType.None)
                hitVfxColor = originalHitVfxColor;
        }

        public void PlayOnDamageVfx()
        {
            if (onDamageVfxCoroutine != null)
                StopCoroutine(onDamageVfxCoroutine);

            onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCo());
        }

        private IEnumerator OnDamageVfxCo()
        {
            sr.material = onDamageMaterial;

            yield return new WaitForSeconds(onDamageVfxDuration);
            sr.material = originalMaterial;
        }
    }
}