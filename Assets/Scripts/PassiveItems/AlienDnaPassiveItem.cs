using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDnaPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMight *= 1 + passiveItemData.Multipliier / 100f;
    }
}
