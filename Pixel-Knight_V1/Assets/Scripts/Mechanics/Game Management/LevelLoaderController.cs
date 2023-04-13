using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderController : MonoBehaviour
{
    [SerializeField] bool isTransitionScreen;
    [SerializeField] bool isControllsScreen;

    const string IN = "Fade_In";
    const string OUT = "Fade_Out";

    string currentState;

    [SerializeField] GameObject CrossFade;

    Animator anim;
    public static LevelLoaderController Instance;

    private void Awake()
    {
        Instance = this;
        anim = CrossFade.GetComponent<Animator>();
    }

    void Start()
    {
        ChangeAnimationState(IN);

        if (isTransitionScreen)
        {
            StartCoroutine(TransitionScreen());
        }
    }


    void FixedUpdate()
    {
        if (isControllsScreen)
        {
            if (Input.GetButton("Jump") ||
                Input.GetButton("Fire") ||
                Input.GetButton("Cancel"))
            {
                ChangeAnimationState(OUT);
                Invoke("EndControllsScreen", 1);
            }
        }
    }

    void EndControllsScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeOut()
    {
        ChangeAnimationState(OUT);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadSavedLevel()
    {
        StartCoroutine(LoadSavedLevelCoroutine());
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(1.4f);

        ChangeAnimationState(OUT);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadSavedLevelCoroutine()
    {
        ChangeAnimationState(OUT);

        yield return new WaitForSeconds(1);

        string savedWorld = PlayerPrefs.GetString("Save World");
        SceneManager.LoadScene(savedWorld);
    }

    IEnumerator QuitGameCoroutine()
    {
        ChangeAnimationState(OUT);

        yield return new WaitForSeconds(1);

        Application.Quit();
    }

    IEnumerator TransitionScreen()
    {
        yield return new WaitForSeconds(3);

        ChangeAnimationState(OUT);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
