using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    [Min(1)] public int key = 1;

    private bool _checkpointActived = false;
    private string _checkpointKey = "CheckpointKey";

    private void Start()
    {
        TurnOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_checkpointActived == false && other.CompareTag("Player"))
        {
            ValidateCheckpoint();
        }
    }

    private void ValidateCheckpoint()
    {
        SaveCheckpoint();
        TurnOn();
    }

    [NaughtyAttributes.Button]
    private void TurnOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    [NaughtyAttributes.Button]
    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckpoint()
    {
        /*if (PlayerPrefs.GetInt(_checkpointKey, 0) > key)
        {
            PlayerPrefs.SetInt(_checkpointKey, key);
        }*/

        CheckpointManager.Instance.SaveCheckpoint(key);

        _checkpointActived = true;
    }
}