using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    [Min(1)] public int key = 1;

    [SerializeField, NaughtyAttributes.ReadOnly] bool _checkpointActived = false;
    private string _checkpointKey = "CheckpointKey";

    private void Start()
    {
        ChangeTotemLight(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_checkpointActived == false && other.CompareTag("Player"))
        {
            UpdateCheckpointStatus();
        }
    }

    private void UpdateCheckpointStatus()
    {
        SaveCheckpoint();
        ChangeTotemLight(true);
    }

    private void ChangeTotemLight(bool activated)
    {
        if (activated == true)
        {
            meshRenderer.material.SetColor("_EmissionColor", Color.white);
        }
        else
        {
            meshRenderer.material.SetColor("_EmissionColor", Color.grey);
        }
    }

    private void SaveCheckpoint()
    {
        if (PlayerPrefs.GetInt(_checkpointKey, 0) > key)
        {
            PlayerPrefs.SetInt(_checkpointKey, key);
        }

        CheckpointManager.Instance.SaveCheckpoint(key);
        _checkpointActived = true;
    }
}