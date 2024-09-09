using UnityEngine;
using CrowdAssassin.Data;
using CrowdAssassin.Event;

namespace CrowdAssassin.Character
{
    public class BaseArmController : MonoBehaviour
    {
        #region Variables

        private Quaternion _initialQuaternion;

        private Animator _animator;

        [SerializeField] private GameStateEventSO _gameStateEventSO;
        [SerializeField] private PlayerEnemyEventSO _playerEnemyEventSO;

		#endregion Variables

		#region Properties

		protected Quaternion InitialQuaternion { get => _initialQuaternion; set => _initialQuaternion = value; } 

		protected Animator Animator { get => _animator; set => _animator = value; }
        
		protected GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
        protected PlayerEnemyEventSO PlayerEnemyEventSO { get => _playerEnemyEventSO; set => _playerEnemyEventSO = value; }

		#endregion Properties

        #region Functions

        public virtual void Initialize()
        {
            Animator = GetComponent<Animator>();
        }

        public virtual void SubscribeEvents()
        {

        }

        public virtual void UnSubscribeEvents()
        {

        }

        #endregion Functions
    }
}