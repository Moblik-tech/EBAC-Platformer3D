using UnityEngine;
using Moblik.Utils;

public class ArmourChanger : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private string shaderIDName = "_EmissionMap";

    private Texture2D _defaultTexture;

    private void Awake()
    {
        _defaultTexture = (Texture2D)skinnedMesh.materials[0].GetTexture(shaderIDName);
    }

    public void ChangeTexture(ArmourSetup armourSetup)
    {
        if (armourSetup == null)
        {
            Debug.LogWarning("ArmourSetup is null.");
            return;
        }

        skinnedMesh.materials[0].SetTexture(shaderIDName, armourSetup.armourTexture);
    }

    [NaughtyAttributes.Button]
    public void ResetTexture()
    {
        skinnedMesh.materials[0].SetTexture(shaderIDName, _defaultTexture);
    }
}