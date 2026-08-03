namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Entity_Stats : MonoBehaviour
    {
        public Stat maxHealth;
        public Stat_MajorGroup major;
        public Stat_OffenseGroup offense;
        public Stat_DefenseGroup defense;

        private const float HEALTH_PER_VITALITY = 5f;
        private const float VASION_PER_EVASION = .5f;
        private const float CRIT_CHANCE_PER_AGILITY = .3f;
        private const float CRIT_DAMAGE_PER_STRENGTH = .5f;

        public float GetPhysicalDamage()
        {
            var baseDamage = offense.damage.GetValue();
            var bonusDamage = major.strength.GetValue();
            var totalBaseDamage = baseDamage + bonusDamage;

            var baseCritChance = offense.critChance.GetValue();
            var bonusCritChance = major.agility.GetValue() * CRIT_CHANCE_PER_AGILITY;
            var critChance = baseCritChance + bonusCritChance;

            var baseCritPower = offense.critPower.GetValue();
            var bonusCritPower = major.strength.GetValue() * CRIT_DAMAGE_PER_STRENGTH;
            var critPower = (baseCritPower + bonusCritPower) / 100;

            var isCrit = Random.Range(0, 100) < critChance;
            var finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

            return finalDamage;
        }

        public float GetMaxHealth()
        {
            var baseHp = maxHealth.GetValue();
            var bonusHp = major.vitality.GetValue() * HEALTH_PER_VITALITY;

            return baseHp + bonusHp;
        }

        public float GetEvasion()
        {
            var baseVasion = defense.evasion.GetValue();
            var bonusEvasion = major.agility.GetValue() * VASION_PER_EVASION;

            var totalEvasion = baseVasion + bonusEvasion;
            var evasionCap = 85f;

            var finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

            return finalEvasion;
        }
    }
}