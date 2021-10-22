using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel,winPanel, pausePanel;
    public GameObject character, backGround;
    public Button restart, replay, pause, resume, quit, rsPause;
    public bool isLoose, isWin;
    private AudioSource source;
    public AudioClip backgroundSound, gameOverSound, winnerSound;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isLoose = false;
        isWin = false;
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        restart.onClick.AddListener(ResetGame);
        replay.onClick.AddListener(ResetGame);
        rsPause.onClick.AddListener(ResetGame);
        pause.onClick.AddListener(PauseGame);
        resume.onClick.AddListener(ResumGame);
        quit.onClick.AddListener(QuitGame);
        source = gameObject.AddComponent<AudioSource>();
        source.clip = backgroundSound;
        source.loop = true;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin)
        {
            StartCoroutine(Winner());
        }
        else if(isLoose)
        {
            StartCoroutine(GameOver());
        }    
    }
    IEnumerator GameOver()
    {
        isLoose = false;
        yield return new WaitForSeconds(1.0f);
        gameOverPanel.SetActive(true);
        character.SetActive(false);
        backGround.SetActive(false);
        source.clip = gameOverSound;
        source.Play();
        source.loop = false;
    }
    IEnumerator Winner()
    {
        isWin = false;
        yield return new WaitForSeconds(1.0f);
        winPanel.SetActive(true);
        character.SetActive(false);
        backGround.SetActive(false);
        source.clip = winnerSound;
        source.Play();
        source.loop = false;
    }
    void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        character.SetActive(false);
        backGround.SetActive(false);
        pause.enabled = false;
    }
    void ResumGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        character.SetActive(true);
        backGround.SetActive(true);
        pause.enabled = true;
    }
    void QuitGame()
    {
        Application.Quit();
    }   
}
