using System;
using System.IO;
using UnityEngine;
using Moblik.Core.Singleton;
using Moblik.Utils;

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

    private void Start()
    {
        Invoke(nameof(LoadGame), 0.1f);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetupParams
        {
            currentLevelNumber = 0,
            coinAmount = 0,
            lifePackAmount = 0,
            playerCurrentHealth = PlayerController.Instance.healthBase.startLife,
            currentArmour = ArmourType.NONE
        };
    }

    #region SAVE GAME

    [NaughtyAttributes.Button]
    private void SaveGame()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);

        SaveFile(setupToJson);
        Debug.Log(setupToJson);
    }

    public void SaveParams()
    {
        var player = PlayerController.Instance;

        _saveSetup.playerCurrentHealth = player.healthBase._currentLife;
        _saveSetup.currentArmour = player.CurrentArmour;

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
        _saveSetup.coinAmount = Moblik.Manager.InventoryManager.Instance.GetItemByType(Moblik.Utils.ItemType.COIN).scriptobInt.value;
        _saveSetup.lifePackAmount = Moblik.Manager.InventoryManager.Instance.GetItemByType(Moblik.Utils.ItemType.LIFE_PACK).scriptobInt.value;

        SaveGame();
    }

    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    #region LOAD GAME

    [NaughtyAttributes.Button]
    private void LoadGame()
    {
        if (File.Exists(_path))
        {
            string fileLoaded = File.ReadAllText(_path);

            _saveSetup = JsonUtility.FromJson<SaveSetupParams>(fileLoaded);
            currentLevelNumber = _saveSetup.currentLevelNumber;
        }
        else
        {
            CreateNewSave();
            SaveGame();
        }

        ApplySaveData();
        FileLoaded?.Invoke(_saveSetup);
    }

    private void ApplySaveData()
    {
        PlayerController.Instance.ChangeArmour(_saveSetup.currentArmour);
    }

    #endregion
}