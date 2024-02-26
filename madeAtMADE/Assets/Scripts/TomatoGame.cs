using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TomatoGame : MonoBehaviour
{
    public Transform player;
    public Transform materPrefab;
    public Transform materCanvas;
    public Transform leftMaterSpawn;
    public Transform rightMaterSpawn;
    public float spread = 50f;
    float timeSinceMater = 0f;
    float timeToMater;
    public Vector2 timeToMaterrange = new Vector2(1.5f,3f);

    void Start()
    {
        SetRandomTime();
    }

    void SetRandomTime()
    {
        timeToMater = Random.Range(timeToMaterrange.x, timeToMaterrange.y);
    }

    void Update()
    {
        if(MinigameTimer.lost) return;

        if(timeSinceMater > timeToMater)
        {
            timeSinceMater = 0f;
            SetRandomTime();

            //spawn mater
            var obj = Instantiate(materPrefab, materCanvas);
            obj.transform.position = new Vector3(MinigameTimer.instance.player.transform.position.x + Random.Range(-spread, spread), leftMaterSpawn.transform.position.y, 0f);
        }

        timeSinceMater += Time.deltaTime;
    }
}
