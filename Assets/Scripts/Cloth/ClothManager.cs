using System.Collections.Generic;
using UnityEngine;
using Moblik.Core.Singleton;

namespace Moblik.Cloth
{
    public enum ClothType
    {
        BLAND, SPEED, POWER
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetup;

        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetup.Find(i => i.clothType == clothType);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D clothTexture;
    }
}