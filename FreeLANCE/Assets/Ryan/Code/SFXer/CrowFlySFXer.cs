using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFlySFXer : MonoBehaviour
{
    [SerializeField] private float m_FlySpeed;

    private void Start()
    {
        SoundManager.instance.PlayCrowsFly(transform);

        Destroy(gameObject, 2.5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * m_FlySpeed * Time.deltaTime);
    }
}
