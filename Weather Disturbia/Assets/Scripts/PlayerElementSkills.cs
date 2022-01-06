using UnityEngine;

public class PlayerElementSkills : MonoBehaviour
{
    public bool isIce;
    public GameObject projectileStandardFirePrefab;
    public GameObject projectileStandardIcePrefab;
    public Transform shootPosition;

    public static PlayerElementSkills instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of PlayerElementSkills in the scene");
            return;
        }
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ShootStandardSkill();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeElement();
        }
    }

    void ShootStandardSkill()
    {
        if (isIce)
        {
            Instantiate(projectileStandardIcePrefab, shootPosition.position, shootPosition.rotation);
        }
        else
        {
            Instantiate(projectileStandardFirePrefab, shootPosition.position, shootPosition.rotation);
        }
    }

    void ChangeElement()
    {
        if(isIce)
        {
            isIce = false;
            Debug.Log("Fire mode !");
        }
        else
        {
            isIce = true;
            Debug.Log("Ice mode !");
        }
    }
}
