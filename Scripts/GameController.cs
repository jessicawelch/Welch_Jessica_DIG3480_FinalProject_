using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class GameController : MonoBehaviour
{
    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text restartText;
    public Text gameOverText;
    public Text ScoreText;
    public Text winText;
    public Text hardModeText;
    

    public bool gameOver;
    private bool restart;
    private bool hardMode;
    
    public int points;

    public BGScroller BGScroll;
    public ParticleSystem StarField1;
    public ParticleSystem StarField2;

    public AudioMixerSnapshot Background;
    public AudioMixerSnapshot Win;
    
    public float bpm = 128;

    private float m_TransitionIn;
    private float m_QuarterNote;


    void Start()
    {
        gameOver = false;
        restart = false;
        hardMode = false;
    
        winText.text = "";
        restartText.text = "";
        gameOverText.text = "";
        hardModeText.text = "";
        

        points = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());

        m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
       
    }

    private void Update()
    {
        hardModeText.text = "Press 'H' for Hard Mode";
        

        if (restart)
        {
            if(Input.GetKeyDown (KeyCode.P))
            {
                SceneManager.LoadScene("SampleScene");
                Background.TransitionTo(m_TransitionIn);
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();


        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("HardMode");
            Background.TransitionTo(m_TransitionIn);
        }
                
      

    }




    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'P' for Play Again";
                restart = true;
                break;
                
            }
        }
    
    }
    public void AddScore(int newScoreValue)
    {
        points += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + points;

        if (points >= 100)
        {
            winText.text = "Game Created by Jessica Welch";
            BGScroll.scrollSpeed = -20;

            ParticleSystem.MainModule psMain1 = StarField1.main;
            ParticleSystem.MainModule psMain2 = StarField2.main;

            psMain1.simulationSpeed = +100;
            psMain2.simulationSpeed = +100;

           Win.TransitionTo(m_TransitionIn);

            gameOver = true;
            restart = true;

        }
    }
    public void GameOver ()
    {
        gameOverText.text = "Game Over";
        gameOver = true;

    }

    
}
