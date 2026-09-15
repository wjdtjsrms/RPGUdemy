using System;
using System.Collections.Generic;
using UnityEngine;

namespace SSunSoft.RPGUdemy
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private float baseValue;
        [SerializeField] private List<StatModifier> modifiers = new();

        private bool needToCalculate = true;
        private float finalValue;

        public float GetValue()
        {
            if (needToCalculate)
            {
                finalValue = GetFinalValue();
                needToCalculate = false;
            }

            return finalValue;
        }

        public void AddModifier(float value, string source)
        {
            StatModifier modToAdd = new(value, source);
            modifiers.Add(modToAdd);
            needToCalculate = true;
        }

        public void RemoveModifier(string source)
        {
            modifiers.RemoveAll(modifier => modifier.source == source);
            needToCalculate = true;
        }

        private float GetFinalValue()
        {
            var finalValue = baseValue;

            foreach (var modifier in modifiers)
            {
                finalValue += modifier.value;
            }

            return finalValue;
        }

        public void SetBaseValue(float value) => baseValue = value;
    }

    [Serializable]
    public class StatModifier
    {
        public float value;
        public string source;

        public StatModifier(float value, string source)
        {
            this.value = value;
            this.source = source;
        }
    }
}