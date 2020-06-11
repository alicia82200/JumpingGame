using UnityEngine;
using System.Collections;

public class CAApplyForceOnTarget : CustomActionScript {

	public ForceMode _forceMode = ForceMode.VelocityChange;

	public float _force = 30f;

	public Transform _transformReference;

	public override IEnumerator DoActionOnEvent (MonoBehaviour sender, GameObject args)
	{
		var bumpedRigidbody = args.GetComponent<Rigidbody>();
		if (bumpedRigidbody != null && !bumpedRigidbody.isKinematic) 
		{
			yield return new WaitForFixedUpdate ();
			bumpedRigidbody.AddForce (_transformReference.rotation * Vector3.forward * _force, _forceMode);
		}
		yield return null;
	}
}
