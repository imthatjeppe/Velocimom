using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Score : MonoBehaviour
{
    public float score;
    public Text totalScore;

    private Vector2 scoreStartPos;
    private Color32 greyColor = new Color32(50, 50, 50, 255);
    private Color32 greenColor = new Color32(0, 255, 0, 255);
    AudioSource audioSource;

    private void Start()
    {
        scoreStartPos = totalScore.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        totalScore.text = "Score: " + score;
    }
    
    public void AddScore(float points)
    {
        score += points;
    }

    public void ScoreVFX()
    {
        totalScore.DOColor(greenColor, 1f);
        totalScore.transform.DOShakePosition(1f, 2.5f, 10, 45f, true, false);
        totalScore.transform.DOPunchScale(new Vector3(0f, 0.3f, 0f), 1f, 10, 0f);
        audioSource.Play();
        Invoke(nameof(resetScoreVFX), 1.25f);
    }

    public void resetScoreVFX()
    {
        totalScore.DOColor(greyColor, 1f);
        totalScore.transform.position = scoreStartPos;
    }
}