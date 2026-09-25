using Moblik.Core.Singleton;
using Moblik.Utils;
using System;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [Header("<------ Save Configs ------>")]
    [SerializeField, NaughtyAttributes.ReadOnly] private SaveSetupParams _saveSetup;
    //private string _path => $"{Application.persistentDataPath}/Save.txt";
    private string _path => $"{Application.streamingAssetsPath}/Save.txt";

    public Action<SaveSetupParams> FileLoaded;

    public SaveSetupParams Setup => _saveSetup;
    public bool IsLoaded { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        LoadGame();
    }

    #region SAVE

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetupParams
        {
            currentCheckPointKey = 0,
            coinAmount = 0,
            lifePackAmount = 0,
            hasPlayerData = false,
            playerCurrentHealth = 10,
            currentArmour = ArmourType.NONE
        };
    }

    public void SavePlayerStats(int currentHealth, ArmourType currentArmour)
    {
        _saveSetup.playerCurrentHealth = currentHealth;
        _saveSetup.currentArmour = currentArmour;

        SaveGame();
    }

    public void SaveInventory(int coinAmount, int lifePackAmount)
    {
        _saveSetup.coinAmount = coinAmount;
        _saveSetup.lifePackAmount = lifePackAmount;

        SaveGame();
    }

    public void SaveLastCheckPointKey(int checkPointKey)
    {
        _saveSetup.currentCheckPointKey = checkPointKey;

        SaveGame();
    }

    public void SaveCurrentGameState()
    {
        if (PlayerController.Instance != null)
        {
            _saveSetup.hasPlayerData = true;
            _saveSetup.playerCurrentHealth = PlayerController.Instance.healthBase.CurrentLife;
            _saveSetup.currentArmour = PlayerController.Instance.CurrentArmour;
        }

        if (Moblik.Manager.InventoryManager.Instance != null)
        {
            var inventory = Moblik.Manager.InventoryManager.Instance;

            _saveSetup.coinAmount = inventory.GetItemByType(Moblik.Utils.ItemType.COIN).scriptobInt.amount;
            _saveSetup.lifePackAmount = inventory.GetItemByType(Moblik.Utils.ItemType.LIFE_PACK).scriptobInt.amount;
        }

        SaveGame();
    }

    [NaughtyAttributes.Button]
    private void SaveGame()
    {
        if (_saveSetup == null)
        {
            Debug.LogWarning("Não foi possível salvar: SaveSetupParams não foi inicializado.");
            return;
        }

        string json = JsonUtility.ToJson(_saveSetup, true);

        File.WriteAllText(_path, json);

        Debug.Log($"Save criado em: {_path}");
        Debug.Log(json);
    }

    #endregion

    #region LOAD

    [NaughtyAttributes.Button]
    private void LoadGame()
    {
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);

            _saveSetup = JsonUtility.FromJson<SaveSetupParams>(json);
        }
        else
        {
            CreateNewSave();
            SaveGame();
        }

        IsLoaded = true;
        FileLoaded?.Invoke(_saveSetup);
    }

    #endregion
}