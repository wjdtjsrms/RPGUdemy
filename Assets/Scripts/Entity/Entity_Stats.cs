namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class Entity_Stats : MonoBehaviour
    {
        public Stat_SetupSO defaultStatSetup;

        public Stat_ResourceGroup resource;
        public Stat_OffenseGroup offense;
        public Stat_DefenseGroup defense;
        public Stat_MajorGroup major;

        private const float HEALTH_PER_VITALITY = 5f;
        private const float VASION_PER_EVASION = .5f;
        private const float CRIT_CHANCE_PER_AGILITY = .3f;
        private const float CRIT_DAMAGE_PER_STRENGTH = .5f;
        private const float RESISTANCE_PER_INTELLIGENCE = .5f;

        public float GetElementalDamage(out ElementType element, float scaleFactor = 1f)
        {
            var fireDamage = offense.fireDamage.GetValue();
            var iceDamage = offense.iceDamage.GetValue();
            var lightningDamage = offense.lightningDamage.GetValue();
            var bonusElementalDamage = major.intelligence.GetValue();

            var highestDamage = fireDamage;
            element = ElementType.Fire;

            if (iceDamage > highestDamage)
            {
                highestDamage = iceDamage;
                element = ElementType.Ice;
            }

            if (lightningDamage > highestDamage)
            {
                highestDamage = lightningDamage;
                element = ElementType.Lightning;
            }

            if (highestDamage <= 0)
            {
                element = ElementType.None;
                return 0;
            }

            var bonusFire = (fireDamage == highestDamage) ? 0 : fireDamage * .5f;
            var bonusIce = (iceDamage == highestDamage) ? 0 : iceDamage * .5f;
            var bonusLightning = (lightningDamage == highestDamage) ? 0 : lightningDamage * .5f;

            var weakerElementsDamage = bonusFire + bonusIce + bonusLightning;
            var finalDamage = highestDamage + bonusElementalDamage + weakerElementsDamage;

            return finalDamage * scaleFactor;
        }

        public float GetElementalResistance(ElementType element)
        {
            var baseResistance = 0f;
            var bonusResistance = major.intelligence.GetValue() * RESISTANCE_PER_INTELLIGENCE;

            switch (element)
            {
                case ElementType.Fire:
                    baseResistance = defense.fireRes.GetValue();
                    break;

                case ElementType.Ice:
                    baseResistance = defense.iceRes.GetValue();
                    break;

                case ElementType.Lightning:
                    baseResistance = defense.lightningRes.GetValue();
                    break;
            }

            var resistance = baseResistance + bonusResistance;
            var resistanceCap = 75f;
            var finalResistance = Mathf.Clamp(resistance, 0f, resistanceCap);

            return finalResistance;
        }

        public float GetPhysicalDamage(out bool isCrit, float scaleFactor = 1f)
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

            return finalDamage * scaleFactor;
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
            var baseMaxHealth = resource.maxHealth.GetValue();
            var bonusHp = major.vitality.GetValue() * HEALTH_PER_VITALITY;

            var finalMaxHealth = baseMaxHealth + bonusHp;
            return finalMaxHealth;
        }

        public Stat GetStatByType(StatType statType)
        {
            return statType switch
            {
                StatType.MaxHelath => resource.maxHealth,
                StatType.HealthRegen => resource.healthRegen,
                StatType.Strength => major.strength,
                StatType.Agillity => major.agility,
                StatType.Intelligence => major.intelligence,
                StatType.Vitality => major.vitality,
                StatType.AttackSpeed => offense.attackSpeed,
                StatType.Damage => offense.damage,
                StatType.CritChance => offense.critChance,
                StatType.CritPoser => offense.critPower,
                StatType.ArmorReduction => offense.armorReduction,
                StatType.FireDamage => offense.fireDamage,
                StatType.IceDamage => offense.iceDamage,
                StatType.LightningDamage => offense.lightningDamage,
                StatType.Armor => defense.armor,
                StatType.Evasion => defense.evasion,
                StatType.IceResistance => defense.iceRes,
                StatType.FireResistance => defense.fireRes,
                StatType.LightningResistance => defense.lightningRes,
                _ => null
            };
        }

        [ContextMenu("Update Default Stat Setup")]
        public void ApplyDefaultStatSetup()
        {
            if (defaultStatSetup == null)
            {
                Debug.LogError("Default Stat Setup is not assigned in the inspector.");
                return;
            }

            resource.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
            resource.healthRegen.SetBaseValue(defaultStatSetup.healthRegen);

            major.strength.SetBaseValue(defaultStatSetup.strength);
            major.agility.SetBaseValue(defaultStatSetup.agility);
            major.intelligence.SetBaseValue(defaultStatSetup.intelligence);
            major.vitality.SetBaseValue(defaultStatSetup.vitality);

            offense.attackSpeed.SetBaseValue(defaultStatSetup.attackSpeed);
            offense.damage.SetBaseValue(defaultStatSetup.damage);
            offense.critChance.SetBaseValue(defaultStatSetup.critChance);
            offense.critPower.SetBaseValue(defaultStatSetup.critPower);
            offense.armorReduction.SetBaseValue(defaultStatSetup.armorReduction);

            offense.fireDamage.SetBaseValue(defaultStatSetup.fireDamage);
            offense.iceDamage.SetBaseValue(defaultStatSetup.iceDamage);
            offense.lightningDamage.SetBaseValue(defaultStatSetup.lightningDamage);

            defense.armor.SetBaseValue(defaultStatSetup.armor);
            defense.evasion.SetBaseValue(defaultStatSetup.evasion);

            defense.fireRes.SetBaseValue(defaultStatSetup.fireRes);
            defense.iceRes.SetBaseValue(defaultStatSetup.iceRes);
            defense.lightningRes.SetBaseValue(defaultStatSetup.lightningRes);
        }
    }
}