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

    private void PlaySound(string eventPath, Transform eventTransform)
    {
        FMOD.Studio.EventInstance _sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_sound, eventTransform, GetComponent<Rigidbody>());
        _sound.start();
        _sound.release();
    }

    public void PlayCrows(Transform eventTransform)
    {
        PlaySound(m_crowsEventPath, eventTransform);
    }

    public void PlayCrowsFly(Transform eventTransform)
    {
        PlaySound(m_crowsFlyEventPath, eventTransform);
    }

    public void PlayFoliage(Transform eventTransform)
    {
        PlaySound(m_FoliageEventPath, eventTransform);
    }

    public void PlayWind(Transform eventTransform)
    {
        PlaySound(m_windEventPath, eventTransform);
    }

    public void PlayCollectItem(Transform eventTransform)
    {
        PlaySound(m_collectItemEventPath, eventTransform);
    }

    public void PlayDropBreadcrumbs(Transform eventTransform)
    {
        PlaySound(m_dropBreadcrumbsEventPath, eventTransform);
    }
}
