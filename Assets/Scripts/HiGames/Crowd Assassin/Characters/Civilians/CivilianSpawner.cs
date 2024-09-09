using UnityEngine;
using CrowdAssassin.Data;
using CrowdAssassin.Event;
using System.Collections.Generic;
using Framework.Enums;

namespace CrowdAssassin.Civilian
{
    public class CivilianSpawner : MonoBehaviour, IStateEvent
    {
        #region Variables

        private bool _isCountDownActive;

        [SerializeField] private int _initialCivilianSpawnCount;

        private float _spawnInterval;
        [SerializeField] private float _minSpawnInterval;
        [SerializeField] private float _maxSpawnInterval;

        [SerializeField] private List<CivilianInitialTargetPosition> _initialPositionList;

        [SerializeField] private GameStateEventSO _gameStateEventSO;
        [SerializeField] private CivilianPoolEventSO _civilianPoolEventSO;

		#endregion Variables

		#region Properties

		private bool IsCountDownActive { get => _isCountDownActive; set => _isCountDownActive = value; }
		
		private int InitialCivilianSpawnCount { get => _initialCivilianSpawnCount; set => _initialCivilianSpawnCount = value; }
        
        private float SpawnInterval { get => _spawnInterval; set => _spawnInterval = value; }
		private float MinSpawnInterval { get => _minSpawnInterval; set => _minSpawnInterval = value; }
		private float MaxSpawnInterval { get => _maxSpawnInterval; set => _maxSpawnInterval = value; }
		
		private List<CivilianInitialTargetPosition> InitialPositionList { get => _initialPositionList; set => _initialPositionList = value; }
        
		private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
        private CivilianPoolEventSO CivilianPoolEventSO { get => _civilianPoolEventSO; set => _civilianPoolEventSO = value; }

		#endregion Properties

		#region Awake - Start - Update - FixedUpdate

		void Awake()
        {
            Initialize();
            SubscribeEvents();
        }

        void Start()
        {
            InitialCivilianSpawn();
        }

        void Update()
        {
            CountDown();
        }

        void OnDestroy()
        {
            UnSubscribeEvents();
        }

        #endregion Awake - Start - Update - FixedUpdate

        #region Functions

        public void Initialize()
        {
            IsCountDownActive = false;
            SetSpawnInterval();
        }

        public void SubscribeEvents()
        {
            GameStateEventSO.OnStateEnter += SubscribeStateEnter;
            GameStateEventSO.OnStateExit += SubscribeStateExit;
        }

        public void UnSubscribeEvents()
        {
            GameStateEventSO.OnStateEnter -= SubscribeStateEnter;
            GameStateEventSO.OnStateExit -= SubscribeStateExit;
        }

        private void InitialCivilianSpawn()
		{
			for (int i = 0; i < InitialCivilianSpawnCount; i++)
			{
                SpawnCivilian(true);
            }
		}

        private float GetRandom(float x, float y)
		{
            return Random.Range(x, y);
		}

        private void SetSpawnInterval()
		{
            SpawnInterval = Random.Range(MinSpawnInterval, MaxSpawnInterval);
		}

        private void SpawnCivilian(bool isRandom)
        {
            CivilianControllerData civilianControllerData = new CivilianControllerData();
            CivilianPoolEventSO.RaiseOnCivilianControllerDataRequested(civilianControllerData);

            int index = Random.Range(0, InitialPositionList.Count);

            Vector3 initialPosition = InitialPositionList[index].InitialPosition;
            Vector3 targetPosition = InitialPositionList[index].TargetPosition;

            if (isRandom)
                civilianControllerData.CivilianController.ActivateCivilian(new Vector3(GetRandom(initialPosition.x, targetPosition.x), initialPosition.y, GetRandom(initialPosition.z, targetPosition.z)), targetPosition);
            else
                civilianControllerData.CivilianController.ActivateCivilian(initialPosition, targetPosition);
        }

        private void CountDown()
		{
			if (IsCountDownActive)
			{
                SpawnInterval -= Time.deltaTime;

                if (SpawnInterval <= 0)
                {
                    SetSpawnInterval();
                    SpawnCivilian(false);
                }
            }
		}

        public void SubscribeStateEnter(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {
                IsCountDownActive = true;
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
                IsCountDownActive = false;
            }
        }

        #endregion Functions
    }
}