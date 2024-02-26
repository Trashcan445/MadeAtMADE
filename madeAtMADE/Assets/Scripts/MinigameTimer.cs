using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTimer : MonoBehaviour
{
    public enum Minigame {Bribe, Hook, Tomato};
    public Minigame mgame;
    public GameObject bribeMinigame;
    public GameObject hookMinigame;
    public GameObject tomatoMinigame;
    public int minigamesToWin = 1;
    public int minigamesWon = 0;
    public string[] winTexts;
    public GameObject success;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject player;
    public GameObject timer;
    public GameObject countdown;
    public Transform moneyCanvas;
    public GameObject[] mGames {get{return new GameObject[3]{bribeMinigame, hookMinigame, tomatoMinigame};}}
    int index;
    public float timeLeft = 10f;
    public float countdownDuration = 1.5f;
    public Transform barFill;
    public static bool bribedSuccessfully;
    public static bool lost;
    public static MinigameTimer instance; 

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //mgame = (Minigame) Random.Range(0,2);

        bribeMinigame.SetActive(false);
        hookMinigame.SetActive(false);
        tomatoMinigame.SetActive(false);
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);

        StartCoroutine(StartMinigame());

        lost = false;
        bribedSuccessfully = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator StartMinigame()
    {
        timer.gameObject.SetActive(false);
        //enable minigame
        mGames[(int)mgame].SetActive(false);
            
        //do countdown
        countdown.SetActive(true);

        yield return new WaitForSeconds(countdownDuration);

        countdown.SetActive(false);

        //enable minigame and timer
        mGames[(int)mgame].SetActive(true);

        //enable timer
        timer.gameObject.SetActive(true);

        float timeElapsed = 0f;
        
        while(timeElapsed < timeLeft)
        {
            barFill.localScale = new Vector3(Mathf.Clamp01(1f - (timeElapsed / timeLeft)), 1f, 1f);

            //we lost a nonbribe minigame
            if(lost && mgame != Minigame.Bribe)
                break;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        bool won = (bribedSuccessfully && mgame == Minigame.Bribe) || (!lost && mgame != Minigame.Bribe);
        

        if(mgame == Minigame.Bribe)
        {
            for(int i = moneyCanvas.childCount - 1; i >= 0; i--)
            {
                Destroy(moneyCanvas.GetChild(i).gameObject);
            }
        }

        if(won)
        {
            mGames[(int)mgame].SetActive(false);
            minigamesWon++;

            if(minigamesWon >= minigamesToWin){
                timer.gameObject.SetActive(false);
                winScreen.SetActive(true);
                player.SetActive(false);
            }else{
                //go to next minigame
                StartCoroutine(SwitchMinigames());
            }
        }else{
            timer.gameObject.SetActive(false);

            if(mgame == Minigame.Hook){
                //wait for hook anim
                Invoke("Lose",.5f);
            }else{
                Lose();
            }
        }
    }

    void Lose()
    {
        mGames[(int)mgame].SetActive(false);
        gameOverScreen.SetActive(true);
        player.SetActive(false);
        Debug.Log("lost");
        
    }

    IEnumerator SwitchMinigames()
    {
        Debug.Log("switching minigames");

        success.GetComponent<TextMeshProUGUI>().text = winTexts[(int)mgame];
        //show success text
        success.SetActive(true);
        yield return new WaitForSeconds(1f);  
        success.SetActive(false);

        //next minigame
        int nextGame = (((int)mgame)+1);
        if(nextGame > 2)
        {
            nextGame = 0;
        }
        mgame = (Minigame) nextGame;

        StartCoroutine(StartMinigame());

        //intermission
        //next minigame
    }
}
