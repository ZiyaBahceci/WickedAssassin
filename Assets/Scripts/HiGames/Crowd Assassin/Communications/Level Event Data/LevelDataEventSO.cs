using UnityEngine;
using Framework.Data;
using CrowdAssassin.Data;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class LevelDataEventSO : ScriptableObject
    {
        #region Events

        public delegate CharacterDataSO CharacterDataRequested();
        public event CharacterDataRequested OnCharacterDataRequested;

        public delegate GameData GameDataRequested();
        public event GameDataRequested OnGameDataRequested;

        public delegate FloatData MoneyDataRequested();
        public event MoneyDataRequested OnMoneyDataRequested;

        #endregion Events

        #region Functions

        public CharacterDataSO RaiseOnCharacterDataRequested()
        {
            return OnCharacterDataRequested?.Invoke();
        }

        public GameData RaiseOnGameDataRequested()
		{
            return OnGameDataRequested?.Invoke();
        }

        public FloatData RaiseOnMoneyDataRequested()
        {
            return OnMoneyDataRequested?.Invoke();
        }

        #endregion Functions
    }
}