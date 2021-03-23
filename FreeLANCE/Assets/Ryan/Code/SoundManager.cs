using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("FMod Settings")]
    [SerializeField] [FMODUnity.EventRef] private string m_crowsEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_crowsFlyEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_FoliageEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_windEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_collectItemEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_dropBreadcrumbsEventPath;

    [Header("Playback Settings")]
    [SerializeField] private GameObject m_player;

    private float m_crowTimer;
    private float m_crowFlyTimer;
    private float m_foliageTimer;
    private float m_windTimer;

    private void Start()
    {
        m_crowTimer = 10f;
        m_crowFlyTimer = 11f;
        m_foliageTimer = 3f;
        m_windTimer = 1f;
    }

    private void Update()
    {
        //m_foliageTimer -= Time.deltaTime;
        //if (m_foliageTimer <= 0)
        //{
        //    PlaySound(m_FoliageEventPath, m_player.transform);
        //    m_foliageTimer = Random.Range(6f, 20f);
        //}
    }

    private void PlaySound(string eventPath, Transform eventLocation)
    {
        FMOD.Studio.EventInstance _sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_sound, eventLocation, GetComponent<Rigidbody>());
        _sound.start();
        _sound.release();
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
