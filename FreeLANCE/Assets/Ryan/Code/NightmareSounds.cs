using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareSounds : MonoBehaviour
{
    [Header("FMod Settings")]
    [SerializeField] [FMODUnity.EventRef] private string m_footstepEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_GhostSoundsEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_GhostWindEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_JumpscareEventPath;

    [Header("Playback Settings")]
    [SerializeField] private float m_stepDistance = 2f;

    private float m_stepRandom;
    private Vector3 m_prevPos;
    private float m_distanceTravelled;
    private float m_timeTakenSinceStep;

    private void Start()
    {
        m_stepRandom = Random.Range(0f, .5f);
        m_prevPos = transform.position;
    }

    private void Update()
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

    private void PlaySound(string eventPath)
    {
        FMOD.Studio.EventInstance _sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_sound, transform, GetComponent<Rigidbody>());
        _sound.start();
        _sound.release();
    }
}