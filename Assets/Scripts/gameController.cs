using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public List<Transform> playerPositions = new List<Transform>();
    public List<Transform> drownedPositions = new List<Transform>();
    public List<Transform> turtlePositions = new List<Transform>();
    public List<turtle> turtles = new List<turtle>();

    public player deliveryMan;

    public int currentPosition = 0;

    public TextMeshPro scoreText;
    public TextMeshPro gameOverText;
    public TextMeshPro gameOverScore;


    public GameObject scoreContainer;

    int dangerPos = -1;

    bool isSafe, hardMode = false;

    int points = 0;
    int seconds = 2, diveSeconds = 2;


    private int random, random2, startPos = 0, divePos, divePos2;

    private void Start()
    {

        scoreText.text = points.ToString();
        
        
        deliveryMan.transform.position = playerPositions[currentPosition].position;

        for (int i = 0; i < turtles.Count; i++)
        {
            turtles[i].transform.position = turtlePositions[startPos].position;
            startPos = startPos + 2;
        }
        StartCoroutine("waitTime");

        gameOverText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(seconds);
        turtleDive();
    }

    IEnumerator diveTime()
    {
        StopCoroutine("waitTime");

        yield return new WaitForSeconds(diveSeconds);
        turtleRise();

    }

    void turtleRise()
    {
        StopCoroutine(diveTime());
        divePos--;
        turtles[random].transform.position = turtlePositions[divePos].position;
        turtles[random].isDiving = false;

        

        StartCoroutine("waitTime");
    }

    private void turtleDive()
    {
        StopCoroutine(waitTime());
        random = Random.Range(0, 4);

        if (random == dangerPos && isSafe == false) 
         {
            gameOver();

        }
        

        divePos = turtles[random].position * 2;
       
        divePos++;
        

        turtles[random].transform.position = turtlePositions[divePos].position;
        turtles[random].isDiving = true;

        
        StartCoroutine(diveTime());
    }

    private void OnEnable()
    {
        buttonInput.OnLeft += OnLeftPressed;
        buttonInput.OnRight += OnRightPressed;
    }

    private void OnDisable()
    {
        buttonInput.OnLeft -= OnLeftPressed;
        buttonInput.OnRight -= OnRightPressed;
    }

    public void OnRightPressed()
    {
        
         if (currentPosition < 5)
        {

            dangerPos++;
            isSafe = false;

        }
        if (turtles[dangerPos].isDiving == true && isSafe == false)
        {
            gameOver();

        }

        if (currentPosition < 6 && deliveryMan.drowned == false)
        {
            currentPosition++;

            if (currentPosition == 6)
            {
                deliveryMan.SR.sprite = deliveryMan.withPackage;
                
                isSafe = true;
                deliveryMan.transform.position = playerPositions[currentPosition].position;

                deliveryMan.isCarrying = true;

                deliveryMan.changeSprite();
                
            }
            else
            {
                deliveryMan.transform.position = playerPositions[currentPosition].position;

            }
            print("current position= " + currentPosition);

        }
        print("right pressed dangerpos");
        print(dangerPos);

    }

    public void OnLeftPressed()
    {
        if (currentPosition > 1)
        {

            if (currentPosition != 6)
            {
                dangerPos--;

            }
            isSafe = false;
            print("moving left" + isSafe + " " + dangerPos);
        }
        if(dangerPos >= 0)
        {
            if (turtles[dangerPos].isDiving == true && isSafe == false)
            {
                gameOver();

            }
        }
            
        if (currentPosition > 0 && deliveryMan.drowned == false)
        {

            currentPosition--;

            if (currentPosition == 0)
            {
                
                isSafe = true;
                dangerPos = -1;

                if(deliveryMan.isCarrying == true)
                {
                    print("Delivering!!!");

                    deliveryMan.isCarrying = false;
                    deliveryMan.SR.sprite = deliveryMan.withOutPackage;
                    points++;

                    if(points > 5)
                    {
                        print("hardMode enabled");
                        seconds = 1;
                        diveSeconds = 1;
                    }
                    
                }
               

                scoreText.text = points.ToString();
                deliveryMan.transform.position = playerPositions[currentPosition].position;

            }
            else
            {

                deliveryMan.transform.position = playerPositions[currentPosition].position;

            }
            print("current position= " + currentPosition);

        }
        print("left pressed dangerpos");
        print(dangerPos);

    }

   void gameOver()
    {
        deliveryMan.SR.sprite = deliveryMan.hasDrowned;

        deliveryMan.transform.position = drownedPositions[random].position;
        deliveryMan.drowned = true;
        print("------You have drowned-------");

        StopAllCoroutines();

        gameOverText.text = "Game over!";
        gameOverScore.text = "score: " + points + "p";

        gameOverText.gameObject.SetActive(true);
        gameOverScore.gameObject.SetActive(true);


        scoreContainer.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }
  
}
