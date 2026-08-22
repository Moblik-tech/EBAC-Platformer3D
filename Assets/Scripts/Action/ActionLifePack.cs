using UnityEngine;
using Moblik.Items;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.L;
    public SOInt sOInt;

    private void Start()
    {
        sOInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).sOInt;
    }

    private void RecoverLife()
    {
        if (sOInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK, 1);

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