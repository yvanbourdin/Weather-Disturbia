using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player reaches the end of the game, it starts credits scene
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadCreditsScene());
        }
    }

    // Fade to credits scene
    private IEnumerator LoadCreditsScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Credits");
    }
}
