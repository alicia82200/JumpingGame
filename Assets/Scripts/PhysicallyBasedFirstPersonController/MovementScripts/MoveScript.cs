using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
	
	[SerializeField]
	private Transform _transform;

	[SerializeField]
	private Rigidbody _rigidbody;

	[SerializeField]
	private float _moveForce = 0;

	[SerializeField]
	private float _geometricDrag = 0;

	[SerializeField]
	private float _linearDrag = 0;

	[SerializeField]
	private float _maxVelocity = 0;

	public GameObject player;

	private Vector3 lastPos = new Vector3(0,0,0);

	void Start () {
		if (_rigidbody == null)
			_rigidbody = this.GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		
		var direction = (Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal")).normalized;
		direction = direction.normalized;
		
		var speeddelta =  _transform.rotation 
			*  (direction
			    *_moveForce);
		var speed = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

		_rigidbody.AddForce((speed - speed.normalized * _linearDrag) * _geometricDrag - speed, ForceMode.VelocityChange);

		speed -= speed.normalized * _linearDrag;
		speed *= _geometricDrag;
	
			if (speed.magnitude > _maxVelocity)
			{
				_rigidbody.AddForce(Vector3.ClampMagnitude((speed + speeddelta * Time.deltaTime), speed.magnitude) - speed, ForceMode.VelocityChange);
			}
			else
			{
				_rigidbody.AddForce(Vector3.ClampMagnitude((speed + speeddelta * Time.deltaTime), _maxVelocity) - speed, ForceMode.VelocityChange);
			}
		
		if ((_transform.position != lastPos))
		{
			player.GetComponent<Animator>().SetBool("Run", true);
		}
		else
		{
			player.GetComponent<Animator>().SetBool("Run", false);
		}

		lastPos = _transform.position;
	}
}
