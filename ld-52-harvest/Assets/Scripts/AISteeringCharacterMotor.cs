using UnityEngine;

public class AISteeringCharacterMotor : MonoBehaviour
{
	public float MoveSpeed;
	public float MaxSteeringAngle;

	public Vector3 TargetPosition;

	private CharacterController _characterController;

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		var targetDirection = TargetPosition - transform.position;
		targetDirection.y = 0;

		var rotatedDirection = Vector3.RotateTowards(transform.forward, targetDirection, MaxSteeringAngle * Mathf.Deg2Rad * Time.deltaTime, 1);

		_characterController.SimpleMove(rotatedDirection.normalized * MoveSpeed);

		transform.LookAt(transform.position + rotatedDirection.normalized);
	}
}
