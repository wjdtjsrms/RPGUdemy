namespace SSunSoft
{
    using SSunSoft.RPGUdemy;
    using UnityEngine;

    public class Chest : MonoBehaviour, IDamgable
    {
        private Rigidbody2D rb;
        private Animator animator;
        private Entity_VFX fx;

        [Header("Open Details")]
        [SerializeField] private Vector2 knockback;

        private void Awake()
        {
            rb = GetComponentInChildren<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            fx = GetComponent<Entity_VFX>();
        }

        public void TakeDamage(float damage, Transform damageDealer)
        {
            fx.PlayOnDamageVfx();
            animator.SetBool("chestOpen", true);
            rb.linearVelocity = knockback;
            rb.angularVelocity = Random.Range(-200f, 200f);
        }
    }
}