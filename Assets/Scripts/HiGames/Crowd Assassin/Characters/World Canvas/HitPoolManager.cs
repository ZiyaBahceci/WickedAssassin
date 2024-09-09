using UnityEngine;
using Framework.Enums;
using CrowdAssassin.Data;
using CrowdAssassin.Event;
using System.Collections.Generic;

namespace CrowdAssassin.Character
{
    public class HitPoolManager : MonoBehaviour, IStateEvent
    {
        #region Variables

        [SerializeField] private GameObject _hitImagePrefab;
        [SerializeField] private GameObject _civilianHitImagePrefab;

        private Stack<HitImageController> _hitImageControllerStack;
        private Stack<HitCivilianImageController> _civilianHitImageControllerStack;

        [SerializeField] private GameStateEventSO _gameStateEventSO;
        [SerializeField] private HitImagePoolEventSO _hitImagePoolEventSO;

        #endregion Variables

        #region Properties

        private GameObject HitImagePrefab { get => _hitImagePrefab; set => _hitImagePrefab = value; }
		private GameObject CivilianHitImagePrefab { get => _civilianHitImagePrefab; set => _civilianHitImagePrefab = value; }

        private Stack<HitImageController> HitImageControllerStack { get => _hitImageControllerStack; set => _hitImageControllerStack = value; }
        private Stack<HitCivilianImageController> CivilianHitImageControllerStack { get => _civilianHitImageControllerStack; set => _civilianHitImageControllerStack = value; }

        private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
        private HitImagePoolEventSO HitImagePoolEventSO { get => _hitImagePoolEventSO; set => _hitImagePoolEventSO = value; }

		#endregion Properties

		#region Awake - Start - Update - OnDestroy

		void Awake()
        {
            Initialize();
            SubscribeEvents();
        }

        void OnDestroy()
        {
            UnSubscribeEvents();
        }

        #endregion Awake - Start - Update - OnDestroy

        #region Functions

        private void Initialize()
        {
            HitImageControllerStack = new Stack<HitImageController>();
            CivilianHitImageControllerStack = new Stack<HitCivilianImageController>();
        }

        private void SubscribeEvents()
        {
            GameStateEventSO.OnStateEnter += SubscribeStateEnter;
            GameStateEventSO.OnStateExit += SubscribeStateExit;
        }

        private void UnSubscribeEvents()
        {
            GameStateEventSO.OnStateEnter -= SubscribeStateEnter;
            GameStateEventSO.OnStateExit -= SubscribeStateExit;
        }

		public void SubscribeStateEnter(GameState gameState)
		{
            if (gameState == GameState.Menu)
            {
            
            }
            else if (gameState == GameState.Game)
            {
                HitImagePoolEventSO.OnHitImageControllerDataRequested += OnHitImageControllerDataRequested;
                HitImagePoolEventSO.OnHitImageControllerSentBack += OnHitImageControllerSentBack;
                
                HitImagePoolEventSO.OnCivilianHitImageControllerDataRequested += OnCivilianHitImageControllerDataRequested;
                HitImagePoolEventSO.OnCivilianHitImageControllerSentBack += OnCivilianHitImageControllerSentBack;
            }
		}

		public void SubscribeStateExit(GameState gameState)
		{
			if (gameState == GameState.Menu)
            {
            
            }
            else if (gameState == GameState.Game)
            {
                HitImagePoolEventSO.OnHitImageControllerDataRequested -= OnHitImageControllerDataRequested;
                HitImagePoolEventSO.OnHitImageControllerSentBack -= OnHitImageControllerSentBack;

                HitImagePoolEventSO.OnCivilianHitImageControllerDataRequested -= OnCivilianHitImageControllerDataRequested;
                HitImagePoolEventSO.OnCivilianHitImageControllerSentBack -= OnCivilianHitImageControllerSentBack;
            }
		}

		private void OnHitImageControllerDataRequested(HitImageControllerData hitImageControllerData)
		{
            hitImageControllerData.HitImageController = GetHitImageController();
        }

		private void OnHitImageControllerSentBack(HitImageController hitImageController)
		{
			if (hitImageController)
			{
                hitImageController.DeactivateImageController(transform);
                HitImageControllerStack.Push(hitImageController);
			}
		}

        private void OnCivilianHitImageControllerDataRequested(HitCivilianImageControllerData hitCivilianImageControllerData)
        {
            hitCivilianImageControllerData.HitCivilianImageController = GetCivilianHitImageController();
        }

        private void OnCivilianHitImageControllerSentBack(HitCivilianImageController hitCivilianImageController)
        {
            if (hitCivilianImageController)
            {
                hitCivilianImageController.DeactivateImageController(transform);
                CivilianHitImageControllerStack.Push(hitCivilianImageController);
            }
        }

        private HitImageController GetHitImageController()
        {
            if (HitImageControllerStack.Count > 0)
            {
                return HitImageControllerStack.Pop();
            }
            else
            {
                return Instantiate(HitImagePrefab, transform, true).GetComponent<HitImageController>();
            }
        }

        private HitCivilianImageController GetCivilianHitImageController()
        {
            if (CivilianHitImageControllerStack.Count > 0)
            {
                return CivilianHitImageControllerStack.Pop();
            }
            else
            {
                return Instantiate(CivilianHitImagePrefab, transform, true).GetComponent<HitCivilianImageController>();
            }
        }

        #endregion Functions
    }
}