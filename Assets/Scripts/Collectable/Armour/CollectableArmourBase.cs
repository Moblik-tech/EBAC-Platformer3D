using UnityEngine;
using Moblik.Utils;

namespace Moblik.Items
{
    public class CollectableArmourBase : CollectableBase
    {
        [Header("<------ Armour Configs ------>")]
        public ArmourType armourType;

        protected override void OnCollect()
        {
            base.OnCollect();
            PlayerController.Instance.ChangeArmour(armourType);
        }
    }
}