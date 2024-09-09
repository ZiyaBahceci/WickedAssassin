using UnityEngine;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class LevelOperationEventSO : ScriptableObject
    {
        #region Events

        public delegate void LevelLoadRequested();
        public event LevelLoadRequested OnLevelLoadRequested;
        public event LevelLoadRequested OnNextLevelLoadRequested;

        #endregion Events

        #region Functions

        public void RaiseOnLevelLoadRequested()
        {
            OnLevelLoadRequested?.Invoke();
        }

        public void RaiseOnNextLevelLoadRequested()
        {
            OnNextLevelLoadRequested?.Invoke();
        }

        #endregion Functions
    }
}