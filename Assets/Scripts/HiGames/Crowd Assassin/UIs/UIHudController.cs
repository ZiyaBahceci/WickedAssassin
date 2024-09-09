using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Framework.UI
{
    public class UIHudController : UIBaseController
    {
        #region Variables

        [SerializeField] private TextMeshProUGUI _tutorialText;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public override void Initialize()
        {
            base.Initialize();
        }

        public void ActivateHUD()
        {
            _tutorialText.transform.DOScale(1.25f, .5f).SetLoops(-1, LoopType.Yoyo);
        }

        public void DeactivateHUD()
        {
            gameObject.SetActive(false);
        }

        #endregion Functions
    }
}