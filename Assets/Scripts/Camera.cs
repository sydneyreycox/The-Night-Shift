using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x+5f, transform.position.y, transform.position.z);
    }
}
