using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------
    // Serialized fields
    //-----------------------------------------------------------------------------------------------------------------
    [SerializeField] private CharacterController _player;
    [SerializeField] private Transform _receiverCollider;

    //-----------------------------------------------------------------------------------------------------------------
    // Non-serialized fields
    //-----------------------------------------------------------------------------------------------------------------
    private bool _playerIsOverlapping = false;
    private const string PlayerTag = "Player";
    
    //-----------------------------------------------------------------------------------------------------------------
    // Unity events
    //-----------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        if (_playerIsOverlapping)
        {
            Vector3 portalToPlayer = _player.transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, _receiverCollider.rotation);
                rotationDiff += 180;
                _player.transform.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                _player.enabled = false;
                _player.transform.position = _receiverCollider.position + positionOffset;
                _player.enabled = true;

                _playerIsOverlapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(PlayerTag)) 
        {
            _playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag(PlayerTag))
        {
            _playerIsOverlapping = false;
        }
    }
}
