using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tomato : MonoBehaviour
{
    public bool active;
    public float radius;
    public AudioClip throwclip;
    public AudioClip splatclip;
    public AudioSource source;

    public void PlayThrow(){
        source.PlayOneShot(throwclip);
    }

    public void PlaySplat(){
        source.PlayOneShot(splatclip);
    }


    void Start()
    {
        Invoke("Dest", 3f); 
    }

    void Dest()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if(active && Physics2D.CircleCast(transform.position, radius, Vector2.up, 0))
        {
            MinigameTimer.lost = true;
        }
    }
}
