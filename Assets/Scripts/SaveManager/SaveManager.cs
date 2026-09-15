using System.IO;
using UnityEngine;
using Moblik.Core.Singleton;
using System;
using Moblik.Cloth;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField, NaughtyAttributes.ReadOnly] private SaveSetupParams _saveSetup;
    [Space(15)]
    [Min(0)] public int currentLevelNumber = 0;
    
    private string _path = $"{Application.streamingAssetsPath}/save.txt";
    public Action<SaveSetupParams> FileLoaded;
    public SaveSetupParams Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetupParams
        {
            currentLevelNumber = 0,
            coinAmount = 0,
            lifePackAmount = 0,
            playerCurrentHealth = PlayerController.Instance.healthBase.startLife,
            currentCloth = ClothType.BLAND
        };
    }

    private void Start()
    {
        Invoke(nameof(LoadGame), 0.1f);
    }

    #region SAVE GAME
    [NaughtyAttributes.Button]
    private void SaveGame()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);

        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveParams()
    {
        _saveSetup.playerCurrentHealth = PlayerController.Instance.healthBase._currentLife;
        _saveSetup.currentCloth = ClothType.POWER;
        SaveItemsAmount();
    }

    public void SaveLastLevel(int levelNumber)
    {
        _saveSetup.currentLevelNumber = levelNumber;
        SaveItemsAmount();
        SaveGame();
    }

    public void SaveItemsAmount()
    {
        _saveSetup.coinAmount = Moblik.Items.ItemManager.Instance.GetItemByType(Moblik.Items.ItemType.COIN).sOInt.value;
        _saveSetup.lifePackAmount = Moblik.Items.ItemManager.Instance.GetItemByType(Moblik.Items.ItemType.LIFE_PACK).sOInt.value;
        SaveGame();
    }
    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    private void LoadGame()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetupParams>(fileLoaded);
            currentLevelNumber = _saveSetup.currentLevelNumber;
        }
        else
        {
            CreateNewSave();
            SaveGame();
        }

        FileLoaded?.Invoke(_saveSetup);
    }
}

[System.Serializable]
public class SaveSetupParams
{
    [Header("Level Setup")]
    public int currentLevelNumber;
    public int coinAmount;
    public int lifePackAmount;

    [Header("Player Setup")]
    public float playerCurrentHealth;
    public ClothType currentCloth;
}