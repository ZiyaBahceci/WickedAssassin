using UnityEngine;
using Framework.Enums;
using Framework.Player;
using CrowdAssassin.Event;
using CrowdAssassin.Character;

namespace Framework.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameStateEventSO _gameStateEventSO;
        [SerializeField] private PlayerEnemyEventSO _playerEnemyEventSO;
        [SerializeField] private GameStateCanvasEventSO _gameStateCanvasEventSO;

		#endregion Variables

		#region Properties

        private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
		private PlayerEnemyEventSO PlayerEnemyEventSO { get => _playerEnemyEventSO; set => _playerEnemyEventSO = value; }
		private GameStateCanvasEventSO GameStateCanvasEventSO { get => _gameStateCanvasEventSO; set => _gameStateCanvasEventSO = value; }

		#endregion Properties

		#region Awake - Start - OnDestroy

		void Awake()
		{
            Initialize();
            SubscribeEvents();
        }

		void Start()
        {
            GameStateEventSO.ChangeState(GameState.Menu);
        }

		void OnDestroy()
		{
            UnSubscribeEvents();
        }

		#endregion Awake - Start - OnDestroy

		#region Functions

		private void Initialize()
        {
            GameStateEventSO.Reset();
        }

        private void SubscribeEvents()
		{
            PlayerEnemyEventSO.OnCharacterDied += OnCharacterDied;
        }

        private void UnSubscribeEvents()
		{
            PlayerEnemyEventSO.OnCharacterDied -= OnCharacterDied;
        }

        private void OnCharacterDied(BaseCharacterController characterController)
		{
			if (characterController is PlayerController)
			{
                GameStateCanvasEventSO.RaiseOnGameLose();
            }
			else
			{
                GameStateCanvasEventSO.RaiseOnGameWin();
            }

            GameStateEventSO.ChangeState(GameState.Menu);
		}

		#endregion Functions
	}
}