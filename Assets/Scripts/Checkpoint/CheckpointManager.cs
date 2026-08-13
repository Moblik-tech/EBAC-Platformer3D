using UnityEngine;
using Moblik.Core.Singleton;
using System.Collections.Generic;

public class CheckpointManager : Singleton<CheckpointManager>
{
    [SerializeField, NaughtyAttributes.ReadOnly] private int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    protected override void Awake()
    {
        base.Awake();
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);

        return checkpoint.transform.position;
    }
}