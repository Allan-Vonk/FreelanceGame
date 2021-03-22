using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("FMod Settings")]
    [SerializeField] [FMODUnity.EventRef] private string m_crowsEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_crowsFlyEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_FoliageEventPath;
    [SerializeField] [FMODUnity.EventRef] private string m_windEventPath;

    [Header("Playback Settings")]
    [SerializeField] private GameObject m_player;

    private float m_crowTimer;
    private float m_crowFlyTimer;
    private float m_foliageTimer;
    private float m_windTimer;

    private void Update()
    {
        
    }

    private void PlaySound(string eventPath, Transform eventLocation)
    {
        FMOD.Studio.EventInstance _sound = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_sound, eventLocation, GetComponent<Rigidbody>());
        _sound.start();
        _sound.release();
    }
}
