using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using CrowdAssassin.Event;
using Framework.Enums;

namespace CrowdAssassin.World
{
    public class EnvironmentLoopMovement : MonoBehaviour, IStateEvent
    {
        #region Variables

        [SerializeField] private float _duration;
        [SerializeField] private float _endYValue;
        
        [SerializeField] private GameStateEventSO _gameStateEventSO;

		#endregion Variables

		#region Properties

		private float Duration { get => _duration; set => _duration = value; }
		private float EndYValue { get => _endYValue; set => _endYValue = value; }
		
        private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }

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

        public void Initialize()
        {

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

            }
            else if (gameState == GameState.Game)
            {
                ActivateMovementLoop();
            }
        }

        public void SubscribeStateExit(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {

            }
            else if (gameState == GameState.Game)
            {
                DeactivateMovementLoop();
            }
        }

        private void ActivateMovementLoop()
		{
            KillDotween();

            transform.DOMoveZ(EndYValue, Duration).SetLoops(-1).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed);
		}

        private void DeactivateMovementLoop()
		{
            KillDotween();
        }

        private void KillDotween()
		{
            if (DOTween.IsTweening(transform))
                DOTween.Kill(transform);
        }

        #endregion Functions
    }
}