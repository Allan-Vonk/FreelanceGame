using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMareMovement : MonoBehaviour
{
    public GameObject Target;
    private Unit unit;
    public Transform lastKnowPos;
    public bool CanSeeTarget
    {
        get { return canSeeTarget; }
        set 
        {   
            if (canSeeTarget == true && value == false)
            {
                lastKnowPos = Target.transform;
                unit.Target = lastKnowPos;
            }
            canSeeTarget = value;
        }
    }
    private bool canSeeTarget;
    private void Start ()
    {
        unit = GetComponent<Unit>();
    }
    private void Update ()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,(Target.transform.position - transform.position),out hit))
        {
            if (hit.transform.gameObject.CompareTag(Target.tag))
            {
                CanSeeTarget = true;
            }
            else
            {
                CanSeeTarget = false;
            }
        }

    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = (canSeeTarget) ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position, Target.transform.position);
    }
}
