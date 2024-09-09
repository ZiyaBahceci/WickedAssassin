using UnityEngine;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class MoneyEventSO : ScriptableObject
    {
        #region Events

        public delegate void MoneyUpdated(float moneyAmount);
        public event MoneyUpdated OnMoneyUpdated;

        #endregion Events

        #region Functions

        public void RaiseOnMoneyUpdated(float moneyAmount)
        {
            OnMoneyUpdated?.Invoke(moneyAmount);
        }

        #endregion Functions
    }
}