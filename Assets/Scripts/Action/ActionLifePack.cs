using UnityEngine;
using Moblik.Utils;
using Moblik.Manager;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SOInt sOInt;

    private void Start()
    {
        sOInt = InventoryManager.Instance.GetItemByType(ItemType.LIFE_PACK).scriptobInt;
    }

    private void RecoverLife()
    {
        if (sOInt.amount > 0 && PlayerController.Instance.healthBase.CurrentLife < PlayerController.Instance.healthBase.startLife)
        {
            InventoryManager.Instance.RemoveByType(ItemType.LIFE_PACK, 1);
            PlayerController.Instance.healthBase.ResetLife();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoverLife();
        }
    }
}