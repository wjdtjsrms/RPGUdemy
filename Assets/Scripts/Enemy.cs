using UnityEngine;

namespace SSunSoft.RPGUdemy
{
    public class Enemy : Entity
    {
        public Enemy_IdleState idleState;
        public Enemy_MoveState moveState;
        public Enemy_AttackState attackState;
        public Enemy_BattleState battleState;

        [Header("Movement Details")]
        public float idleTime = 2f;
        public float moveSpeed = 1.4f;
        [Range(0.1f, 2f)]
        public float moveAnimSpeedMultiplier = 1f;

        [Header("Player Detection")]
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Transform playerCheck;
        [SerializeField] public float playerCheckDistance = 10f;

        public RaycastHit2D PlayerDetection()
        {
            var hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);

            if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
                return default;

            return hit;
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));


        }
    }
}