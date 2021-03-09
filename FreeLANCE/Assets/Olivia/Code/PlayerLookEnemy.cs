using UnityEngine;

public class PlayerLookEnemy : MonoBehaviour
{
    void Update()
    {
      
    }



    // RaycastHit hit;
       // Ray ray = Camera.current.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

       // if (Physics.Raycast(ray, out hit))
       // {
       //     Debug.Log("I have been rayed");
       //     if (hit.transform.CompareTag("Enemy")) EnemyInSight();
        //} 
    }

    public void EnemyInSight()
    {
        Debug.Log("I have been spotted");
    }
}