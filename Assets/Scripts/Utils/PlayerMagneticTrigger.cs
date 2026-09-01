using UnityEngine;
using Moblik.Items;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase i = other.transform.GetComponent<ItemCollectableBase>();

        if (i != null && i.gameObject.GetComponent<Magnetic>() == null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}