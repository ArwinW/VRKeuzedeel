using System.Collections;
using UnityEngine;
using VRBuilder.Core;
using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.SceneObjects;

[DataContract(IsReference = true)]
[DisplayName("Object Touching Condition")]
public class ObjectTouchingConditionData : IConditionData
{
    [DataMember]
    public SceneObjectReference ObjectToCheck { get; set; }

    [DataMember]
    public string TagToCheck { get; set; }

    public Metadata Metadata { get; set; }
    public string Name { get; set; }
    public bool IsCompleted { get; set; }

    public ObjectTouchingConditionData()
    {
        ObjectToCheck = new SceneObjectReference("");
        TagToCheck = "Untagged";
    }
}

public class ObjectTouchingConditionActiveProcess : StageProcess<ObjectTouchingConditionData>
{
    public override void Start()
    {
    }

    public override IEnumerator Update()
    {
        Collider[] colliders = Data.ObjectToCheck.Value.GameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(Data.TagToCheck))
            {
                Data.IsCompleted = true;
                yield break;
            }
        }

        yield return null;
    }

    public override void End()
    {
    }

    public override void FastForward()
    {
    }

    public ObjectTouchingConditionActiveProcess(ObjectTouchingConditionData data) : base(data)
    {
    }
}

public class ObjectTouchingCondition : Condition<ObjectTouchingConditionData>
{
    public override IStageProcess GetActiveProcess()
    {
        return new ObjectTouchingConditionActiveProcess(Data);
    }

    protected override IAutocompleter GetAutocompleter()
    {
        return null;
    }

    public ObjectTouchingCondition()
    {
        Data.Name = "Object Touching Condition";
    }
}
