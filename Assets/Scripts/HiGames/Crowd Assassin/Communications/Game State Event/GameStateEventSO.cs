using UnityEngine;
using Framework.Enums;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class GameStateEventSO : ScriptableObject
    {
        #region Events

        public delegate void StateEvent(GameState gameState);
        public event StateEvent OnStateEnter;
        public event StateEvent OnStateExit;

        #endregion Events

        #region Variables

        private GameState _currentGameState = GameState.None;

		#endregion Variables

		#region Properties

		public GameState CurrentGameState { get => _currentGameState; set => _currentGameState = value; }

		#endregion Properties

		#region Functions

        public void ChangeState(GameState targetGameState)
        {
            if (CurrentGameState == targetGameState)
			{
                Debug.Log("Already in " + CurrentGameState);
                return;
			}

            OnStateExit?.Invoke(CurrentGameState);
            CurrentGameState = targetGameState;
            OnStateEnter?.Invoke(CurrentGameState);
        }

        public void Reset()
		{
            CurrentGameState = GameState.None;
        }

        public void RaiseEnter(GameState gameState)
		{
            OnStateEnter?.Invoke(gameState);
		}

        #endregion Functions
    }
}