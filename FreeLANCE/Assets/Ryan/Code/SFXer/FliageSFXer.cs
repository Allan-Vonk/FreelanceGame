using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliageSFXer : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.PlayFoliage(transform);

        Destroy(gameObject, 5.5f);
    }
}
