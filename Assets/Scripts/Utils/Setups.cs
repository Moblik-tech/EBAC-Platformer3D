using System;
using UnityEngine;

namespace Moblik.Utils
{
    [Serializable]
    public class SaveSetupParams
    {
        [Header("Level Setup")]
        public int currentCheckPointKey;
        public int coinAmount;
        public int lifePackAmount;

        [Header("Player Setup")]
        public bool hasPlayerData;
        public int playerCurrentHealth;
        public ArmourType currentArmour;
    }

    [Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt scriptobInt;
        public Sprite itemIcon;
    }

    [Serializable]
    public class ArmourSetup
    {
        public ArmourType armourType;
        public Texture2D armourTexture;
        public ArmourStats armourStats;
    }

    [Serializable]
    public class ArmourStats
    {
        public float newSpeed = 1f;
        public int damageReduction = 1;
    }

    [Serializable]
    public class AnimationSetup
    {
        public AnimationType animationType;
        public string triggerName;
    }

    [Serializable]
    public class MusicSetup
    {
        public MusicType musicType;
        public AudioClip audioClip;
    }

    [Serializable]
    public class SFXSetup
    {
        public SFXType sFXType;
        public AudioClip audioClip;
    }
}