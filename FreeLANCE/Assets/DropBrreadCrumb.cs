using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBrreadCrumb : MonoBehaviour
{
    public KeyCode dropKey = KeyCode.Space;
    public GameObject crumbPrefab;
    public Transform dropPoint;
    private void Update ()
    {
        if (Input.GetKeyDown(dropKey))
        {
            DropCrumb(crumbPrefab,dropPoint);
        }
    }
    private void DropCrumb (GameObject prefab, Transform dropPoint)
    {
        Debug.Log("Dropping");
        Instantiate(prefab, dropPoint.position,Quaternion.identity);
        SoundManager.instance.PlayDropBreadcrumbs(transform);
    }
}
