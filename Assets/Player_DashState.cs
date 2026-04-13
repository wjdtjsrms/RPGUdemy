namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Player_DashState : EntityState
    {
        private float originalGravityScale = 0f;
        private int dashDir;

        public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = player.dashDuration;
            dashDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir; ;

            originalGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;
        }

        public override void Update()
        {
            base.Update();

            CancelDashIfNeeded();

            player.SetVelocity(player.dashSpeed * dashDir, 0f);

            if (stateTimer < 0f)
            {
                if (player.groundDetected)
                    stateMachine.ChangeState(player.idleState);
                else
                    stateMachine.ChangeState(player.fallState);
            }
        }

        public override void Exit()
        {
            base.Exit();

            player.SetVelocity(0f, 0f);
            rb.gravityScale = originalGravityScale;
        }

        private void CancelDashIfNeeded()
        {
            if (player.wallDetected)
            {
                if (player.groundDetected)
                    stateMachine.ChangeState(player.idleState);
                else
                    stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }
}