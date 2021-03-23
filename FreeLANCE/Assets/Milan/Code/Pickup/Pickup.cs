using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private int value = 5;

    private void Awake()
    {
        ScoreManager.instance.SaveAndLoadPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        PickingUp();
    }

    public void PickingUp()
    {
        ScoreManager.pickedUp = true;
        ScoreManager.instance.score += value;

        ScoreManager.instance.RefreshUI();
        ScoreManager.instance.SaveAndLoadPlayer();

        SoundManager.instance.PlayCollectItem();

        gameObject.SetActive(false);
    }
}
