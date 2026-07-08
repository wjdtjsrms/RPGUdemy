namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Enemy_DeadState : EnemyState
    {
        private readonly Collider2D col;

        public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
        {
            col = enemy.GetComponent<Collider2D>();
        }

        public override void Enter()
        {
            anim.enabled = false;
            col.enabled = false;

            rb.gravityScale = 12f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15f);

            stateMachine.SwitchOffStateMachine();
        }
    }
}