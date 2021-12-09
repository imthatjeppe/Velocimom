using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public GameObject dropOffZone;


    private Score scoreRef;
    private NextLevelScript nextLevelScript;

    private void Start()
    {
        scoreRef = dropOffZone.GetComponent<Score>();
        nextLevelScript = dropOffZone.GetComponent<NextLevelScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //timer.isTimeTicking = false;

            if (scoreRef.score > nextLevelScript.scoreForNextLevel)
            {
                //Öppna meny med alternativ för att fortsätta tills nästa nivå eller fortsätta spela på nuvarande
                nextLevelScript.ContinueToNextLevel();
            }

            if (scoreRef.score < nextLevelScript.scoreForNextLevel)
            {
                //Informera spelaren om att dom inte har tillräckligt med poäng för att fortsätta
            }
        }
    }
}
