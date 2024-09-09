using UnityEngine;
using DG.Tweening;
using Framework.Enums;
using CrowdAssassin.Event;

namespace CrowdAssassin.World
{
    public class TrainMovement : MonoBehaviour, IStateEvent
    {
        #region Variables

        private bool _isCountDownActive;

        [Range(-1,1)]
        [SerializeField] private int _direction;

        private float _timer;
        [SerializeField] private float _duration;
        [SerializeField] private float _minTimeInterval;
        [SerializeField] private float _maxTimeInterval;

        private Vector3 _initialPosition;

        [SerializeField] private GameObject _trainGameObject;

        [SerializeField] private GameStateEventSO _gameStateEventSO;

		#endregion Variables

		#region Properties

		private bool IsCountDownActive { get => _isCountDownActive; set => _isCountDownActive = value; }
		
        private int Direction { get => _direction; set => _direction = value; }
		
        private float Timer { get => _timer; set => _timer = value; }
		private float Duration { get => _duration; set => _duration = value; }
        private float MinTimeInterval { get => _minTimeInterval; set => _minTimeInterval = value; }
		private float MaxTimeInterval { get => _maxTimeInterval; set => _maxTimeInterval = value; }
		
		private Vector3 InitialTrainPosition { get => _initialPosition; set => _initialPosition = value; }
        
        private GameObject TrainGameObject { get => _trainGameObject; set => _trainGameObject = value; }
		
        private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }

		#endregion Properties

		#region Awake - Start - Update - FixedUpdate

		void Awake()
        {
            Initialize();
        }

        void Start()
        {

        }

        void Update()
        {
            CountDown();
        }

        void FixedUpdate()
        {

        }

        #endregion Awake - Start - Update - FixedUpdate

        #region Functions

        public void Initialize()
        {
            IsCountDownActive = true;
            InitialTrainPosition = TrainGameObject.transform.position;

            SetTimer();
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

        public void SubscribeStateEnter(GameState gameState)
		{
            if (gameState == GameState.Menu)
            {
                ActivateCountDown();
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
                
            }
        }

        private void ActivateCountDown()
		{
            IsCountDownActive = true;
            TrainGameObject.transform.position = InitialTrainPosition;
        }

        private void DeactivateCountDown()
		{
            IsCountDownActive = false;

            SetTimer();
        }

        private void CountDown()
		{
			if (IsCountDownActive)
			{
                Timer -= Time.deltaTime;

                if (Timer <= 0)
                {
                    OnCountDownFinished();
                }
            }
		}

        private void OnCountDownFinished()
		{
            DeactivateCountDown();

            TrainGameObject.transform.DOMoveZ(100f * Direction, Duration).SetEase(Ease.Linear).OnComplete(()=>
            {
                ActivateCountDown();
            });
		}

        private void SetTimer()
		{
            Timer = Random.Range(MinTimeInterval, MaxTimeInterval);
        }

        #endregion Functions
    }
}