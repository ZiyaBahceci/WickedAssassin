using UnityEngine;
using CrowdAssassin.Data;
using CrowdAssassin.Event;
using CrowdAssassin.Civilian;
using System.Collections.Generic;

namespace CrowdAssassin.Pool
{
    public class CivilianPoolManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _civilianPrefab;

        private Stack<CivilianController> _civilianControllerStack;

        [SerializeField] private GameStateEventSO _gameStateEventSO;
        [SerializeField] private CivilianPoolEventSO _civilianPoolEventSO;

		#endregion Variables

		#region Properties

		private GameObject CivilianPrefab { get => _civilianPrefab; set => _civilianPrefab = value; }
		
        private Stack<CivilianController> CivilianControllerStack { get => _civilianControllerStack; set => _civilianControllerStack = value; }
		
        private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
		private CivilianPoolEventSO CivilianPoolEventSO { get => _civilianPoolEventSO; set => _civilianPoolEventSO = value; }

		#endregion Properties

		#region Awake

		void Awake()
        {
            Initialize();
            SubscribeEvents();
        }

        void OnDestroy()
		{
            UnSubscribeEvents();
        }

        #endregion Awake

        #region Functions

        private void Initialize()
        {
            CivilianControllerStack = new Stack<CivilianController>();
        }

        private void SubscribeEvents()
        {
            CivilianPoolEventSO.OnCivilianControllerDataRequested += OnCivilianRequested;
            CivilianPoolEventSO.OnCivilianControllerSentBack += OnCivilianSentBack;
        }

        private void UnSubscribeEvents()
        {
            CivilianPoolEventSO.OnCivilianControllerDataRequested -= OnCivilianRequested;
            CivilianPoolEventSO.OnCivilianControllerSentBack -= OnCivilianSentBack;
        }

        private void OnCivilianRequested(CivilianControllerData civilianControllerData)
		{
            civilianControllerData.CivilianController = GetCivilianController();
        }

        private void OnCivilianSentBack(CivilianController civilianController)
        {
			if (civilianController)
			{
                CivilianControllerStack.Push(civilianController);
            }
        }

        private CivilianController GetCivilianController()
		{
			if (CivilianControllerStack.Count > 0)
			{
                return CivilianControllerStack.Pop();
			}
			else
			{
                return Instantiate(CivilianPrefab, transform, true).GetComponent<CivilianController>();
			}
		}

        #endregion Functions
    }
}