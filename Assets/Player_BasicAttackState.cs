namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Player_BasicAttackState : EntityState
    {
        private float attackVelocityDuration;

        private const int FirstComboIndex = 1;
        private int combonIndex = 1;
        private int comboLimit = 3;

        private float lastTimeAttacked;
        private bool comboAttackQueued;
        private int attackDir;

        public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
            if (comboLimit != player.attackVelocity.Length)
            {
                Debug.LogWarning("I've adjusted combo Limit");
                comboLimit = player.attackVelocity.Length;
            }
        }

        public override void Enter()
        {
            base.Enter();

            comboAttackQueued = false;
            ResetComboIndexIfNeeded();

            attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;

            ApplyAttackVelocity();
            anim.SetInteger("basicAttackIndex", combonIndex);
        }

        public override void Update()
        {
            base.Update();
            HandleAttackVelocity();

            if (input.Player.Attack.WasPressedThisFrame())
                QueueNextAttack();

            if (triggerCalled)
                HandleStateExit();
        }

        public override void Exit()
        {
            base.Exit();
            combonIndex++;
            lastTimeAttacked = Time.time;
        }

        private void HandleStateExit()
        {
            if (comboAttackQueued)
            {
                anim.SetBool(animBoolName, false);
                player.EnterAttackStateWithDelay();
            }
            else
                stateMachine.ChangeState(player.idleState);
        }

        private void QueueNextAttack()
        {
            if (combonIndex < comboLimit)
                comboAttackQueued = true;
        }

        private void ResetComboIndexIfNeeded()
        {
            if (Time.time > lastTimeAttacked + player.comboResetTime)
                combonIndex = FirstComboIndex;

            if (combonIndex > comboLimit)
                combonIndex = FirstComboIndex;
        }

        private void HandleAttackVelocity()
        {
            attackVelocityDuration -= Time.deltaTime;

            if (attackVelocityDuration < 0f)
                player.SetVelocity(0, rb.linearVelocity.y);
        }

        private void ApplyAttackVelocity()
        {
            Vector2 attackVelocity = player.attackVelocity[combonIndex - 1];

            attackVelocityDuration = player.attackVelocityDuration;
            player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
        }
    }
}