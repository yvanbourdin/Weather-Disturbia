using UnityEngine;
using UnityEngine.UI;

public class ElementSystemUI : MonoBehaviour
{
    public Color iceColor;
    public Color fireColor;
    public Sprite iceIcon;
    public Sprite fireIcon;

    public Button buttonChangeElement;
    public Image iconNextElement;
    public Button buttonStandardSkill;
    public Button buttonSpecialSkill;
    public Image imageStandardSkill;
    public Image imageSpecialSkill;

    public Sprite iconProjectileStandardFire;
    public Sprite iconProjectileStandardIce;
    public Sprite iconProjectileSpecialFire;
    public Sprite iconProjectileSpecialIce;

    public static ElementSystemUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of ElementSystemUI in the scene");
            return;
        }
        instance = this;
    }

}
