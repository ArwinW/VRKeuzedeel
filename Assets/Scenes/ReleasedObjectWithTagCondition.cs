using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine.Scripting;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.Configuration;
using VRBuilder.Core.RestrictiveEnvironment;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Settings;
using VRBuilder.Core.Utils;
using VRBuilder.Core.Validation;

namespace VRBuilder.BasicInteraction.Conditions
{
    /// <summary>
    /// Condition which is completed when `GrabbableProperty` with the given tag becomes ungrabbed.
    /// </summary>
    [DataContract(IsReference = true)]
    [HelpLink("https://www.mindport.co/vr-builder/manual/default-conditions/release-object")]
    public class ReleasedObjectWithTagCondition : Condition<ReleasedObjectWithTagCondition.EntityData>
    {
        [DisplayName("Release Object with Tag")]
        public class EntityData : IConditionData
        {
            [DataMember]
            [DisplayName("Tag")]
            public SceneObjectTag<IGrabbableProperty> Tag { get; set; }

            public bool IsCompleted { get; set; }

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name
            {
                get
                {
                    string tag = SceneObjectTags.Instance.GetLabel(Tag.Guid);
                    tag = string.IsNullOrEmpty(tag) ? "<none>" : tag;

                    return $"Release an object with tag {tag}";
                }
            }

            public Metadata Metadata { get; set; }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            private IGrabbableProperty currentHeldObject;

            public ActiveProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                base.Start();
                currentHeldObject = null;
            }

            public void SetCurrentHeldObject(IGrabbableProperty heldObject)
            {
                currentHeldObject = heldObject;
            }

            protected override bool CheckIfCompleted()
            {
                return currentHeldObject == null || !currentHeldObject.IsGrabbed;
            }
        }

        [JsonConstructor, Preserve]
        public ReleasedObjectWithTagCondition() : this(Guid.Empty)
        {
        }

        public ReleasedObjectWithTagCondition(Guid guid)
        {
            Data.Tag = new SceneObjectTag<IGrabbableProperty>(guid);
        }

        public override IStageProcess GetActiveProcess()
        {
            return new ActiveProcess(Data);
        }
    }
}
