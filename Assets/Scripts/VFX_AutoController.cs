namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class VFX_AutoController : MonoBehaviour
    {
        [SerializeField] private bool autoDestroy = true;
        [SerializeField] private float destroyDelay = 1f;
        [Space]
        [SerializeField] private bool randomOffset = true;
        [SerializeField] private bool randomRotation = true;

        [Header("Random Position")]
        [SerializeField] private float xMinOffset = -.3f;
        [SerializeField] private float xMaxOffset = .3f;
        [Space]
        [SerializeField] private float yMinOffset = -.3f;
        [SerializeField] private float yMaxOffset = .3f;

        private void Start()
        {
            ApplyRandomOffset();
            ApplyRadomRotation();

            if (autoDestroy)
                Destroy(gameObject, destroyDelay);
        }

        private void ApplyRandomOffset()
        {
            if (randomOffset == false)
                return;

            var xOffset = Random.Range(xMinOffset, xMaxOffset);
            var yOffset = Random.Range(yMinOffset, yMaxOffset);

            transform.position = transform.position + new Vector3(xOffset, yOffset);
        }

        private void ApplyRadomRotation()
        {
            if (randomRotation == false)
                return;

            var zRotation = Random.Range(0, 360);
            transform.Rotate(0, 0, zRotation);
        }
    }
}