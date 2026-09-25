using UnityEngine;
using Moblik.Items;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CollectableBase i = other.transform.GetComponent<CollectableBase>();

        if (i != null && i.gameObject.GetComponent<Magnetic>() == null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}