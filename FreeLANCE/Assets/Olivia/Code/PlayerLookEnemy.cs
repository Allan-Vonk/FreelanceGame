using UnityEngine;

public class PlayerLookEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Enemy")) EnemyInSight();
        }
    }

    public void EnemyInSight()
    {
        Debug.Log("I have been spotted");
    }
}