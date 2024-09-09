using TMPro;
using UnityEngine;

namespace Framework.UI
{
    public class UIFeedbackController : UIBaseController
    {
        #region Events



        #endregion Events

        #region Variables

        private int _armyIsMovingFeedbackCount;
        private int _roadIsBeingUsedFeedbackCount;

        private bool _isFeedbackActivated;
        private Animator _animator;
        [SerializeField] private TextMeshProUGUI _feedbackText;

		#endregion Variables

		#region Properties

		public bool IsFeedbackActivated { get => _isFeedbackActivated; set => _isFeedbackActivated = value; }
		public TextMeshProUGUI FeedbackText { get => _feedbackText; set => _feedbackText = value; }
		private Animator Animator { get => _animator; set => _animator = value; }
		public int ArmyIsMovingFeedbackCount { get => _armyIsMovingFeedbackCount; set => _armyIsMovingFeedbackCount = value; }
		public int RoadIsBeingUsedFeedbackCount { get => _roadIsBeingUsedFeedbackCount; set => _roadIsBeingUsedFeedbackCount = value; }

		#endregion Properties

		#region Update

        void Update()
        {
            DeactivateFeedback();
        }

        #endregion Update

        #region Functions

        public override void Initialize()
        {
            base.Initialize();

            ArmyIsMovingFeedbackCount = 0;
            RoadIsBeingUsedFeedbackCount = 0;

            Animator = GetComponentInChildren<Animator>();
            IsFeedbackActivated = false;
        }

        private void DeactivateFeedback()
		{
			if (IsFeedbackActivated && Input.GetMouseButtonDown(0))
			{
                IsFeedbackActivated = false;
                Animator.SetTrigger("Close");
            }
		}

        public void CloseFeedback()
		{
            IsFeedbackActivated = true;
            Animator.SetTrigger("Open");
        }

        #endregion Functions
    }
}
