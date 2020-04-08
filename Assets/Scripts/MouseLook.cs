using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------
    // Serialized fields
    //-----------------------------------------------------------------------------------------------------------------
    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private Transform _playerBody;

    //-----------------------------------------------------------------------------------------------------------------
    // Non-serialized fields
    //-----------------------------------------------------------------------------------------------------------------
    private float _xRotation = 0f;
    
    //-----------------------------------------------------------------------------------------------------------------
    // Unity events
    //-----------------------------------------------------------------------------------------------------------------
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.localRotation = Quaternion.Euler(_xRotation, 0,0);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
}
