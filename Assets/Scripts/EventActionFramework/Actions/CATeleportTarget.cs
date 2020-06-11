using UnityEngine;
using System.Collections;

public class CATeleportTarget : CustomActionScript {

	public Transform _teleportTransform;

	public bool _applyRotation = true;

	public bool _resetVelocityOfRigidBodies = false;

	public bool _immediate = false;

	private int deathNumber = 0;

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		if (_teleportTransform != null)
			Debug.DrawLine (this.transform.position, _teleportTransform.position, Color.blue);
	}

	public override IEnumerator DoActionOnEvent (MonoBehaviour sender, GameObject args)
	{
		if (!_immediate)
			yield return new WaitForFixedUpdate();
		args.transform.position = _teleportTransform.position;
		Debug.Log("Death");
		if (_applyRotation)
			args.transform.rotation = _teleportTransform.rotation;
		var rb = args.GetComponent<Rigidbody>();

		if (rb != null && !rb.isKinematic && _resetVelocityOfRigidBodies)
			rb.velocity = Vector3.zero;

		deathNumber++;

		yield return null;
	}

	public int numDeath()
    {
		return deathNumber;
    }
}
