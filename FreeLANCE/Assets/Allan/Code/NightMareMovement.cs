using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMareMovement : MonoBehaviour
{
    public GameObject Target;
    public float speed;
    public Unit unit;
    public Vector3 lastKnowPos;
    public bool CanSeeTarget
    {
        get { return canSeeTarget; }
        set 
        {   
            if (canSeeTarget == true && value == false)
            {
                lastKnowPos = Target.transform.position;
                unit.Target = lastKnowPos;
            }
            canSeeTarget = value;
        }
    }
    private bool canSeeTarget;
    private Grid grid;
    private Pathfinding pf;

    public List<Node> possibleWanderTargets;
    private void Start ()
    {
        pf = FindObjectOfType<Pathfinding>();
        grid = FindObjectOfType<Grid>();
        unit = GetComponent<Unit>();
        Invoke("initialize", 2f);
    }
    void initialize ()
    {
        possibleWanderTargets = GatherPossibleWanderTargets();
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
        Light FlashLight = Target.GetComponentInChildren<Camera>().gameObject.GetComponentInChildren<Light>();
        if (canSeeTarget && FlashLight.enabled == true)
        {
            unit.StopPath();
            Vector3 direction = Target.transform.position - transform.position;
            transform.position += direction.normalized * speed;
            transform.LookAt(Target.transform.position);
        }
        if (canSeeTarget == false && unit.Target == Vector3.zero || canSeeTarget && FlashLight.enabled == false&& unit.Target == Vector3.zero)
        {
            if (possibleWanderTargets != null)
            {
                unit.Target = possibleWanderTargets[Random.Range(0, possibleWanderTargets.Count)].worldPosition;
            }
        }
    }
    private List<Node> GatherPossibleWanderTargets ()
    {
        List<Node> nodes = new List<Node>();

        foreach (Node node in grid.grid)
        {
            if (node.walkable)
            {
                if (pf.FindPath(transform.position,node.worldPosition) != null)
                {
                    nodes.Add(node);
                }
            }
        }
        return nodes;
    }
    private void OnDrawGizmos ()
    {
        Gizmos.color = (canSeeTarget) ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position, Target.transform.position);
    }
}
