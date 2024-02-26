using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
    public HookGame hookgame;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.tag == "Player")
        {
            hookgame.player.Play("gethooked",0,0f);
            MinigameTimer.lost = true;
        }
    }
}
