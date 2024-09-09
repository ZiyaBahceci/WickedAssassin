using UnityEngine;
using Framework.Enums;
using CrowdAssassin.Data;
using CrowdAssassin.Event;
using CrowdAssassin.Character;
using CrowdAssassin.Civilian;
using Framework.Data;

namespace CrowdAssassin.Enemy
{
    public class EnemyController : BaseCharacterController, IStateEvent
    {
        #region Variables

        private bool _isFireActive;

        private float _fireTimeInterval;
        [SerializeField] private float _minFireTimeInterval;
        [SerializeField] private float _maxFireTimeInterval;

        [SerializeField] private GameStateEventSO _gameStateEventSO;

        [SerializeField] private EnemyCanvasController _enemyCanvasController;
        [SerializeField] private EnemyAnimationController _enemyAnimationController;

        #endregion Variables

        #region Properties

        private bool IsFireActive
        {
            get => _isFireActive;
            set => this._isFireActive = value;
        }

        private float FireTimeInterval
        {
            get => _fireTimeInterval;
            set => _fireTimeInterval = value;
        }

        private float MinFireTimeInterval
        {
            get => _minFireTimeInterval;
            set => _minFireTimeInterval = value;
        }

        private float MaxFireTimeInterval
        {
            get => _maxFireTimeInterval;
            set => _maxFireTimeInterval = value;
        }

        private GameStateEventSO GameStateEventSO
        {
            get => _gameStateEventSO;
            set => _gameStateEventSO = value;
        }

        private EnemyCanvasController EnemyCanvasController
        {
            get => _enemyCanvasController;
            set => _enemyCanvasController = value;
        }

        private EnemyAnimationController EnemyAnimationController
        {
            get => _enemyAnimationController;
            set => _enemyAnimationController = value;
        }

        #endregion Properties

        #region Update

        void Update()
        {
            FireCountDown();
        }

        #endregion Update

        #region Functions

        public override void Initialize()
        {
            base.Initialize();

            IsFireActive = false;

            GetRandomFireInterval();

            GetHealth();
            EnemyCanvasController.SetHealth(Health);
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
                Health = characterDataSO.EnemyHP;
        }

        protected override void OnHit()
        {
            base.OnHit();

            EnemyCanvasController.ActivateHitImagePoolItem();

            CheckDeath();
            EnemyCanvasController.UpdateHealth(Health);
        }

        protected override void Fire(Vector3 direction)
        {
            base.Fire(direction);
        }

        private void TriggerFire()
        {
            EnemyAnimationController.TriggerFireAnimation();

            Vector3Data playerPosition = new Vector3Data();
            PlayerEnemyEventSO.RaiseOnPlayerPositionRequested(playerPosition);

            Vector3 direction = playerPosition.Position - GunGameObject.transform.position;
            Fire(direction);
        }

        private void OnCharacterPositionRequested(Vector3Data vector3Data)
        {
            vector3Data.Position = transform.position;
        }

        private void OnCivilianHit()
        {
            CheckDeath();
            EnemyCanvasController.UpdateHealth(Health);
            EnemyCanvasController.ActivateCivilianHitImagePoolItem();
        }

        private void OnCivilianHit(CivilianController civilianController, BulletOwner bulletOwner,
            Vector3 bulletDirection)
        {
            if (bulletOwner == BulletOwner.Enemy)
            {
                if (Health > 1)
                {
                    OnCivilianHit();
                }
            }
        }

        public void SubscribeStateEnter(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {
            }
            else if (gameState == GameState.Game)
            {
                GameData gameData = LevelDataEventSO.RaiseOnGameDataRequested();
                if (gameData.levelText < 3)
                    _enemyCanvasController.SetTargetIcon();
                IsFireActive = true;
                HitEventSO.OnEnemyHit += OnHit;
                HitEventSO.OnCivilianHit += OnCivilianHit;
                PlayerEnemyEventSO.OnEnemyPositionRequested += OnCharacterPositionRequested;
            }
        }

        public void SubscribeStateExit(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {
            }
            else if (gameState == GameState.Game)
            {
                IsFireActive = false;
                HitEventSO.OnEnemyHit -= OnHit;
                HitEventSO.OnCivilianHit -= OnCivilianHit;
                PlayerEnemyEventSO.OnEnemyPositionRequested -= OnCharacterPositionRequested;
            }
        }

        #endregion Functions

        #region Fire

        private void GetRandomFireInterval()
        {
            FireTimeInterval = Random.Range(MinFireTimeInterval, MaxFireTimeInterval);
        }

        private void FireCountDown()
        {
            if (IsFireActive)
            {
                FireTimeInterval -= Time.deltaTime;

                if (FireTimeInterval <= 0)
                {
                    GetRandomFireInterval();

                    TriggerFire();
                }
            }
        }

        #endregion Fire
    }
}