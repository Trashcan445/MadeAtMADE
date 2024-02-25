using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyThrowAnim : MonoBehaviour
{
    public Transform player;
    public Transform[] judges;
    public float timeToThrow = 0.25f;
    public AnimationCurve scaleCurve;
    public GameObject moneyPrefab;
    public Transform canvas;

    public void ThrowMoney()
    {
        StartCoroutine(MoneyThrowAnim_Coroutine());
    }

    IEnumerator MoneyThrowAnim_Coroutine()
    {
        Vector3 startPosition = player.transform.position;
        Vector3 startScale = moneyPrefab.transform.localScale;
        Vector3 endPosition = judges[Random.Range(0, judges.Length)].position;
        Quaternion endRotation = Quaternion.Euler(0f,0f,((Random.Range(0f,1f)>0.5f) ? 1f : -1f) * (Random.Range(360f,90f)));
    
        float timeElapsed = 0f; 
        float completion = 0f;
        
        GameObject money = Instantiate(moneyPrefab, canvas);
        money.transform.position = startPosition;
        Quaternion startRotation = Quaternion.Euler(0f,0f,Random.Range(0f,360f));
        money.transform.rotation = startRotation;
        endRotation *= money.transform.rotation;

        while(timeElapsed < timeToThrow)
        {
            completion = timeElapsed / timeToThrow;

            money.transform.position = Vector3.Lerp(startPosition, endPosition, completion);
           // money.transform.rotation = Quaternion.Lerp(startRotation, endRotation, completion);
            money.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, startScale, scaleCurve.Evaluate(completion));

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(money);
    }
}
