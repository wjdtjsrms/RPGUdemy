namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Player_CounterAttackState : PlayerState
    {
        public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

    }
}