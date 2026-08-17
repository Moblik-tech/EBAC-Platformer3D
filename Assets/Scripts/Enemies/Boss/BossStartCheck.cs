using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string tagToVerify = "Player";
    public GameObject bossCamera;
    public Color gizmosColor = Color.white;

    private void Awake()
    {
        bossCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToVerify))
        {
            TurnCameraOn();
        }
    }

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }
}