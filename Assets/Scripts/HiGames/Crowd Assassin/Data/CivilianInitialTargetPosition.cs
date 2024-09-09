using System;
using UnityEngine;

namespace CrowdAssassin.Data
{
    [Serializable]
    public class CivilianInitialTargetPosition
    {
        #region Variables

        [SerializeField] private Vector3 _initialPosition;
        [SerializeField] private Vector3 _targetPosition;

		#endregion Variables

		#region Properties

		public Vector3 InitialPosition { get => _initialPosition; set => _initialPosition = value; }
		public Vector3 TargetPosition { get => _targetPosition; set => _targetPosition = value; }

		#endregion Properties
    }
}