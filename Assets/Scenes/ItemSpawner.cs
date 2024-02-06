using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core.SceneObjects;

[DataContract(IsReference = true)]
public class ItemSpawner : IBehaviorData
{

    [DataMember]
    [DisplayName("Spawner")]
    public SceneObjectReference spawner;

    Metadata IData.Metadata { get; set; }
    string INamedData.Name { get; }
}

// Stage process for the "Spawn Cube" behavior
public class SpawnCubeBehaviorActivatingProcess : StageProcess<ItemSpawner>
{
    public SpawnCubeBehaviorActivatingProcess(ItemSpawner data) : base(data)
    {
    }

    public override void Start()
    {
        // Get the position of the spawner
        Vector3 spawnerPosition = Data.spawner.Value.GameObject.transform.position;

        GameObject cubePrefab = Resources.Load<GameObject>("Cube");

        if (cubePrefab != null)
        {
            // Instantiate the cube prefab at the spawner's position
            GameObject newCube = GameObject.Instantiate(cubePrefab, spawnerPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Cube prefab not found in Resources folder!");
        }
    }

    // The following methods can be empty as we don't need to update, end, or fast-forward the process for this simple behavior

    public override IEnumerator Update()
    {
        yield break;
    }

    public override void End()
    {
    }

    public override void FastForward()
    {
    }
}

// Behavior class for the "Spawn Cube" behavior
[DataContract(IsReference = true)]
public class SpawnCubeBehavior : Behavior<ItemSpawner>
{
    public SpawnCubeBehavior()
    {
        Data.spawner = new SceneObjectReference(""); // Initialize spawner scene object reference
    }

    public override IStageProcess GetActivatingProcess()
    {
        return new SpawnCubeBehaviorActivatingProcess(Data);
    }
}
