using VRBuilder.BasicInteraction.Conditions;
using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;

namespace VRBuilder.Editor.BasicInteraction.UI.Conditions
{
    public class ReleasedObjectWithTagMenuItem : MenuItem<ICondition>
    {
        public override string DisplayedName { get; } = "Custom/Release Object with Tag";

        public override ICondition GetNewItem()
        {
            return new ReleasedObjectWithTagCondition();
        }
    }
}