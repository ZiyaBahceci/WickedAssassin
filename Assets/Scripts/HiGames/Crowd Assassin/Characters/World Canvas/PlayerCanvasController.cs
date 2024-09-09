namespace CrowdAssassin.Character
{
    public class PlayerCanvasController : BaseCharacterCanvasController
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

            PlayerEnemyEventSO.OnCharacterDied -= OnCharacterDied;
        }

        private void OnCharacterDied(BaseCharacterController characterController)
		{
            DeactivateCharacterCanvas();
        }

        #endregion Functions
    }
}