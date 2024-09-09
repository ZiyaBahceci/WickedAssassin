using UnityEngine;
using CrowdAssassin.Civilian;

namespace CrowdAssassin.Data
{
    public class CivilianControllerData
    {
        #region Variables

        private CivilianController _civilianController;

		#endregion Variables

		#region Properties

		public CivilianController CivilianController { get => _civilianController; set => _civilianController = value; }

		#endregion Properties

		#region Functions

		public void Initialize()
        {

        }

        #endregion Functions
    }
}