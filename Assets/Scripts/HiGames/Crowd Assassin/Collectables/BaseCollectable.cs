using UnityEngine;
using DG.Tweening;

namespace Framework.Collectables
{
    public class BaseCollectable : MonoBehaviour
    {
        #region Events



        #endregion Events

        #region Variables

        [SerializeField] private float _collectableValue;
        [SerializeField] private GameObject _onCollectedParticle;

        #endregion Variables

        #region Properties

        public float CollectableValue { get => _collectableValue; set => _collectableValue = value; }
		private GameObject OnCollectedParticle { get => _onCollectedParticle; set => _onCollectedParticle = value; }

		#endregion Properties

		#region Start

		protected virtual void Start()
		{
            RotateCollectable();
        }

		#endregion Start

		#region Functions

        public void RotateCollectable() => transform.DORotate(Vector3.up * 360f, 3f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);

        private void DeactivateCollectable()
        {
            enabled = false;
            gameObject.SetActive(false);
        }

        public virtual void OnCollected()
		{
            OnCollectedParticle.SetActive(true);
            DeactivateCollectable();
        }

        #endregion Functions
    }
}