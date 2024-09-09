using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CrowdAssassin.Event;
using Framework.Enums;

namespace CrowdAssassin.Civilian
{
    public class CivilianManager : MonoBehaviour
    {
        #region Events



        #endregion Events

        #region Variables

        [SerializeField] private HitEventSO _hitEventSO;

		#endregion Variables

		#region Properties

		private HitEventSO HitEventSO { get => _hitEventSO; set => _hitEventSO = value; }

		#endregion Properties

		#region Awake - Start - Update - FixedUpdate

		void Awake()
        {
            Initialize();
            SubscribeEvents();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        void FixedUpdate()
        {

        }

        #endregion Awake - Start - Update - FixedUpdate

        #region Functions

        public void Initialize()
        {

        }

        public void SubscribeEvents()
        {
            HitEventSO.OnCivilianHit += OnCivilianHit;
        }

        public void UnSubscribeEvents()
        {
            HitEventSO.OnCivilianHit -= OnCivilianHit;
        }

        private void OnCivilianHit(CivilianController civilianController, BulletOwner bulletOwner, Vector3 bulletPosition)
		{
            civilianController.OnCivilianHit(bulletPosition);
		}

        #endregion Functions
    }
}