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
            if (scoreRef.score > nextLevelScript.scoreForNextLevel)
            {
                //�ppna meny med alternativ f�r att forts�tta tills n�sta niv� eller forts�tta spela p� nuvarande
                nextLevelScript.ContinueToNextLevel();
            }

            if (scoreRef.score < nextLevelScript.scoreForNextLevel)
            {
                //Informera spelaren om att dom inte har tillr�ckligt med po�ng f�r att forts�tta
                Debug.Log("Lose");
            }
        }
    }
}
