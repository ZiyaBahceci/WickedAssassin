using System;
using UnityEngine;
using Framework.Extension;
using CrowdAssassin.Event;
using Framework.Enums;

namespace CrowdAssassin.Civilian
{
    public class CivilianController : MonoBehaviour, IStateEvent
    {
        #region Variables

        private bool _isDead;

        [SerializeField] private ParticleSystem _bloodParticle;
        [SerializeField] private float _speed;
        [SerializeField] private float _deactivateTime;

        private Collider _collider;
        private Rigidbody _rigidbody;

        private CivilianAnimationController _civilianAnimationController;

        [SerializeField] private GameStateEventSO _gameStateEventSO;
        [SerializeField] private CivilianPoolEventSO _civilianPoolEventSO;

        #endregion Variables

        #region Properties

		private bool IsDead { get => _isDead; set => _isDead = value; }
		
        private float Speed { get => _speed; set => _speed = value; }
		private float DeactivateTime { get => _deactivateTime; set => _deactivateTime = value; }
		
		private Collider Collider { get => _collider; set => _collider = value; }
        private Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
		
		private CivilianAnimationController CivilianAnimationController { get => _civilianAnimationController; set => _civilianAnimationController = value; }
        
		private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
        private CivilianPoolEventSO CivilianPoolEventSO { get => _civilianPoolEventSO; set => _civilianPoolEventSO = value; }

		#endregion Properties

		#region Awake - OnDestroy

		void Awake()
        {
            Initialize();
            SubscribeEvents();
        }

		void OnDestroy()
		{
            UnSubscribeEvents();
        }

		#endregion Awake - OnDestroy

		#region Functions

		public void Initialize()
        {
            IsDead = false;

            Collider = GetComponent<Collider>();
            Rigidbody = GetComponent<Rigidbody>();

            CivilianAnimationController = GetComponent<CivilianAnimationController>();
            CivilianAnimationController.Initialize();
        }

        public void SubscribeEvents()
        {
            GameStateEventSO.OnStateEnter += SubscribeStateEnter;
            GameStateEventSO.OnStateExit += SubscribeStateExit;
        }

        public void UnSubscribeEvents()
        {
            GameStateEventSO.OnStateEnter -= SubscribeStateEnter;
            GameStateEventSO.OnStateExit -= SubscribeStateExit;
        }

        private void StopDeadCivilian()
		{
			if (IsDead)
			{
                Rigidbody.velocity = Vector3.zero;
            }
		}

		public void ActivateCivilian(Vector3 position, Vector3 targetPosition)
		{
            IsDead = false;

            transform.position = position;
            transform.rotation = Quaternion.LookRotation(targetPosition);
            Rigidbody.velocity = (targetPosition - position).normalized * Speed;
            Collider.enabled = true;

            gameObject.SetActive(true);
            ((Action)DeactivateCivilianController).AddDelay(DeactivateTime);
		}

        private void DeactivateCivilianController()
		{
            Rigidbody.velocity = Vector3.zero;
            CivilianPoolEventSO.RaiseOnCivilianControllerSentBack(this);
            CivilianAnimationController.ResetAnimator();

            gameObject.SetActive(false);
		}

        public void OnCivilianHit(Vector3 bulletDirection)
		{
            IsDead = true;
           
            Rigidbody.velocity = Vector3.back * 3.75f; // 3.75f is the speed of platform. Did not get it from the environmentLoopController. Access it if it's needed.
            Collider.enabled = false;
            _bloodParticle.transform.position = transform.position;
            _bloodParticle.Play();
            bulletDirection = new Vector3(-bulletDirection.x, 0, 0);
            transform.rotation = Quaternion.LookRotation(bulletDirection);

            CivilianAnimationController.OnCivilianHit();
        }

		public void SubscribeStateEnter(GameState gameState)
		{
            if (gameState == GameState.Menu)
            {

            }
            else if (gameState == GameState.Game)
            {

            }
        }

		public void SubscribeStateExit(GameState gameState)
		{
            if (gameState == GameState.Menu)
            {

            }
            else if (gameState == GameState.Game)
            {
                StopDeadCivilian();
            }
        }

		#endregion Functions
	}
}