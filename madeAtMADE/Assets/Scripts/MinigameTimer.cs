using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinigameTimer : MonoBehaviour
{
    public enum Minigame {Bribe, Hook, Tomato};
    public Minigame mgame;
    public GameObject bribeMinigame;
    public GameObject hookMinigame;
    public GameObject tomatoMinigame;
    public GameObject gameOverScreen;
    public GameObject[] mGames {get{return new GameObject[3]{bribeMinigame, hookMinigame, tomatoMinigame};}}
    int index;
    public float timeLeft = 10f;
    public Transform barFill;
    public static bool bribedSuccessfully;
    public static bool lost;

    void Start()
    {
        mgame = 0;//(Minigame) Random.Range(0,2);
        StartCoroutine(StartMinigame());

        lost = false;
        bribedSuccessfully = false;
    }

    IEnumerator StartMinigame()
    {
        //do countdown
        //enable minigame and timer
        mGames[(int)mgame].SetActive(true);

        //enable timer

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

        if(won)
        {
            StartCoroutine(SwitchMinigames());
        }else{
            mGames[(int)mgame].SetActive(false);
            gameOverScreen.SetActive(true);
            Debug.Log("lost");
        }
    }

    IEnumerator SwitchMinigames()
    {
        Debug.Log("switching minigames");

        yield return null;

        //intermission
        //next minigame
    }
}
