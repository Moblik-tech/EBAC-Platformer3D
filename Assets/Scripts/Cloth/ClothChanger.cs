using UnityEngine;

namespace Moblik.Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer skinnedMesh;
        public Texture2D texture;
        public string shaderIDName = "_EmissionMap";
        
        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = (Texture2D) skinnedMesh.materials[0].GetTexture(shaderIDName);
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            skinnedMesh.materials[0].SetTexture(shaderIDName, texture);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            //skinnedMesh.sharedMaterials[skinnedMesh.sharedMaterials.Length]
            skinnedMesh.sharedMaterials[0].SetTexture(shaderIDName, setup.clothTexture);
        }

        public void ResetTexture()
        {
            skinnedMesh.materials[0].SetTexture(shaderIDName, _defaultTexture);
        }
    }
}