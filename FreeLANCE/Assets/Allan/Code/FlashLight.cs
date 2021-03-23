using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashLight : MonoBehaviour
{
    Light lightcomp;
    private void Start ()
    {
        lightcomp = GetComponent<Light>();
    }
    private void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lightcomp.enabled = !lightcomp.enabled;
        }
    }
}
