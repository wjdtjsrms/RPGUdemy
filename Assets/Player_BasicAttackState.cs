namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Player_BasicAttackState : EntityState
    {
        private float attackVelocityDuration;

        public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

        public override void Enter()
        {
            base.Enter();
            GenerateAttackVelocity();
        }

        public override void Update()
        {
            base.Update();
            HandleAttackVelocity();

            if (triggerCalled)
                stateMachine.ChangeState(player.idleState);
        }

        private void HandleAttackVelocity()
        {
            attackVelocityDuration -= Time.deltaTime;

            if (attackVelocityDuration < 0f)
                player.SetVelocity(0, rb.linearVelocity.y);
        }

        private void GenerateAttackVelocity()
        {
            attackVelocityDuration = player.attackVelocityDuration;
            player.SetVelocity(player.attackVelocity.x * player.facingDir, player.attackVelocity.y);
        }
    }
}