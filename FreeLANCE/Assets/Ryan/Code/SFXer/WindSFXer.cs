using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSFXer : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.PlayWind(transform);

        Destroy(gameObject, 15f);
    }
}
