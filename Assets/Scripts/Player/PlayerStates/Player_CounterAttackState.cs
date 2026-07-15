namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Player_CounterAttackState : PlayerState
    {
        private readonly Player_Combat combat;
        private bool counterdSomebody;

        public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
            combat = player.GetComponent<Player_Combat>();
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = combat.GetCounterRecoveryDuration();
            counterdSomebody = combat.CounterAttackPerformed();

            anim.SetBool("counterAttackPerformed", counterdSomebody);
        }

        public override void Update()
        {
            base.Update();

            player.SetVelocity(0, rb.linearVelocity.y);

            // Finish Counter Attack Animation
            if (triggerCalled)
                stateMachine.ChangeState(player.idleState);

            if (stateTimer < 0f && counterdSomebody == false)
                stateMachine.ChangeState(player.idleState);
        }

    }
}