using UnityEngine;

public class FPMovement : MonoBehaviour
{
    public float Speed;
    [SerializeField] private Rigidbody _playerRB;

    // Start is called before the first frame update
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //vector * float is niet efficient daarom eerst (speed * deltatime) dan hoeft het maar 1 keer.
        movement = movement.normalized * (Speed * Time.deltaTime) ;
      

        _playerRB.AddRelativeForce(movement, ForceMode.Impulse);
    }
}