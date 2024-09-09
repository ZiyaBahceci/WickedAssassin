using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class InputEventSO : ScriptableObject
    {
        #region Events

        public delegate void Tap();
        public event Tap OnTapped;

        #endregion Events

        #region Variables



        #endregion Variables

        #region Properties



        #endregion Properties


        #region Functions

        public void RaiseOnTapped()
        {
            OnTapped?.Invoke();
        }

        #endregion Functions
    }
}