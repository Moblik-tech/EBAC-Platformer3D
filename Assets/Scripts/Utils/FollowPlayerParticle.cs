using UnityEngine;

public class FollowPlayerParticle : MonoBehaviour
{
    public Transform playerTransform;

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 20, playerTransform.position.z);
    }
}