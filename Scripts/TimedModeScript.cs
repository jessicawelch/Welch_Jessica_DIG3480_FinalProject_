using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimedModeScript : MonoBehaviour
{
    public float targetTime = 10.0f;
    private GameController gameController;
    private DestroyByContact destroyByContact;

    public AudioMixerSnapshot Lose;
    private float m_TransitionIn;
    private float m_QuarterNote;
    public float bpm = 128;

    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        Instantiate(destroyByContact.playerExplosion, transform.position, transform.rotation);
        gameController.GameOver();
        GetComponent<AudioSource>().Play();
        Lose.TransitionTo(m_TransitionIn);

    }

}
