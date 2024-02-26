using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public float speed = 5f;
    public float max = 50f;
    public float position;
    float startpos;

    void Start()
    {
        startpos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(MinigameTimer.instance.mgame == MinigameTimer.Minigame.Tomato && !MinigameTimer.lost)
        {
            if(Input.GetKey(KeyCode.LeftArrow))
                position += speed * Time.deltaTime;

            if(Input.GetKey(KeyCode.RightArrow))
                position -= speed * Time.deltaTime;

            if(position > max) position = max;
            if(position < -max) position = -max;

            //position
            transform.position = new Vector3(startpos + position, transform.position.y, transform.position.z);
        }else{
            //move back to orig
            transform.position = Vector3.Lerp(transform.position, new Vector3(startpos, transform.position.y, transform.position.z), Time.deltaTime);
        }
    }
}
