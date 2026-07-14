namespace SSunSoft.RPGUdemy
{
    public class StateMachine
    {
        public EntityState currentState { get; private set; }
        private bool canChanageState;

        public void Initialize(EntityState startState)
        {
            canChanageState = true;
            currentState = startState;
            currentState.Enter();
        }

        public void ChangeState(EntityState newState)
        {
            if (canChanageState == false)
                return;

            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void UpdateActiveState()
        {
            currentState.Update();
        }

        public void SwitchOffStateMachine() => canChanageState = false;
    }
}