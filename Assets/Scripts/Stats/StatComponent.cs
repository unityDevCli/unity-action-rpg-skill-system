using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    public class StatComponent : MonoBehaviour
    {
        private Dictionary<StatType, float> stats = new();

        private void Awake()
        {
            stats[StatType.Hp] = 100;
            stats[StatType.Attack] = 10;
            stats[StatType.Defense] = 5;
        }

        public float GetStat(StatType stat)
        {
            return stats.GetValueOrDefault(stat, 0);
        }


        public void ModifyStat(StatType stat, float amount)
        {
            stats[stat] += amount;
        }
    }
}