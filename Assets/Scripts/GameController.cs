using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel,winPanel;
    public GameObject character, backGround;
    public Button restart, replay;
    public bool isLoose, isWin;
   // public GameObject gameScreen;
    // Start is called before the first frame update
    void Start()
    {
        isLoose = false;
        isWin = false;
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        restart.onClick.AddListener(ResetGame);
        replay.onClick.AddListener(ResetGame);
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
        yield return new WaitForSeconds(0.5f);
        gameOverPanel.SetActive(true);
        character.SetActive(false);
        backGround.SetActive(false);
        
    }
    IEnumerator Winner()
    {
        yield return new WaitForSeconds(0.5f);
        winPanel.SetActive(true);
        character.SetActive(false);
        backGround.SetActive(false);
    }
    void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
