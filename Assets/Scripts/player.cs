using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int currentPosition = 0;
    public bool drowned = false;
    public bool isCarrying = false;
 
    public Sprite withPackage;
    public Sprite withOutPackage;
    public Sprite hasDrowned;

    public SpriteRenderer SR;

   public void changeSprite()
    {
        if(drowned == true)
        {
            SR.sprite = hasDrowned;
        }
        if(isCarrying == true)
        {
            SR.sprite = withPackage;
        }
        else
        {
            SR.sprite = withOutPackage;
        }
    }


    
    
}
