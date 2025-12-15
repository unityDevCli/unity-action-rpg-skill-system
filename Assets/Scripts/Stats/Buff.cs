using System.Collections;
using UnityEngine;

namespace Stats
{
    public class Buff
    {
        public static IEnumerator Apply(StatComponent stat, StatType type, float value, float duration)
        {
            stat.ModifyStat(type, value);
            yield return new WaitForSeconds(duration);
            stat.ModifyStat(type, -value);
        }
    }
}