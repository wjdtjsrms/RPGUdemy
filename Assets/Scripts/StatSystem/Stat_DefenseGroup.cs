namespace SSunSoft.RPGUdemy
{
    using System;

    [Serializable]
    public class Stat_DefenseGroup
    {
        // Physical Defense
        public Stat armor;
        public Stat evasion;

        // Elemental Resistance
        public Stat fireRes;
        public Stat iceRes;
        public Stat lightningRes;
    }
}