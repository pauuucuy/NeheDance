using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;


    public int currentMultiplier;
    public int multiplerTracker;
    public int[] multiplierThresholds; 

    public Text scoreText;
    public Text multiText;

    public float totalNotes;    
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodText, perfectText, missText, rankText, finalScoreText;


    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }


    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
        else
        {
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodText.text = goodHits.ToString();
                perfectText.text = perfectHits.ToString();
                missText.text = "" + missHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F"; 

                if (percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 85)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }
                 
                    
                 rankText.text = rankVal;   

                finalScoreText.text = currentScore.ToString();


            }
        }
    }

    public void NoteHit()
    {
        //Debug.Log("A tiempo :D");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplerTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplerTracker)
            {
                multiplerTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Puntuacion: " + currentScore;
    }
    
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;
    }

    public void NoteMissed()    
    {
        Debug.Log("Miss");

        currentMultiplier = 1;
        multiplerTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missHits++; 
    }


}
