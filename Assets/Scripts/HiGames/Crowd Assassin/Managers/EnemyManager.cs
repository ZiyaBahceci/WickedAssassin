using UnityEngine;
using CrowdAssassin.Enemy;
using CrowdAssassin.Character;

namespace Framework.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private EnemyArmController _enemyArmController;
        [SerializeField] private EnemyCanvasController _enemyCanvasController;
        [SerializeField] private EnemyAnimationController _enemyAnimationController;

        #endregion Variables

        #region Properties

        private EnemyController EnemyController { get => _enemyController; set => _enemyController = value; }
        private EnemyArmController EnemyArmController { get => _enemyArmController; set => _enemyArmController = value; }
		private EnemyCanvasController EnemyCanvasController { get => _enemyCanvasController; set => _enemyCanvasController = value; }
        private EnemyAnimationController EnemyAnimationController { get => _enemyAnimationController; set => _enemyAnimationController = value; }

		#endregion Properties

		#region Awake - Start

		void Awake()
        {
            Initialize();
            SubscribeEvents();
        }

        void Start()
        {

        }

		private void OnDestroy()
		{
            UnSubscribeEvents();
        }

		#endregion Awake - Start

		#region Functions

		private void Initialize()
        {
            EnemyController.Initialize();
            EnemyArmController.Initialize();
            EnemyCanvasController.Initialize();
            EnemyAnimationController.Initialize();
        }

        private void SubscribeEvents()
        {
            EnemyController.SubscribeEvents();
            EnemyArmController.SubscribeEvents();
            EnemyCanvasController.SubscribeEvents();
            EnemyAnimationController.SubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            EnemyController.UnSubscribeEvents();
            EnemyArmController.UnSubscribeEvents();
            EnemyCanvasController.UnSubscribeEvents();
            EnemyAnimationController.UnSubscribeEvents();
        }

        #endregion Functions
    }
}