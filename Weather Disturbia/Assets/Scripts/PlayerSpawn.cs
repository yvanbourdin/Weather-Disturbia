using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        // Place the player at the position of this object (PlayerSpawn)
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
