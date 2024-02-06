using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

public class ItemSpawnerMenuItem : MenuItem<IBehavior>
{
    public override string DisplayedName { get; } = "Custom/Item Spawner";

    public override IBehavior GetNewItem()
    {
        return new SpawnCubeBehavior();
    }


}
