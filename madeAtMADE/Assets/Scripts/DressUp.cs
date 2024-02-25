using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DressUp : MonoBehaviour
{
    [Header("Clothes")]
    public Sprite[] hats;
    public Sprite[] tops;
    public Sprite[] bottoms;

    [Header("Dependencies")]
    public Image[] clothesImages;
    //merge the 3 arrays for ease of use
    public Sprite[][] clothes {get{return new Sprite[][]{hats,tops,bottoms};}}
    public int[] indices = new int[3];

    void Start()
    {
        //put on base clothes
        ChangeClothes(0);
        ChangeClothes(1);
        ChangeClothes(2);
    }


    public void CycleClothesForward(int index)
    {
        //CYCLE INDEX
        indices[index]++;
        //roll over
        indices[index]%=clothes[index].Length;

        //CHANGE APPEARED CLOTHES
        ChangeClothes(index);
    }

    public void CycleClothesBackward(int index)
    {
        //CYCLE INDEX
        indices[index]--;
        //roll under
        if(indices[index] < 0) indices[index] = clothes[index].Length-1;

        //CHANGE APPEARED CLOTHES
        ChangeClothes(index);
    }

    void ChangeClothes(int index)
    {
        //CHANGE APPEARED CLOTHES
        Sprite sprite = clothes[index][indices[index]];
        Image clothesImg = clothesImages[index];
        
        clothesImg.sprite = sprite;

        //make transparent if no image
        clothesImg.color = sprite!=null ? Color.white : new Color(1,1,1,0);
    }
}
