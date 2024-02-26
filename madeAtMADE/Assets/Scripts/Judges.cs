using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Judges : MonoBehaviour
{
    [TextArea(3,25)] public string[] bad;
    [TextArea(3,25)] public string[] okay;
    [TextArea(3,25)] public string[] good;
    [TextArea(3,25)] public string[] great;
    string[][] speeches {get{return new string[][]{bad,okay,good,great};}}

    float minTimeBetweenSpeech = 1f;
    public Vector2 timeBetweenSpeech;
    [HideInInspector] public float timeSinceSpeech = 0f;

    [Space]
    public Transform[] judges;
    [Space]
    public Transform canvas;
    public GameObject speechBubblePrefab;
    public float typeDelay = 0.01f;
    public float destroyDelay = 1.5f;
    [Space]
    public UnityEvent onJudgingStart;
    [Space]
    public TextMeshProUGUI moneyCount;
    public int moneyGiven = 0;
    public int moneyInterval = 2500;
    public Vector2Int bribeAmt = new Vector2Int(10,100);
    int speechIndex {get{return Mathf.Clamp(moneyGiven / moneyInterval, 0, speeches.Length-1);}}
    public bool[] talking = new bool[3];
    public Transform judgefavorBar;

    void Start()
    {
        onJudgingStart?.Invoke();
    }


    // Update is called once per frame
    void Update()
    {
        if(MinigameTimer.instance.bribeMinigame.gameObject.activeSelf)
        {
            if(timeSinceSpeech > minTimeBetweenSpeech)
            {
                SpeakRandom();
                minTimeBetweenSpeech = Random.Range(timeBetweenSpeech.x, timeBetweenSpeech.y);
                timeSinceSpeech = 0f;
            }

            timeSinceSpeech+=Time.deltaTime;
        }
    }

    void SpeakRandom()
    {
        int judgeIndex = 0;
        for (int i = 0; i < 10; i++)
        {
            judgeIndex = Random.Range(0, judges.Length);
            if(!talking[judgeIndex]) break;
        }
        Speak(judges[judgeIndex].position, speeches[speechIndex][Random.Range(0, speeches[speechIndex].Length)], judgeIndex);
        timeSinceSpeech = 0f;
    }

    void Speak(Vector3 position, string text, int judge)
    {
        GameObject bubble = Instantiate(speechBubblePrefab, canvas);
        bubble.transform.position = position;
        StartCoroutine(Speak_Coroutine(bubble, text, judge));
    }

    IEnumerator Speak_Coroutine(GameObject bubble, string text, int judge)
    {
        talking[judge] = true;

        TextMeshProUGUI txt = bubble.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            txt.text += text[i];

            if(text[i] != ' ')
            yield return new WaitForSeconds(typeDelay);
        }

        yield return new WaitForSeconds(destroyDelay);

        bubble.GetComponent<Animator>().Play("close");

        talking[judge] = false;

        yield return new WaitForSeconds(1f);

        Destroy(bubble);
    }

    public void GiveMoney()
    {
        moneyGiven+=Random.Range(bribeAmt.x, bribeAmt.y);
        moneyCount.text = "$" + (moneyGiven / 100f).ToString("f2");

        SetJudgeFavorBar();
int maxMoney = moneyInterval * speeches.Length;
        if(moneyGiven >= maxMoney)
        {
            MinigameTimer.bribedSuccessfully = true;
        }
    }

    public void SetJudgeFavorBar()
    {
        int maxMoney = moneyInterval * speeches.Length;
        judgefavorBar.localScale = new Vector3(moneyGiven / (maxMoney * 1f), 1f, 1f);
    }
}
