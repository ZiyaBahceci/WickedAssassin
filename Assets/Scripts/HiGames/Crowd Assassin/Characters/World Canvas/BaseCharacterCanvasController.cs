using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using CrowdAssassin.Event;
using CrowdAssassin.Data;

namespace CrowdAssassin.Character
{
    public class BaseCharacterCanvasController : MonoBehaviour
    {
        #region Variables

        private float _totalHealth;
        private float _currentHealth;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _fillFrame;
        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _healthText;

        [SerializeField] private PlayerEnemyEventSO _playerEnemyEventSO;
        [SerializeField] private HitImagePoolEventSO _hitImagePoolEventSO;

        #endregion Variables

        #region Properties

        private float TotalHealth
        {
            get => _totalHealth;
            set => _totalHealth = value;
        }

        private float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        private Canvas Canvas
        {
            get => _canvas;
            set => _canvas = value;
        }

        private GameObject FillFrame
        {
            get => _fillFrame;
            set => _fillFrame = value;
        }

        private Image FillImage
        {
            get => _fillImage;
            set => _fillImage = value;
        }

        private TextMeshProUGUI HealthText
        {
            get => _healthText;
            set => _healthText = value;
        }

        protected PlayerEnemyEventSO PlayerEnemyEventSO
        {
            get => _playerEnemyEventSO;
            set => _playerEnemyEventSO = value;
        }

        private HitImagePoolEventSO HitImagePoolEventSO
        {
            get => _hitImagePoolEventSO;
            set => _hitImagePoolEventSO = value;
        }

        #endregion Properties

        #region Functions

        public virtual void Initialize()
        {
        }

        public virtual void SubscribeEvents()
        {
        }

        public virtual void UnSubscribeEvents()
        {
        }

        public void SetHealth(float totalHealth)
        {
            TotalHealth = totalHealth;
            CurrentHealth = TotalHealth;

            FillImage.DOFillAmount(1f, 0.5f);
            HealthText.text = CurrentHealth + "/" + TotalHealth;
        }

        public void UpdateHealth(float health)
        {
            CurrentHealth = health;

            FillImage.DOFillAmount(CurrentHealth / TotalHealth, 0.5f);
            HealthText.text = CurrentHealth + "/" + TotalHealth;
        }

        protected void DeactivateCharacterCanvas()
        {
              
            FillFrame.gameObject.SetActive(false);
            HealthText.gameObject.SetActive(false);
        }

        public void ActivateHitImagePoolItem()
        {
            HitImageControllerData hitImageControllerData = new HitImageControllerData();
            HitImagePoolEventSO.RaiseOnHitImageControllerDataRequested(hitImageControllerData);

            if (hitImageControllerData.HitImageController)
            {
                Debug.Log("QPQPQPQP");
                hitImageControllerData.HitImageController.ActivateHitImageController(Canvas);
            }
        }

        public void ActivateCivilianHitImagePoolItem()
        {
            HitCivilianImageControllerData hitCivilianImageControllerData = new HitCivilianImageControllerData();
            HitImagePoolEventSO.RaiseOnCivilianHitImageControllerDataRequested(hitCivilianImageControllerData);

            if (hitCivilianImageControllerData.HitCivilianImageController)
            {
                hitCivilianImageControllerData.HitCivilianImageController.ActivateCivilianHitImageController(Canvas);
            }
        }

        #endregion Functions
    }
}