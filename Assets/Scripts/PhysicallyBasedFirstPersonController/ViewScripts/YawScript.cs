using UnityEngine;
using System.Collections;

public class YawScript : MonoBehaviour {
	
	public Transform _transform;
	
	[SerializeField]
	private float _yawSpeed = 0;

	void Start () {
		if (_transform == null)
			_transform = this.transform;
	}

	void FixedUpdate () {
		if (Input.GetMouseButton(1))
		{
			Debug.Log("Right Mouse Button Down");
			_transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * _yawSpeed, Space.Self);
		}
	}
}
