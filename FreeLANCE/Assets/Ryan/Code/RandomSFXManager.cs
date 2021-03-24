using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFXManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;

    [Header("Sound Objects")]
    [SerializeField] private GameObject m_CrowObject;
    [SerializeField] private GameObject m_CrowFlyObject;
    [SerializeField] private GameObject m_FoliageObject;
    [SerializeField] private GameObject m_WindObject;

    [Header("Timers")]
    [SerializeField] private float m_CrowMinTime;
    [SerializeField] private float m_CrowMaxTime;
    [SerializeField] private float m_CrowFlyMinTime;
    [SerializeField] private float m_CrowFlyMaxTime;
    [SerializeField] private float m_FoliageMinTime;
    [SerializeField] private float m_FoliageMaxTime;
    [SerializeField] private float m_WindMinTime;
    [SerializeField] private float m_WindMaxTime;

    private float m_crowTimer;
    private float m_crowFlyTimer;
    private float m_foliageTimer;
    private float m_windTimer;

    private void Start()
    {
        m_crowTimer = 5f;
        m_crowFlyTimer = 27f;
        m_foliageTimer = 60f;
        m_windTimer = 1f;
    }

    private void Update()
    {
        CrowTimer();
        CrowFlyTimer();
        FoliageTimer();
        WindTimer();
    }

    private void CrowTimer()
    {
        m_crowTimer -= Time.deltaTime;
        if (m_crowTimer <= 0)
        {
            Instantiate(m_CrowObject, m_Player.transform.position + new Vector3(0, 2, 2), Quaternion.identity);

            m_crowTimer = Random.Range(m_CrowMinTime, m_CrowMaxTime);
        }
    }

    private void CrowFlyTimer()
    {
        m_crowFlyTimer -= Time.deltaTime;
        if (m_crowFlyTimer <= 0)
        {
            Instantiate(m_CrowFlyObject, m_Player.transform.position + new Vector3(0, 2, 2), Quaternion.identity);

            m_crowFlyTimer = Random.Range(m_CrowMinTime, m_CrowMaxTime);
        }
    }

    private void FoliageTimer()
    {
        m_foliageTimer -= Time.deltaTime;
        if (m_foliageTimer <= 0)
        {
            Instantiate(m_FoliageObject, m_Player.transform.position + new Vector3(0, 0, 2), Quaternion.identity);

            m_foliageTimer = Random.Range(m_CrowMinTime, m_CrowMaxTime);
        }
    }

    private void WindTimer()
    {
        m_windTimer -= Time.deltaTime;
        if (m_windTimer <= 0)
        {
            Instantiate(m_WindObject, m_Player.transform.position + new Vector3(0, 2, 0), Quaternion.identity);

            m_windTimer = Random.Range(m_CrowMinTime, m_CrowMaxTime);
        }
    }
}
