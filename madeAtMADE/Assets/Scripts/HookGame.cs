using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HookGame : MonoBehaviour
{
    public Animator player;
    public Animator hook;
    float timeSinceHook = 0f;
    float timeToHook;
    public Vector2 timeToHookrange = new Vector2(1.5f,3f);

    void Start()
    {
        SetRandomHookTime();
    }

    void SetRandomHookTime()
    {
        timeToHook = Random.Range(timeToHookrange.x, timeToHookrange.y);
    }

    void Update()
    {
        if(MinigameTimer.lost) return;
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.Play("jumpOverHook");
        }

        if(timeSinceHook > timeToHook)
        {
            timeSinceHook = 0f;
            SetRandomHookTime();
            hook.Play("hook");
        }

        timeSinceHook += Time.deltaTime;
    }
}
