using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BribeGame : MonoBehaviour
{
    public Judges judgesscript;
    public Animator moneybuttonAnimator;
    public Button button;

    // Start is called before the first frame update
    void OnEnable()
    {
        MinigameTimer.bribedSuccessfully = false;
        judgesscript.moneyGiven = 0;
        judgesscript.timeSinceSpeech = 0f;
        judgesscript.SetJudgeFavorBar();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PressMoneyButton();
        }
    }

    void PressMoneyButton(){
        moneybuttonAnimator.ResetTrigger("Pressed");
        moneybuttonAnimator.SetTrigger("Pressed");
        button.onClick?.Invoke();
    }
}
