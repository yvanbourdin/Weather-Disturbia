using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public int coinsPickedUpInThisSceneCount; // Delete the amount of picked up coins if the player dies during the level
    public Vector3 respawnPoint;
    public int levelToUnlock;

    public static CurrentSceneManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of CurrentSceneManager in the scene");
            return;
        }
        instance = this;

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position; // default (first) spawn is where the player starts the level
    }
}
