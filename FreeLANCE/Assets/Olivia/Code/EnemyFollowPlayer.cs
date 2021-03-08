using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform PlayerPos;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxDist;
    [SerializeField] private float _minDist;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerPos.position) <= _maxDist)
        {
            if (Vector3.Distance(transform.position, PlayerPos.position) >= _minDist)
            {
                transform.position += transform.forward * (_movementSpeed * Time.deltaTime);
                transform.LookAt(PlayerPos);
            }
        }
    }
}