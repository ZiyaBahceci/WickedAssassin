using UnityEngine;
using CrowdAssassin.Data;
using System.Collections.Generic;

namespace CrowdAssassin.Pool
{
    public class BulletPoolManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _bulletGameObject;

        private Stack<GameObject> _bulletGameObjectStack;

        [SerializeField] private BulletPoolEventSO _bulletPoolEventSO;

		#endregion Variables

		#region Properties

		private GameObject BulletGameObject { get => _bulletGameObject; set => _bulletGameObject = value; }

		private Stack<GameObject> BulletGameObjectStack { get => _bulletGameObjectStack; set => _bulletGameObjectStack = value; }

		private BulletPoolEventSO BulletPoolEventSO { get => _bulletPoolEventSO; set => _bulletPoolEventSO = value; }

		#endregion Properties

		#region Awake

		void Awake()
		{
            Initialize();
            SubscribeEvents();
        }

		void OnDisable()
		{
			UnSubscribeEvents();
		}

		#endregion Awake

		#region Functions

		public void Initialize()
        {
            BulletGameObjectStack = new Stack<GameObject>();
        }

        public void SubscribeEvents()
        {
            BulletPoolEventSO.OnBulletDataSentBack += OnBulletDataSentBack;
            BulletPoolEventSO.OnBulletDataRequested += OnBulletDataRequested;
        }

        public void UnSubscribeEvents()
        {
			BulletPoolEventSO.OnBulletDataSentBack -= OnBulletDataSentBack;
            BulletPoolEventSO.OnBulletDataRequested -= OnBulletDataRequested;
		}

        private void OnBulletDataRequested(BulletData bulletData)
		{
			if (BulletGameObjectStack.Count > 0)
			{
                bulletData.BulletGameObject = BulletGameObjectStack.Pop();
			}
			else
			{
                GameObject newBulletGameObject = Instantiate(BulletGameObject, transform, true);
                bulletData.BulletGameObject = newBulletGameObject;                
			}
		}

        private void OnBulletDataSentBack(BulletData bulletData)
		{
			if (bulletData.BulletGameObject)
			{
                BulletGameObjectStack.Push(bulletData.BulletGameObject);
			}
			else
			{
				Debug.Log("BulletGameOBject could not be found! " + this);
			}
		}

        #endregion Functions
    }
}