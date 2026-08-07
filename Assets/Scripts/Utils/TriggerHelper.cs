using UnityEngine;
using Moblik.Enemy.Boss;

public class TriggerHelper : MonoBehaviour
{
    public Transform spawnPosition;
    public BossBase boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.SwitchState(BossAction.INIT);
            boss.transform.position = spawnPosition.position;
        }
    }
}