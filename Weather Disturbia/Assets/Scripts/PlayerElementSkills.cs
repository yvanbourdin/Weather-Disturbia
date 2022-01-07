using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerElementSkills : MonoBehaviour
{
    public float standardSkillCooldownMax;
    public float specialSkillCooldownMax;
    public bool isIce;

    public SpriteRenderer graphics;

    public GameObject projectileStandardFirePrefab;
    public GameObject projectileStandardIcePrefab;    
    public GameObject projectileSpecialFirePrefab;
    public GameObject projectileSpecialIcePrefab;
    public Transform shootPosition;

    public Color iceColor;
    public Color fireColor;
    public Sprite iceIcon;
    public Sprite fireIcon;
    public Button buttonChangeElement;
    public Image iconNextElement;
    public Button buttonStandardSkill;
    public Button buttonSpecialSkill;

    private string iceModeText = "Mode GLACE";
    private string fireModeText = "Mode FEU";
    private bool standardSkillOnCooldown = false;
    private bool specialSkillOnCooldown = false;

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
        // A = standard skill
        if(Input.GetKeyDown(KeyCode.A) && !standardSkillOnCooldown)
        {
            ShootStandardSkill();
        }

        // E = special skill
        if (Input.GetKeyDown(KeyCode.E) && !specialSkillOnCooldown)
        {
            ShootSpecialSkill();
        }

        // C = change element
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeElement();
        }
    }

    public void ShootStandardSkill()
    {
        if (isIce)
        {
            Instantiate(projectileStandardIcePrefab, shootPosition.position, shootPosition.rotation);
        }
        else
        {
            Instantiate(projectileStandardFirePrefab, shootPosition.position, shootPosition.rotation);
        }

        StartCoroutine(CooldownStandardSkill());
    }

    public void ShootSpecialSkill()
    {
        if(isIce)
        {
            Instantiate(projectileSpecialIcePrefab, shootPosition.position, shootPosition.rotation);
        }
        else
        {
            Instantiate(projectileSpecialFirePrefab, shootPosition.position, shootPosition.rotation);
        }

        StartCoroutine(CooldownSpecialSkill());
    }

    public void ChangeElement()
    {
        if(isIce)
        {
            isIce = false;
            graphics.color = fireColor;
            buttonChangeElement.GetComponent<Image>().sprite = fireIcon;
            iconNextElement.GetComponent<Image>().sprite = iceIcon;
            buttonChangeElement.GetComponentInChildren<Text>().text = fireModeText;
            buttonChangeElement.GetComponentInChildren<Text>().color = fireColor;
        }
        else
        {
            isIce = true;
            graphics.color = iceColor;
            buttonChangeElement.GetComponent<Image>().sprite = iceIcon;
            iconNextElement.GetComponent<Image>().sprite = fireIcon;
            buttonChangeElement.GetComponentInChildren<Text>().text = iceModeText;
            buttonChangeElement.GetComponentInChildren<Text>().color = iceColor;
        }
    }

    IEnumerator CooldownStandardSkill()
    {
        float timeLeft = standardSkillCooldownMax;
        standardSkillOnCooldown = true;
        buttonStandardSkill.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);

        while (timeLeft > 0.01f)
        {
            buttonStandardSkill.GetComponentInChildren<Text>().text = " " + timeLeft.ToString();
            timeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        buttonStandardSkill.GetComponentInChildren<Text>().text = "";
        buttonStandardSkill.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        standardSkillOnCooldown = false;
    }

    public IEnumerator CooldownSpecialSkill()
    {
        float timeLeft = specialSkillCooldownMax;
        specialSkillOnCooldown = true;
        buttonSpecialSkill.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);

        while (timeLeft > 0)
        {
            buttonSpecialSkill.GetComponentInChildren<Text>().text = " " + timeLeft.ToString();
            timeLeft--;
            yield return new WaitForSeconds(1);
        }

        buttonSpecialSkill.GetComponentInChildren<Text>().text = "";
        buttonSpecialSkill.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        specialSkillOnCooldown = false;
    }
}
