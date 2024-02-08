using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

public class DeleteCubeClonesMenuItem : MenuItem<IBehavior>
{
    public override string DisplayedName { get; } = "Custom/Delete Cube Clones";

    public override IBehavior GetNewItem()
    {
        return new DeleteCubeClonesBehavior();
    }


}
