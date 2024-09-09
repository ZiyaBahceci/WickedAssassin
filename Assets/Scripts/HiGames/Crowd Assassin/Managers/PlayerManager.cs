using UnityEngine;
using Framework.Player;
using CrowdAssassin.Player;
using CrowdAssassin.Character;

namespace Framework.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Events



        #endregion Events

        #region Variables

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerArmController _playerArmController;
        [SerializeField] private PlayerCanvasController _playerCanvasController;
        [SerializeField] private PlayerAnimationController _playerAnimationController;

        #endregion Variables

        #region Properties

		private PlayerController PlayerController { get => _playerController; set => _playerController = value; }
		private PlayerArmController PlayerArmController { get => _playerArmController; set => _playerArmController = value; }
		private PlayerCanvasController PlayerCanvasController { get => _playerCanvasController; set => _playerCanvasController = value; }
		private PlayerAnimationController PlayerAnimationController { get => _playerAnimationController; set => _playerAnimationController = value; }

		#endregion Properties

		#region Awake - OnDestroy

		void Awake()
		{
            Initialize();
            SubscribeEvents();
        }
		
        void OnDestroy()
		{
            UnSubscribeEvents();
        }

		#endregion Awake - OnDestroy

		#region Functions

		private void Initialize()
        {
            PlayerController.Initialize();
            PlayerArmController.Initialize();
            PlayerCanvasController.Initialize();
            PlayerAnimationController.Initialize();
        }

        private void SubscribeEvents()
        {
            PlayerController.SubscribeEvents();
            PlayerArmController.SubscribeEvents();
            PlayerCanvasController.SubscribeEvents();
            PlayerAnimationController.SubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            PlayerController.UnSubscribeEvents();
            PlayerArmController.UnSubscribeEvents();
            PlayerCanvasController.UnSubscribeEvents();
            PlayerAnimationController.UnSubscribeEvents();
        }

		#endregion Functions
	}
}