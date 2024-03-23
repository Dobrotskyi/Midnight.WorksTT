using UnityEngine;

namespace Scripts.Game.Buildings
{
    public class Hotel : Building
    {
        public new int GetValue => Mathf.FloorToInt(base.GetValue);

        public override string GetStats()
        {
            string stats = $"Increases workers capacity in nearby offices by {GetValue}";
            return stats;
        }
    }
}