using UnityEngine;
using CrowdAssassin.Data;
using CrowdAssassin.Civilian;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class CivilianPoolEventSO : ScriptableObject
    {
        #region Events

        public delegate void CivilianControllerDataRequested(CivilianControllerData civilianControllerData);
        public event CivilianControllerDataRequested OnCivilianControllerDataRequested;

        public delegate void CivilianControllerRequested(CivilianController civilianController);
        public event CivilianControllerRequested OnCivilianControllerSentBack;

        #endregion Events

        #region Functions

        public void RaiseOnCivilianControllerDataRequested(CivilianControllerData civilianControllerData)
        {
            OnCivilianControllerDataRequested?.Invoke(civilianControllerData);
        }

        public void RaiseOnCivilianControllerSentBack(CivilianController civilianController)
        {
            OnCivilianControllerSentBack?.Invoke(civilianController);
        }

        #endregion Functions
    }
}