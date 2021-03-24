using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSFXer : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.PlayCrows(transform);

        Destroy(gameObject, 5f);
    }
}
