using UnityEngine;

namespace CrowdAssassin.Data
{
    public class BulletData
    {
        #region Variables

        private GameObject _bulletGameObject;

		#endregion Variables

		#region Properties

		public GameObject BulletGameObject { get => _bulletGameObject; set => _bulletGameObject = value; }

		#endregion Properties

		#region Functions

		public BulletData(GameObject gameObject)
        {
            BulletGameObject = gameObject;
        }

        #endregion Functions
    }
}