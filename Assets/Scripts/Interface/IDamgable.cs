namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public interface IDamgable
    {
        public bool TakeDamage(float damage, Transform damageDealer);
    }
}