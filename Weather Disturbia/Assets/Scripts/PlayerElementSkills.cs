using UnityEngine;
using UnityEngine.UI;

public class PlayerElementSkills : MonoBehaviour
{
    public bool isIce;

    public SpriteRenderer graphics;

    public GameObject projectileStandardFirePrefab;
    public GameObject projectileStandardIcePrefab;
    public Transform shootPosition;

    public Color iceColor;
    public Color fireColor;
    public Sprite iceIcon;
    public Sprite fireIcon;
    public Button buttonChangeElement;
    public Image imageNextElement;

    private string iceModeText = "Mode GLACE";
    private string fireModeText = "Mode FEU";

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
        ChangeElement();
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
            graphics.color = fireColor;
            buttonChangeElement.GetComponent<Image>().sprite = fireIcon;
            imageNextElement.GetComponent<Image>().sprite = iceIcon;
            buttonChangeElement.GetComponentInChildren<Text>().text = fireModeText;
            buttonChangeElement.GetComponentInChildren<Text>().color = fireColor;
        }
        else
        {
            isIce = true;
            graphics.color = iceColor;
            buttonChangeElement.GetComponent<Image>().sprite = iceIcon;
            imageNextElement.GetComponent<Image>().sprite = fireIcon;
            buttonChangeElement.GetComponentInChildren<Text>().text = iceModeText;
            buttonChangeElement.GetComponentInChildren<Text>().color = iceColor;
        }
    }
}
