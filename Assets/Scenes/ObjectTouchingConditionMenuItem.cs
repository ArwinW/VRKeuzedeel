using System.Collections;
using UnityEngine;
using VRBuilder.Core;
using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Editor.UI.StepInspector.Menu;

public class ObjectTouchingConditionMenuItem : MenuItem<ICondition>
{
    public override string DisplayedName => "Custom/Object Touching Condition";

    public override ICondition GetNewItem()
    {
        return new ObjectTouchingCondition();
    }
}
