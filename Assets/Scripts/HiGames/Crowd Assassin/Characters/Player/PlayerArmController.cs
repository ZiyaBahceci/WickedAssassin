using UnityEngine;
using DG.Tweening;
using Framework.Enums;
using CrowdAssassin.Data;
using CrowdAssassin.Character;

namespace CrowdAssassin.Player
{
    public class PlayerArmController : BaseArmController, IStateEvent
    {
        #region Variables

        private float _ikValue;

        [SerializeField] private GameObject _leftArmGameObject;

        #endregion Variables

        #region Properties

		private float IkValue { get => _ikValue; set => _ikValue = value; }
        
        private GameObject LeftArmGameObject { get => _leftArmGameObject; set => _leftArmGameObject = value; }

		#endregion Properties

		#region Awake - LateUpdate

		void Awake()
        {
            SetInitialRotation();
        }

		void OnAnimatorIK(int layerIndex)
		{
            Animator.SetIKPosition(AvatarIKGoal.LeftHand, GetOpponentPosition());
            Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, IkValue);
		}

		#endregion Awake - LateUpdate

		#region Functions

		public override void Initialize()
        {
            base.Initialize();

            IkValue = 0f;
        }

        public override void SubscribeEvents()
        {
            base.SubscribeEvents();

            GameStateEventSO.OnStateEnter += SubscribeStateEnter;
            GameStateEventSO.OnStateExit += SubscribeStateExit;
        }

        public override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();

            GameStateEventSO.OnStateEnter -= SubscribeStateEnter;
            GameStateEventSO.OnStateExit -= SubscribeStateExit;
        }

        private void SetInitialRotation()
        {
            InitialQuaternion = LeftArmGameObject.transform.rotation;
        }

        private Vector3 GetOpponentPosition()
		{
            Vector3Data enemyData = new Vector3Data();
            PlayerEnemyEventSO.RaiseOnEnemyPositionRequested(enemyData);

            if (enemyData != null)
                return enemyData.Position;
            else
                return Vector3.zero;
        }

        public void SetArmRotation()
        {
            Vector3Data enemyData = new Vector3Data();
            PlayerEnemyEventSO.RaiseOnEnemyPositionRequested(enemyData);

            Quaternion newRotation = Quaternion.LookRotation((enemyData.Position + Vector3.up) - transform.position, Vector3.up) * InitialQuaternion;
            LeftArmGameObject.transform.rotation = newRotation;
        }

		public void SubscribeStateEnter(GameState gameState)
		{
            if (gameState == GameState.Menu)
            {

            }
            else if (gameState == GameState.Game)
            {
                DOTween.To(() => IkValue, (x) => IkValue = x, 1f, 0.3f);
            }
		}

		public void SubscribeStateExit(GameState gameState)
		{
			if (gameState == GameState.Menu)
            {
                    
            }
            else if (gameState == GameState.Game)
            {
                DOTween.To(() => IkValue, (x) => IkValue = x, 0, 0.3f);
            }
		}

		#endregion Functions
	}
}