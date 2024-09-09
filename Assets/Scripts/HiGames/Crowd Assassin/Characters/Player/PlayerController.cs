using System;
using System.Collections;
using UnityEngine;
using Framework.Enums;
using CrowdAssassin.Data;
using CrowdAssassin.Event;
using CrowdAssassin.Character;
using CrowdAssassin.Civilian;
using Framework.Data;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace Framework.Player
{
    public class PlayerController : BaseCharacterController, IStateEvent
    {
        #region Variables

        [SerializeField] private InputEventSO _inputEventSO;
        [SerializeField] private GameStateEventSO _gameStateEventSO;

        [SerializeField] private PlayerCanvasController _playerCanvasController;

        [SerializeField] private GameObject _trajectoryPointPrefab;

        [SerializeField] private GameObject[] _trajectoryPoints;

        [SerializeField] private int _trajectoryDotsCount;

        #endregion Variables

        #region Properties

        private InputEventSO InputEventSO
        {
            get => _inputEventSO;
            set => _inputEventSO = value;
        }

        public GameStateEventSO GameStateEventSO
        {
            get => _gameStateEventSO;
            set => _gameStateEventSO = value;
        }

        private PlayerCanvasController PlayerCanvasController
        {
            get => _playerCanvasController;
            set => _playerCanvasController = value;
        }

        #endregion Properties

        #region Functions

        public override void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody>();
            GetHealth();
            PlayerCanvasController.SetHealth(Health);
            _gameData = LevelDataEventSO.RaiseOnGameDataRequested();
            SetCharacterYPosition();
            _trajectoryPoints = new GameObject[_trajectoryDotsCount];
        }
        

        private GameData _gameData;

        protected override void SetCharacterYPosition()
        {
            if (_gameData.levelText <= 5)
            {
                MinZPosition = -5;
                MaxZPosition = -5;
            }
            else
            {
                MinZPosition = -6;
                MaxZPosition = 6;
            }

            float zPosition = Random.Range(MinZPosition, MaxZPosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
        }

        private void Update()
        {
            if (_gameStateEventSO.CurrentGameState == GameState.Game && _gameData.levelText <= 3)
            {
                Vector3Data enemyPosition = new Vector3Data();
                PlayerEnemyEventSO.RaiseOnEnemyPositionRequested(enemyPosition);
                Vector3 direction = enemyPosition.Position - GunGameObject.transform.position;

                for (int i = 0; i < _trajectoryPoints.Length; i++)
                {
                    _trajectoryPoints[i].transform.position = TrajectoryPointPosition(i * 0.1f, direction);
                }
            }
        }

        private Vector3 TrajectoryPointPosition(float time, Vector3 direction)
        {
            Vector3 currentPointPos = GunGameObject.transform.position + (direction.normalized * (10f * time));
            currentPointPos.y = 1f;
            return currentPointPos;
        }

        public override void SubscribeEvents()
        {
            base.SubscribeEvents();

            GameStateEventSO.OnStateEnter += SubscribeStateEnter;
            GameStateEventSO.OnStateExit += SubscribeStateExit;
        }

        public override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();

            GameStateEventSO.OnStateEnter -= SubscribeStateEnter;
            GameStateEventSO.OnStateExit -= SubscribeStateExit;
        }

        private void GetHealth()
        {
            CharacterDataSO characterDataSO = LevelDataEventSO.RaiseOnCharacterDataRequested();

            if (characterDataSO)
                Health = characterDataSO.PlayerHP;
        }


        protected override void OnHit()
        {
            base.OnHit();

            CheckDeath();
            PlayerCanvasController.UpdateHealth(Health);
            PlayerCanvasController.ActivateHitImagePoolItem();
        }

        protected override void Fire(Vector3 direction)
        {
            base.Fire(direction);
        }

        private void TriggerFire()
        {
            Vector3Data enemyPosition = new Vector3Data();
            PlayerEnemyEventSO.RaiseOnEnemyPositionRequested(enemyPosition);

            Vector3 direction = enemyPosition.Position - GunGameObject.transform.position;
            Fire(direction);
        }

        private void OnCharacterPositionRequested(Vector3Data vector3Data)
        {
            vector3Data.Position = transform.position;
        }

        private void OnCivilianHit()
        {
            CheckDeath();
            PlayerCanvasController.UpdateHealth(Health);
            PlayerCanvasController.ActivateCivilianHitImagePoolItem();
        }

        private void OnCivilianHit(CivilianController civilianController, BulletOwner bulletOwner,
            Vector3 bulletDirection)
        {
            if (bulletOwner == BulletOwner.Player)
            {
                OnCivilianHit();
            }
        }

        public void SubscribeStateEnter(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {
            }
            else if (gameState == GameState.Game)
            {
                if (_gameData.levelText <= 3)
                {
                    for (int i = 0; i < _trajectoryDotsCount; i++)
                    {
                        _trajectoryPoints[i] =
                            Instantiate(_trajectoryPointPrefab, transform.position, quaternion.identity);
                    }
                }

                HitEventSO.OnPlayerHit += OnHit;
                InputEventSO.OnTapped += TriggerFire;
                HitEventSO.OnCivilianHit += OnCivilianHit;
                PlayerEnemyEventSO.OnPlayerPositionRequested += OnCharacterPositionRequested;
            }
        }

        public void SubscribeStateExit(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {
            }
            else if (gameState == GameState.Game)
            {
                HitEventSO.OnPlayerHit -= OnHit;
                InputEventSO.OnTapped -= TriggerFire;
                HitEventSO.OnCivilianHit -= OnCivilianHit;
                PlayerEnemyEventSO.OnPlayerPositionRequested -= OnCharacterPositionRequested;

                UnSubscribeEvents();
            }
        }

        #endregion Functions
    }
}