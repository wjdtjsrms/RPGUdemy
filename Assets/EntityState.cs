namespace SSunSoft.RPGUdemy
{
    using Unity.VisualScripting;
    using UnityEngine;

    public abstract class EntityState
    {
        protected Player player;
        protected readonly StateMachine stateMachine;
        protected readonly string animBoolName;

        protected Animator anim;
        protected Rigidbody2D rb;

        public EntityState(Player player, StateMachine stateMachine, string animBoolName)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;

            anim = player.anim;
            rb= player.rb;
        }

        public virtual void Enter()
        {
            anim.SetBool(animBoolName, true);
        }

        public virtual void Update()
        {
            Debug.Log($"I run update of {animBoolName}");
        }

        public virtual void Exit()
        {
            anim.SetBool(animBoolName, false);
        }
    }
}