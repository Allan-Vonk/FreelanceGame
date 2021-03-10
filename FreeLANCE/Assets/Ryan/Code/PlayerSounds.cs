using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [Header("FMod Settings")]
    [SerializeField] [FMODUnity.EventRef] private string m_footstepEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_collectItemEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_dropBreadcrumbsEventPath;
    [SerializeField] private string m_speedParameterName;

    [Header("Playback Settings")]
    [SerializeField] private float m_stepDistance = 2f;
    [SerializeField] private float m_startRunningTime = .3f;

    private float m_stepRandom;
    private Vector3 m_prevPos;
    private float m_distanceTravelled;
    private float m_timeTakenSinceStep;
    private int f_playerRunning;

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
            SpeedCheck();
            PlayFootstep();
            m_stepRandom = Random.Range(0f, .5f);
            m_distanceTravelled = 0;
        }
        m_prevPos = transform.position;
    }

    private void SpeedCheck()
    {
        if (m_timeTakenSinceStep < m_startRunningTime)
            f_playerRunning = 1;
        else
            f_playerRunning = 0;
        m_timeTakenSinceStep = 0f;
    }

    private void PlayFootstep()
    {
        FMOD.Studio.EventInstance _footstep = FMODUnity.RuntimeManager.CreateInstance(m_footstepEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_footstep, transform, GetComponent<Rigidbody>());
        //_footstep.setParameterByName(m_speedParameterName, f_playerRunning);
        _footstep.start();
        _footstep.release();
    }

    public void PlayCollectItem()
    {
        FMOD.Studio.EventInstance _collectItem = FMODUnity.RuntimeManager.CreateInstance(m_collectItemEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_collectItem, transform, GetComponent<Rigidbody>());
        _collectItem.start();
        _collectItem.release();
    }

    public void PlayDropBreadcrumbs()
    {
        FMOD.Studio.EventInstance _dropBreadcrumbs = FMODUnity.RuntimeManager.CreateInstance(m_dropBreadcrumbsEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_dropBreadcrumbs, transform, GetComponent<Rigidbody>());
        _dropBreadcrumbs.start();
        _dropBreadcrumbs.release();
    }
}
