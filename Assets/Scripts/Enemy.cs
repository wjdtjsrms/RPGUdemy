using UnityEngine;

namespace SSunSoft.RPGUdemy
{
    public class Enemy : Entity
    {
        public Enemy_IdleState idleState;
        public Enemy_MoveState moveState;

        [Header("Movement Details")]
        public float idleTime = 2f;
        public float moveSpeed = 1.4f;
        [Range(0.1f, 2f)]
        public float moveAnimSpeedMultiplier = 1f;
    }
}