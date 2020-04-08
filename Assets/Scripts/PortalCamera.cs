using UnityEngine;

public class PortalCamera : MonoBehaviour
{
	//-----------------------------------------------------------------------------------------------------------------
	// Serialized fields
	//-----------------------------------------------------------------------------------------------------------------
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _portal;
    [SerializeField] private Transform _otherPortal;
    
    //-----------------------------------------------------------------------------------------------------------------
    // Unity events
    //-----------------------------------------------------------------------------------------------------------------
    private void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = _playerCamera.position - _otherPortal.position;
		transform.position = _portal.position + playerOffsetFromPortal;

		float angularDifferenceBetweenPortalRotations = Quaternion.Angle(_portal.rotation, _otherPortal.rotation);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = portalRotationalDifference * _playerCamera.forward;
		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
