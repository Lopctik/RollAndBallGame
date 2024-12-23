using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public float rotationSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.down, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
