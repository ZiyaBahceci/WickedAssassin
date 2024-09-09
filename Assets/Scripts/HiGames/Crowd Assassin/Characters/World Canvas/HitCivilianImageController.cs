using UnityEngine;
using DG.Tweening;
using CrowdAssassin.Event;

namespace CrowdAssassin.Character
{
    public class HitCivilianImageController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _zEndValue;

        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private HitImagePoolEventSO _hitImagePoolEventSO;

        #endregion Variables

        #region Properties

        private float ZEndValue { get => _zEndValue; set => _zEndValue = value; }

        private CanvasGroup CanvasGroup { get => _canvasGroup; set => _canvasGroup = value; }

        private HitImagePoolEventSO HitImagePoolEventSO { get => _hitImagePoolEventSO; set => _hitImagePoolEventSO = value; }

        #endregion Properties

        #region Functions

        private void Initialize()
        {

        }

        private void SubscribeEvents()
        {

        }

        private void UnSubscribeEvents()
        {

        }

        public void DeactivateImageController(Transform parent)
        {
            CanvasGroup.alpha = 1f;
            gameObject.SetActive(false);
        }

        public void ActivateCivilianHitImageController(Canvas canvas)
        {
            gameObject.SetActive(true);
            transform.SetParent(canvas.transform, false);

            GetComponent<RectTransform>().anchoredPosition = Vector3.up / 2f;

            transform.DOMoveZ(transform.position.z + ZEndValue, 2f);
            CanvasGroup.DOFade(0, 2f).OnComplete(() =>
            {
                HitImagePoolEventSO.RaiseOnCivilianHitImageControllerSentBack(this);
            });
            
        }

        #endregion Functions
    }
}