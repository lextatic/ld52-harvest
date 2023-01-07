using UnityEngine;

namespace Modulo18
{
	public class AttackCooldown : AttackMachineState
	{
		private float _cooldownTimer;

		public AttackCooldown(AttackAnimator attackAnimator, Transform player)
			: base(attackAnimator, player)
		{
			Name = AttackState.Cooldown;
		}

		public override void Enter()
		{
			_cooldownTimer = 1f;
			base.Enter();
		}

		public override void Update()
		{
			_cooldownTimer -= Time.deltaTime;

			if (_cooldownTimer <= 0)
			{
				NextState = new AttackIdle(AttackAnimator, Player);
				Stage = Event.Exit;
			}
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
