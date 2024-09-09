using UnityEngine;
using CrowdAssassin.Event;
using Framework.Enums;

namespace Framework.Managers
{
    public class InputManager : MonoBehaviour, IStateEvent
    {
        #region Variables

        private Touch _touch;

        [SerializeField] private InputEventSO _inputEventSO;
        [SerializeField] private GameStateEventSO _gameStateEventSO;

		#endregion Variables

		#region Properties

		private Touch Touch { get => _touch; set => _touch = value; }
		
        private InputEventSO InputEventSO { get => _inputEventSO; set => _inputEventSO = value; }
		private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }

		#endregion Properties

		#region Update

		void Awake()
		{
            Initialize();
            SubscribeEvents();
        }

		void Update()
        {
            DetectTaps();
        }

		void OnDestroy()
		{
            UnSubscribeEvents();
        }

		#endregion Update

		#region Functions

		private void Initialize()
        {
            Touch = new Touch();
            Input.multiTouchEnabled = false;
        }

        private void SubscribeEvents()
		{
            GameStateEventSO.OnStateEnter += SubscribeStateEnter;
            GameStateEventSO.OnStateExit += SubscribeStateExit;
        }

        private void UnSubscribeEvents()
        {
            GameStateEventSO.OnStateEnter -= SubscribeStateEnter;
            GameStateEventSO.OnStateExit -= SubscribeStateExit;
        }

        private void DetectTaps()
        {
			if (Touch.tapCount > 0 || Input.GetMouseButtonDown(0))
			{
                InputEventSO.RaiseOnTapped();
            }
        }

        private void ActivateInput()
		{
            enabled = true;
		}

        private void DeactivateInput()
        {
            enabled = false;
        }

		public void SubscribeStateEnter(GameState gameState)
		{
			if (gameState == GameState.Menu)
			{
                DeactivateInput();

            }
			else if (gameState == GameState.Game)
			{
                ActivateInput();
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

		#endregion Functions
	}
}