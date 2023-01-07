using UnityEngine;

public class Mast : MonoBehaviour
{
	private bool _enabled;

	private void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		_enabled = true;
	}

	private void OnTriggerExit(Collider other)
	{
		_enabled = false;
	}
}
