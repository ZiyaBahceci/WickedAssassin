using DG.Tweening;
using UnityEngine;

namespace CrowdAssassin.Character
{
    public class EnemyCanvasController : BaseCharacterCanvasController
    {
        #region Variables

        [SerializeField] private GameObject _targetIcon;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public override void Initialize()
        {
            base.Initialize();
           
        }

        public override void SubscribeEvents()
        {
            base.SubscribeEvents();

            PlayerEnemyEventSO.OnCharacterDied += OnCharacterDied;
        }

        public override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();

            PlayerEnemyEventSO.OnCharacterDied -= OnCharacterDied;
        }

        public void SetTargetIcon()
        {
            _targetIcon.SetActive(true);
            _targetIcon.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnCharacterDied(BaseCharacterController characterController)
        {
            _targetIcon.SetActive(false);
            DeactivateCharacterCanvas();
        }

        #endregion Functions
    }
}