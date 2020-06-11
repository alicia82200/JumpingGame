using UnityEngine;
using System.Collections;

public class DisableOnTriggerEnterAfterSeconds : CustomActionScript
{

	public float _seconds;

	[SerializeField]
	public GameObject _targetGameObject;

	public override IEnumerator DoActionOnEvent(MonoBehaviour sender, GameObject args)
	{
		yield return new WaitForSeconds(_seconds);
		_targetGameObject.SetActive(false);
	}
}