using UnityEngine;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class GameStateCanvasEventSO : ScriptableObject
    {
        #region Events

        public delegate void GameFinished();
        public event GameFinished OnGameWin;
        public event GameFinished OnGameLose;
        public event GameFinished OnGameStart;

        #endregion Events

        #region Functions

        public void RaiseOnGameStart()
        {
            OnGameStart?.Invoke();
        }
        public void RaiseOnGameWin()
        {
            OnGameWin?.Invoke();
        }

        public void RaiseOnGameLose()
        {
            OnGameLose?.Invoke();
        }

        #endregion Functions
    }
}