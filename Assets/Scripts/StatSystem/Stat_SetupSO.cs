namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "RPG Setup/Default Stat Setup", fileName = "Default Stat Setup")]
    public class Stat_SetupSO : ScriptableObject
    {
        [Header("Resource")]
        public float maxHealth = 100f;
        public float healthRegen;

        [Header("Offense Stats - Physical Damage")]
        public float attackSpeed = 1f;
        public float damage = 10f;
        public float critChance;
        public float critPower = 150f;
        public float armorReduction;

        [Header("Offense - Elemental Damage")]
        public float fireDamage;
        public float iceDamage;
        public float lightningDamage;

        [Header("Defense Stats - Physical Damage")]
        public float armor;
        public float evasion;

        [Header("Defense Stats - Elemental Resistance")]
        public float iceRes;
        public float fireRes;
        public float lightningRes;

        [Header("Major Stats")]
        public float strength;
        public float agility;
        public float intelligence;
        public float vitality;
    }
}