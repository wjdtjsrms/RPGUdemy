namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class EntityState
    {
        protected readonly StateMachine stateMachine;
        protected readonly string stateName;

        public EntityState(StateMachine stateMachine, string stateName)
        {
            this.stateMachine = stateMachine;
            this.stateName = stateName;
        }

        public virtual void Enter()
        {
            Debug.Log($"I Enter {stateName}");
        }

        public virtual void Update()
        {
            Debug.Log($"I run update of {stateName}");
        }

        public virtual void Exit()
        {
            Debug.Log($"I exit {stateName}");
        }
    }
}