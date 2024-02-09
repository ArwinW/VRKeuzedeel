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
    /// Condition which is completed when the object with a specific tag becomes ungrabbed.
    /// </summary>
    [DataContract(IsReference = true)]
    public class ReleasedObjectWithTagCondition : Condition<ReleasedObjectWithTagCondition.EntityData>
    {
        [DisplayName("Release Object with Tag")]
        public class EntityData : IConditionData, INamedData
        {
            [DataMember]
            [DisplayName("Tag")]
            public SceneObjectTag<IGrabbableProperty> Tag { get; set; }

            [DataMember]
            [DisplayName("Cube Count")]
            public int CubeCount { get; set; } // New field to store the count of cubes associated with the condition

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
            public ActiveProcess(EntityData data) : base(data)
            {
            }

            protected override bool CheckIfCompleted()
            {
                // Check if any of the cubes associated with the condition are being released
                var releasedObjects = RuntimeConfigurator.Configuration.SceneObjectRegistry.GetByTag(Data.Tag.Guid)
                    .Where(sceneObject => sceneObject.UniqueName == $"Cube_{Data.CubeCount}")
                    .ToList();

                // Check if any of the cubes are grabbed
                foreach (var sceneObject in releasedObjects)
                {
                    var gameObject = sceneObject.GameObject;
                    var grabbableProperty = gameObject.GetComponent<IGrabbableProperty>();
                    if (grabbableProperty != null && grabbableProperty.IsGrabbed)
                    {
                        return false; // If any cube is grabbed, the condition is not completed
                    }
                }

                return true; // If none of the cubes are grabbed, the condition is completed
            }
        }

        private class EntityAutocompleter : Autocompleter<EntityData>
        {
            public EntityAutocompleter(EntityData data) : base(data)
            {
            }

            public override void Complete()
            {
                // Nothing needs to be done for autocompletion in this case
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

        protected override IAutocompleter GetAutocompleter()
        {
            return new EntityAutocompleter(Data);
        }

        // Method to increment the cube count associated with the condition
        public void IncrementCubeCount()
        {
            Data.CubeCount++;
        }
    }
}
