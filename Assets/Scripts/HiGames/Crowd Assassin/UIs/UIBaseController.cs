using UnityEngine;
using DG.Tweening;
using CrowdAssassin.Event;

namespace Framework.UI
{
    public class UIBaseController : MonoBehaviour
    {
        #region Variables

        [SerializeField] protected CanvasGroup _canvasGroup;

		[SerializeField] private LevelDataEventSO _levelDataEventSO;
		[SerializeField] private LevelOperationEventSO levelOperationEventSO;

		#endregion Variables

		#region Properties

		protected CanvasGroup CanvasGroup { get => _canvasGroup; set => _canvasGroup = value; }
		
		protected LevelDataEventSO LevelDataEventSO { get => _levelDataEventSO; set => _levelDataEventSO = value; }
		protected LevelOperationEventSO LevelOperationEventSO { get => levelOperationEventSO; set => levelOperationEventSO = value; }

		#endregion Properties

		#region OnEnable

		protected virtual void OnEnable()
		{
            CanvasGroup.DOFade(1f, 0.5f);
			OnEnableInitialize();
		}
        
        #endregion OnEnable

		#region Functions

        public virtual void OnEnableInitialize()
		{

		}

		public virtual void Initialize()
		{

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