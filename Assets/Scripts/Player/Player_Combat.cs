namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Player_Combat : Entity_Combat
    {
        [Header("Counter Attack Details")]
        [SerializeField] private float counterRecovery = .1f;

        public bool CounterAttackPerformed()
        {
            bool hasPerofrmedCounter = false;

            foreach (var target in GetDetectionColliders())
            {
                ICounterable counterable = target.GetComponent<ICounterable>();

                if (counterable == null)
                    continue;

                if (counterable.CanBeCountered)
                {
                    counterable.HandleCounter();
                    hasPerofrmedCounter = true;
                }
            }

            return hasPerofrmedCounter;
        }

        public float GetCounterRecoveryDuration() => counterRecovery;
    }
}