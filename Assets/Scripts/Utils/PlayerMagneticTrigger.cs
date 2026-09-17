using UnityEngine;
using Moblik.Items;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CollectableItemBase i = other.transform.GetComponent<CollectableItemBase>();

        if (i != null && i.gameObject.GetComponent<Magnetic>() == null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}