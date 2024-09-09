using UnityEngine;
using Framework.Enums;
using Framework.Player;
using CrowdAssassin.Event;
using CrowdAssassin.Enemy;

namespace CrowdAssassin.Character
{
    public class BaseAnimationController : MonoBehaviour, IStateEvent
    {
        #region Variables

        private int _walkHash;
        private int _fireHash;
        private int _finalHash;
        private int _deathHash;

        private Animator _animator;

        [SerializeField] private GameStateEventSO _gameStateEventSO;
        [SerializeField] private PlayerEnemyEventSO _playerEnemyEventSO;

        #endregion Variables

        #region Properties

        protected int WalkHash { get => _walkHash; set => _walkHash = value; }
		protected int FireHash { get => _fireHash; set => _fireHash = value; }
        protected int FinalHash { get => _finalHash; set => _finalHash = value; }
		protected int DeathHash { get => _deathHash; set => _deathHash = value; }

        protected Animator Animator { get => _animator; set => _animator = value; }

        private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
        protected PlayerEnemyEventSO PlayerEnemyEventSO { get => _playerEnemyEventSO; set => _playerEnemyEventSO = value; }

		#endregion Properties

		#region Functions

		public virtual void Initialize()
        {
            WalkHash = Animator.StringToHash("Walk");
            FireHash = Animator.StringToHash("Fire");
            FinalHash = Animator.StringToHash("Final");
            DeathHash = Animator.StringToHash("Death");

            Animator = GetComponent<Animator>();
        }

        public virtual void SubscribeEvents()
        {
            GameStateEventSO.OnStateEnter += SubscribeStateEnter;
            GameStateEventSO.OnStateExit += SubscribeStateExit;
        }

        public virtual void UnSubscribeEvents()
        {
            GameStateEventSO.OnStateEnter -= SubscribeStateEnter;
            GameStateEventSO.OnStateExit -= SubscribeStateExit;
        }

        protected void ActivateWalkAnimation()
		{
            SetAnimationBool(Animator, WalkHash, true);
		}

        public void TriggerFireAnimation()
		{
            TriggerAnimation(Animator, FireHash);
		}

        protected void ActivateFinalAnimation()
		{
            TriggerAnimation(Animator, FinalHash);
		}

        protected void ActivateDeathAnimation()
		{
            TriggerAnimation(Animator, DeathHash);
        }

        protected void SetAnimationBool(Animator animator, int hash, bool value)
        {
            if (animator)
            {
                animator.SetBool(hash, value);
            }
        }

        protected void TriggerAnimation(Animator animator, int triggerHash)
		{
			if (animator)
			{
                animator.SetTrigger(triggerHash);
			}
		}

        public void SubscribeStateEnter(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {

            }
            else if (gameState == GameState.Game)
            {
                ActivateWalkAnimation();
            }
        }

        public void SubscribeStateExit(GameState gameState)
        {
            if (gameState == GameState.Menu)
            {

            }
            else if (gameState == GameState.Game)
            {
                
            }
        }

        #endregion Functions
    }
}