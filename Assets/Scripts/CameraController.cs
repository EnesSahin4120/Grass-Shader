using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField] private Transform _target;
	[SerializeField] private float _distance;
	[SerializeField] private float _height;
	[SerializeField] private Vector3 _lookOffset;

	private float _cameraSpeed = 1000f;
	private float _rotSpeed = 100f;

	private void LateUpdate() 
	{
		Follow();
	}

	private void Follow()
	{
		Vector3 lookPosition = _target.position + _lookOffset;
		Vector3 relativePos = lookPosition - transform.position;
		Quaternion rot = Quaternion.LookRotation(relativePos);

		transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * _rotSpeed * 0.1f);

		Vector3 targetPos = _target.transform.position + _target.transform.up * _height - _target.transform.forward * _distance;

		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _cameraSpeed * 0.1f);
	}
}