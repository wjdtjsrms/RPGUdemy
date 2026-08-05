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

        public float GetPhysicalDamage(out bool isCrit)
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

            isCrit = Random.Range(0, 100) < critChance;
            var finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

            return finalDamage;
        }

        public float GetArmorMitigation(float armorReduction)
        {
            var baseArmor = defense.armor.GetValue();
            var bonusArmor = major.vitality.GetValue();
            var totalArmor = baseArmor + bonusArmor;

            var reductionMultiplier = Mathf.Clamp(1 - armorReduction, 0, 1);
            var effectiveArmor = totalArmor * reductionMultiplier;

            var mitigation = totalArmor / (effectiveArmor + 100);
            var mitigationGap = .85f;

            float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationGap);

            return finalMitigation;
        }

        public float GetArmorReduction()
        {
            var finalReduction = offense.armorReduction.GetValue() / 100f;

            return finalReduction;
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

        public float GetMaxHealth()
        {
            var baseMaxHealth = maxHealth.GetValue();
            var bonusHp = major.vitality.GetValue() * HEALTH_PER_VITALITY;

            var finalMaxHealth = baseMaxHealth + bonusHp;
            return finalMaxHealth;
        }
    }
}