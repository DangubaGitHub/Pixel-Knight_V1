using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderController : MonoBehaviour
{
    

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
    }


    void Update()
    {
        
    }

    public void FadeOut()
    {
        ChangeAnimationState(OUT);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        ChangeAnimationState(OUT);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}
