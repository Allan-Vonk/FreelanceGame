using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private int value = 5;

    private void OnTriggerEnter(Collider other)
    {
        PickingUp();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickingUp();
            ScoreManager.RefreshUI();
        }
    }

    public void PickingUp()
    {
        ScoreManager.score += value;
        ScoreManager.pickedUp = true;
        gameObject.SetActive(false);
    }
}
