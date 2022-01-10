using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerElementSkills : MonoBehaviour
{
    public float standardSkillCooldownMax;
    public float specialSkillCooldownMax;
    public bool isIce;
    public float bonusSpeedInIceMode;

    public SpriteRenderer graphics;

    public GameObject projectileStandardFirePrefab;
    public GameObject projectileStandardIcePrefab;    
    public GameObject projectileSpecialFirePrefab;
    public GameObject projectileSpecialIcePrefab;
    public Transform shootPosition;

    private ElementSystemUI elementUI; // get all UI about element (icons, sprites, buttons...)

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

        elementUI = ElementSystemUI.instance; // get access to the element UI
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
            graphics.color = elementUI.fireColor;
            elementUI.buttonChangeElement.GetComponent<Image>().sprite = elementUI.fireIcon;
            elementUI.iconNextElement.GetComponent<Image>().sprite = elementUI.iceIcon;
            elementUI.buttonChangeElement.GetComponentInChildren<Text>().text = fireModeText;
            elementUI.buttonChangeElement.GetComponentInChildren<Text>().color = elementUI.fireColor;
            elementUI.imageStandardSkill.sprite = elementUI.iconProjectileStandardFire;
            elementUI.imageSpecialSkill.sprite = elementUI.iconProjectileSpecialFire;
            PlayerMovement.instance.moveSpeed -= bonusSpeedInIceMode;
        }
        else
        {
            isIce = true;
            graphics.color = elementUI.iceColor;
            elementUI.buttonChangeElement.GetComponent<Image>().sprite = elementUI.iceIcon;
            elementUI.iconNextElement.GetComponent<Image>().sprite = elementUI.fireIcon;
            elementUI.buttonChangeElement.GetComponentInChildren<Text>().text = iceModeText;
            elementUI.buttonChangeElement.GetComponentInChildren<Text>().color = elementUI.iceColor;
            elementUI.imageStandardSkill.sprite = elementUI.iconProjectileStandardIce;
            elementUI.imageSpecialSkill.sprite = elementUI.iconProjectileSpecialIce;
            PlayerMovement.instance.moveSpeed += bonusSpeedInIceMode;
        }
    }

    IEnumerator CooldownStandardSkill()
    {
        standardSkillOnCooldown = true;
        float timeLeft = standardSkillCooldownMax;
        elementUI.buttonStandardSkill.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        elementUI.imageStandardSkill.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); 

        while (timeLeft > 0.01f)
        {
            elementUI.buttonStandardSkill.GetComponentInChildren<Text>().text = " " + timeLeft.ToString();
            timeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        elementUI.buttonStandardSkill.GetComponentInChildren<Text>().text = "";
        elementUI.buttonStandardSkill.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        elementUI.imageStandardSkill.color = new Color(1f, 1f, 1f, 1f);
        standardSkillOnCooldown = false;
    }

    public IEnumerator CooldownSpecialSkill()
    {
        specialSkillOnCooldown = true;
        float timeLeft = specialSkillCooldownMax;    
        elementUI.buttonSpecialSkill.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        elementUI.imageSpecialSkill.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        while (timeLeft > 0)
        {
            elementUI.buttonSpecialSkill.GetComponentInChildren<Text>().text = " " + timeLeft.ToString();
            timeLeft--;
            yield return new WaitForSeconds(1);
        }

        elementUI.buttonSpecialSkill.GetComponentInChildren<Text>().text = "";
        elementUI.buttonSpecialSkill.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        elementUI.imageSpecialSkill.color = new Color(1f, 1f, 1f, 1f);
        specialSkillOnCooldown = false;
    }
}
