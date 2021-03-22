using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareSounds : MonoBehaviour
{
    [Header("FMod Settings")]
    [SerializeField] [FMODUnity.EventRef] private string m_footstepEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_GhostSoundsEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_JumpscareEventPath;

    [Header("Playback Settings")]
    [SerializeField] private GameObject m_player;
    [SerializeField] private float m_stepDistance = 2f;
    [SerializeField] private float m_soundTriggerDistance;

    private float m_stepRandom;
    private Vector3 m_prevPos;
    private float m_distanceTravelled;
    private float m_timeTakenSinceStep;
    private float m_timeLeft;

    private void Start()
    {
        m_stepRandom = Random.Range(0f, .5f);
        m_prevPos = transform.position;
        m_timeLeft = Random.Range(3f, 6f);
    }

    private void Update()
    {
        StepCheck();
        DistanceCheck();
    }

    private void StepCheck()
    {
        m_timeTakenSinceStep += Time.deltaTime;
        m_distanceTravelled += (transform.position - m_prevPos).magnitude;
        if (m_distanceTravelled >= m_stepDistance + m_stepRandom)
        {
            PlaySound(m_footstepEventPath);
            m_stepRandom = Random.Range(0f, .5f);
            m_distanceTravelled = 0;
        }
        m_prevPos = transform.position;
    }

    private void DistanceCheck()
    {
        m_timeLeft -= Time.deltaTime;
        if (m_timeLeft <= 0)
        {
            if (Vector3.Distance(transform.position, m_player.transform.position) <= m_soundTriggerDistance)
            {
                PlaySound(m_GhostSoundsEventPath);
            }

            m_timeLeft = Random.Range(3f, 6f);
        }
    }

    private void PlaySound(string eventPath)
    {
        FMOD.Studio.EventInstance _sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_sound, transform, GetComponent<Rigidbody>());
        _sound.start();
        _sound.release();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "FP")
        {
            PlaySound(m_JumpscareEventPath);
        }
    }
}