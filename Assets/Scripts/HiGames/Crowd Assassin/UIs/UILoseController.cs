using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Framework.Data;

namespace Framework.UI
{
    public class UILoseController : UIBaseController
    {
        #region Variables

        private GameData _gameData;
        
        [SerializeField] private Button _retryButton;
        [SerializeField] private GameObject _levelFailImage;
        [SerializeField] private TextMeshProUGUI _levelText;

        #endregion Variables

        #region Properties

		private GameData GameData { get => _gameData; set => _gameData = value; }
        
        private GameObject LevelFailImage { get => _levelFailImage; set => _levelFailImage = value; }
		private Button RetryButton { get => _retryButton; set => _retryButton = value; }
		private TextMeshProUGUI LevelText { get => _levelText; set => _levelText = value; }

		#endregion Properties

		#region Functions

		public override void OnEnableInitialize()
		{
			base.OnEnableInitialize();

            GetGameData();
            UpdateLevelText();
        }

		public override void Initialize()
        {
            base.Initialize();

            RetryButton.onClick.AddListener(RetryButtonClick);
        }

        public void ActivateScreen() => gameObject.SetActive(true);

        private void RetryButtonClick()
		{
            LevelOperationEventSO.RaiseOnLevelLoadRequested();
        }

        private void GetGameData()
        {
            GameData = LevelDataEventSO.RaiseOnGameDataRequested();
        }

        private void UpdateLevelText()
        {
            LevelText.text = "Level: " + (GameData?.levelText).ToString();
        }

        #endregion Functions
    }
}