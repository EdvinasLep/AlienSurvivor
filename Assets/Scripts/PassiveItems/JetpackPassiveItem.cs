using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentMoveSpeed *= 1 + passiveItemData.Multipliier / 100f;
    }
}
