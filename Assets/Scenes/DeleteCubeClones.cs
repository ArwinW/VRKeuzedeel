using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core.SceneObjects;


// Behavior data for deleting cube clones
public class DeleteCubeClonesData : IBehaviorData
{
    public Metadata Metadata { get; set; }
    public string Name { get; set; }
}

// Stage process for deleting cube clones
public class DeleteCubeClonesProcess : StageProcess<DeleteCubeClonesData>
{
    public DeleteCubeClonesProcess(DeleteCubeClonesData data) : base(data) { }

    public override void Start()
    {
        // Find all GameObjects with the name pattern "Cube(clone)"
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("cube");
        foreach (GameObject cube in cubes)
        {
            if (cube.name.Contains("(Clone)"))
            {
                // Destroy the cube clone GameObject
                GameObject.Destroy(cube);
            }
        }
    }

    // The following methods can be empty as we don't need to update, end, or fast-forward the process for this behavior
    public override IEnumerator Update() { yield break; }
    public override void End() { }
    public override void FastForward() { }
}

// Behavior class for deleting cube clones
public class DeleteCubeClonesBehavior : Behavior<DeleteCubeClonesData>
{
    public DeleteCubeClonesBehavior()
    {
        // Set up behavior name
        Data.Name = "Delete Cube Clones";
    }

    public override IStageProcess GetActivatingProcess()
    {
        return new DeleteCubeClonesProcess(Data);
    }
}