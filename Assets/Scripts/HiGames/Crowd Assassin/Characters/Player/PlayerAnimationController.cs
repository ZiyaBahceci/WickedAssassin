using UnityEngine;
using CrowdAssassin.Event;
using CrowdAssassin.Character;

namespace Framework.Player
{
    public class PlayerAnimationController : BaseAnimationController
	{
		#region Variables

		[SerializeField] private InputEventSO _inputEventSO;

		#endregion Variables

		#region Properties

		private InputEventSO InputEventSO { get => _inputEventSO; set => _inputEventSO = value; }

		#endregion Properties

		#region Functions

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void SubscribeEvents()
		{
			base.SubscribeEvents();

			InputEventSO.OnTapped += TriggerFireAnimation;
			PlayerEnemyEventSO.OnCharacterDied += OnCharacterDied;
		}

		public override void UnSubscribeEvents()
		{
			base.UnSubscribeEvents();

			InputEventSO.OnTapped -= TriggerFireAnimation;
			PlayerEnemyEventSO.OnCharacterDied -= OnCharacterDied;
		}

		private void OnCharacterDied(BaseCharacterController characterController)
		{
			if (characterController is PlayerController)
			{
				ActivateDeathAnimation();
			}
			else
			{
				ActivateFinalAnimation();
			}
		}

		#endregion Functions
	}
}