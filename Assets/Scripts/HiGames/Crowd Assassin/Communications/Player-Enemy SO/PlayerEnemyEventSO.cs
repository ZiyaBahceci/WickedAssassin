using UnityEngine;
using CrowdAssassin.Data;
using CrowdAssassin.Character;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class PlayerEnemyEventSO : ScriptableObject
    {
        #region Events

        public delegate void OpponentPositionRequest(Vector3Data vector3Data);
        public event OpponentPositionRequest OnEnemyPositionRequested;
        public event OpponentPositionRequest OnPlayerPositionRequested;

        public delegate void CharacterDied(BaseCharacterController characterController);
        public event CharacterDied OnCharacterDied;

        #endregion Events

        #region Functions

        public void RaiseOnEnemyPositionRequested(Vector3Data vector3Data)
        {
            OnEnemyPositionRequested?.Invoke(vector3Data);
        }
        
        public void RaiseOnPlayerPositionRequested(Vector3Data vector3Data)
        {
            OnPlayerPositionRequested?.Invoke(vector3Data);
        }

        public void RaiseOnCharacterDied(BaseCharacterController characterController)
		{
            OnCharacterDied?.Invoke(characterController);
        }

        #endregion Functions
    }
}