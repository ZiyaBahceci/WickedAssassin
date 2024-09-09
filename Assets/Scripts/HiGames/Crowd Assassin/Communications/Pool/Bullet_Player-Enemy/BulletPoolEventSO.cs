using UnityEngine;
using CrowdAssassin.Data;
using System.Collections.Generic;

namespace CrowdAssassin.Pool
{
    [CreateAssetMenu]
    public class BulletPoolEventSO : ScriptableObject
    {
        #region Events

        public delegate void BulletDataRequest(BulletData bulletData);
        public event BulletDataRequest OnBulletDataRequested;
        public event BulletDataRequest OnBulletDataSentBack;

        #endregion Events

        #region Variables



		#endregion Variables

		#region Properties


		#endregion Properties

		#region Functions

		public void RaiseOnBulletDataSentBack(BulletData bulletData)
        {
            OnBulletDataSentBack.Invoke(bulletData);
        }

        public void RaiseOnBulletDataRequested(BulletData bulletData)
        {
            OnBulletDataRequested.Invoke(bulletData);
        }

        #endregion Functions
    }
}