using CrowdAssassin.Character;

namespace CrowdAssassin.Enemy
{
	public class EnemyAnimationController : BaseAnimationController
	{
		#region Variables

		

		#endregion Variables

		#region Properties

		

		#endregion Properties

		#region Functions

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void SubscribeEvents()
		{
			base.SubscribeEvents();

			PlayerEnemyEventSO.OnCharacterDied += OnCharacterDied;
		}

		public override void UnSubscribeEvents()
		{
			base.UnSubscribeEvents();
		}

		private void OnCharacterDied(BaseCharacterController characterController)
		{
			if (characterController is EnemyController)
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