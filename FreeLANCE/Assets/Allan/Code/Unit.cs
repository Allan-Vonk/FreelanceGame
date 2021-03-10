using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 1;
    public float checkRadius = .5f;
    public Queue<Vector3> path;
    public Transform Target
    {
        get { return target; }
        set { SetTarget(value); }
    }
    private Transform target;
    private Pathfinding pathfinding;
    private void Start ()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
    }
    public void SetTarget (Transform value)
    {
        Debug.Log("Setting target and starting pathfinding");
        target = value;
        if (target != null)
        {
            path = pathfinding.FindPath(transform.position, target.position);
        }
    }
    private void FixedUpdate ()
    {
        if (path != null && path.Count > 0)
        {
            if (Vector3.Distance(transform.position, path.Peek()) > checkRadius) MoveToNextPosition();
            else path.Dequeue();
        }
        if (path != null && path.Count == 0)
        {
            StopPath();
        }
    }
    private void MoveToNextPosition ()
    {
        Vector3 dir =  (path.Peek() - transform.position).normalized;
        transform.position += dir * speed;
    }
    private void StopPath ()
    {
        path = null;
        target = null;
    }
    private void OnDrawGizmos ()
    {
        if (path != null&&path.Count > 0)
        {
            foreach (Vector3 vector3 in path)
            {
                Gizmos.color = (vector3 == path.Peek()) ? Color.blue : Color.red;
                Gizmos.DrawWireSphere(vector3 + new Vector3(0,1,0),.1f);
            }
        }
    }
}
