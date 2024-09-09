using UnityEngine;
using DG.Tweening;
using Framework.Enums;
using CrowdAssassin.Data;
using CrowdAssassin.Character;

namespace CrowdAssassin.Enemy
{
    public class EnemyArmController : BaseArmController, IStateEvent
    {
        #region Variables

        private float _ikValue;

        [SerializeField] private GameObject _rightArmGameObject;

        #endregion Variables

        #region Properties

        private float IkValue { get => _ikValue; set => _ikValue = value; }

        private GameObject RightArmGameObject { get => _rightArmGameObject; set => _rightArmGameObject = value; }

        #endregion Properties

        #region Awake- LateUpdate

        void Awake()
        {
            SetInitialRotation();
        }

        void OnAnimatorIK(int layerIndex)
        {
            Animator.SetIKPosition(AvatarIKGoal.RightHand, GetOpponentPosition());
            Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, IkValue);
        }

        #endregion Awake- LateUpdate

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
            InitialQuaternion = RightArmGameObject.transform.rotation;
        }

        private Vector3 GetOpponentPosition()
        {
            Vector3Data playerData = new Vector3Data();
            PlayerEnemyEventSO.RaiseOnPlayerPositionRequested(playerData);

            if (playerData != null)
                return playerData.Position;
            else
                return Vector3.zero;
        }

        public void SetArmRotation()
        {
            Vector3Data playerData = new Vector3Data();
            PlayerEnemyEventSO.RaiseOnPlayerPositionRequested(playerData);

            Quaternion newRotation = Quaternion.LookRotation(transform.position - (playerData.Position + Vector3.up)) * InitialQuaternion;
            RightArmGameObject.transform.rotation = newRotation;
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