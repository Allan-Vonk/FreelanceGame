using UnityEngine;

public class FPLook : MonoBehaviour
{
    public float MouseSensitivity;

    public Transform Player;

    [SerializeField][Range(-100, 0)] private float _minRotation = -90f;
    [SerializeField][Range(0, 100)] private float _maxRotation = 90f;
    
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _minRotation, _maxRotation);


        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        Player.Rotate(Vector3.up * mouseX);
    }
}