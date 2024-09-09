using UnityEngine;
using Framework.Data;
using Framework.Enums;
using CrowdAssassin.Data;
using CrowdAssassin.Event;
using Framework.Extension;

namespace Framework.Managers
{
	public class MoneyManager : MonoBehaviour, IStateEvent
	{
		#region Variables

		private float _moneyAmount;
		private float _currentLevelMoneyAmount;

		private GameData _gameData;

		[SerializeField] private MoneyEventSO _moneyEventSO;
		[SerializeField] private GameStateEventSO _gameStateEventSO;
		[SerializeField] private LevelDataEventSO _levelDataEventSO;

		#endregion Variables

		#region Properties
		public float MoneyAmount { get => _moneyAmount; set => _moneyAmount = value; }
		public float CurrentLevelMoneyAmount { get => _currentLevelMoneyAmount; set => _currentLevelMoneyAmount = value; }
		
		private GameData GameData { get => _gameData; set => _gameData = value; }
		
		private MoneyEventSO MoneyEventSO { get => _moneyEventSO; set => _moneyEventSO = value; }
		private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
		private LevelDataEventSO LevelDataEventSO { get => _levelDataEventSO; set => _levelDataEventSO = value; }

		#endregion Properties

		#region Awake

		void Awake()
		{
			Initialize();
			SubscribeEvents();
		}
		
		void OnDestroy()
		{
			UnSubscribeEvents();
		}

		#endregion Awake

		#region Functions

		private void Initialize()
        {
			GetLevelData();
			CurrentLevelMoneyAmount = 0;
		}

		private void SubscribeEvents()
		{
			LevelDataEventSO.OnMoneyDataRequested += OnGameDataRequested;

			GameStateEventSO.OnStateEnter += SubscribeStateEnter;
			GameStateEventSO.OnStateExit += SubscribeStateExit;
		}

		private void UnSubscribeEvents()
		{
			LevelDataEventSO.OnMoneyDataRequested -= OnGameDataRequested;

			GameStateEventSO.OnStateEnter -= SubscribeStateEnter;
			GameStateEventSO.OnStateExit -= SubscribeStateExit;
		}

		private FloatData OnGameDataRequested()
		{
			FloatData floatData = new FloatData();
			floatData.Value = GameData.money;

			return floatData;
		}

		private void GetLevelData()
		{
			GameData = LevelDataEventSO.RaiseOnGameDataRequested();
			if (GameData != null)
				MoneyAmount = GameData.money;
		}

		public void ResetCurrentCoin() => CurrentLevelMoneyAmount = 0;

		public void SubscribeStateEnter(GameState gameState)
		{
			if (gameState == GameState.Menu)
			{

			}
			else if (gameState == GameState.Game)
			{
				
			}
		}

		public void SubscribeStateExit(GameState gameState)
		{
			if (gameState == GameState.Menu)
			{

			}
			else if (gameState == GameState.Game)
			{
				MoneyAmount += 100f;
				MoneyEventSO.RaiseOnMoneyUpdated(MoneyAmount);
			}
		}

		#endregion Functions
	}
}