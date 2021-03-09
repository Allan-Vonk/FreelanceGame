using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private void PlayFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:SFX/Player/Footsteps", transform.position);
    }
}
