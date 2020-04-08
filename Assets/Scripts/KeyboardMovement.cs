using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------
    // Public variables
    //-----------------------------------------------------------------------------------------------------------------
    public CharacterController controller;

    //-----------------------------------------------------------------------------------------------------------------
    // Serialized fields
    //-----------------------------------------------------------------------------------------------------------------
    [Header("Dependencies")]
    [SerializeField] private Transform _groundCheckGameObject;
    [SerializeField] private LayerMask _groundLayer;
    [Header("Settings")]
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _groundDistance = 0.4f;

    //-----------------------------------------------------------------------------------------------------------------
    // Non-serialized fields
    //-----------------------------------------------------------------------------------------------------------------
    private Vector3 _velocity;
    private bool _isGrounded;

    //-----------------------------------------------------------------------------------------------------------------
    // Unity events
    //-----------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(Time.deltaTime * _speed * move);

        // Jump
        CheckIfCharacterIsGrounded();
        if (_isGrounded && _velocity.y < 0) _velocity.y = -2f;
        if (Input.GetButtonDown("Jump") && _isGrounded) _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        _velocity.y += _gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    //-----------------------------------------------------------------------------------------------------------------
    // Private methods
    //-----------------------------------------------------------------------------------------------------------------
    private bool CheckIfCharacterIsGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckGameObject.position, _groundDistance, _groundLayer);
        return _isGrounded;
    }
}