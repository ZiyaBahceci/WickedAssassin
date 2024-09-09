using UnityEngine;
using CrowdAssassin.Data;
using CrowdAssassin.Character;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class HitImagePoolEventSO : ScriptableObject
    {
        #region Events

        public delegate void HitImageControllerDataRequested(HitImageControllerData hitImageControllerData);
        public event HitImageControllerDataRequested OnHitImageControllerDataRequested;
        

        public delegate void HitImageControllerRequested(HitImageController hitImageController);
        public event HitImageControllerRequested OnHitImageControllerSentBack;

        public delegate void CivilianHitImageControllerDataRequested(HitCivilianImageControllerData hitImageControllerData);
        public event CivilianHitImageControllerDataRequested OnCivilianHitImageControllerDataRequested;
        
        public delegate void CivilianHitImageControllerRequested(HitCivilianImageController hitImageController);
        public event CivilianHitImageControllerRequested OnCivilianHitImageControllerSentBack;

        #endregion Events

        #region Functions

        public void RaiseOnHitImageControllerDataRequested(HitImageControllerData hitImageControllerData)
        {
            OnHitImageControllerDataRequested?.Invoke(hitImageControllerData);
        }

        public void RaiseOnHitImageControllerSentBack(HitImageController hitImageController)
        {
            OnHitImageControllerSentBack?.Invoke(hitImageController);
        }

        public void RaiseOnCivilianHitImageControllerDataRequested(HitCivilianImageControllerData hitCivilianImageControllerData)
        {
            OnCivilianHitImageControllerDataRequested?.Invoke(hitCivilianImageControllerData);
        }

        public void RaiseOnCivilianHitImageControllerSentBack(HitCivilianImageController hitCivilianImageController)
        {
            OnCivilianHitImageControllerSentBack?.Invoke(hitCivilianImageController);
        }

        #endregion Functions
    }
}