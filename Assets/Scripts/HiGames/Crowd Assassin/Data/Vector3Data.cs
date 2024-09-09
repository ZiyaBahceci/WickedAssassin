using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CrowdAssassin.Data
{
    public class Vector3Data
    {
        #region Variables

        private Vector3 _position;

		#endregion Variables

		#region Properties

		public Vector3 Position { get => _position; set => _position = value; }

		#endregion Properties
    }
}