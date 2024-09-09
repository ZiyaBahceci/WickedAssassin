using TMPro;
using UnityEngine;
using Framework.Data;
using UnityEngine.UI;
using CrowdAssassin.Event;

namespace Framework.UI
{
    public class UIWinController : UIBaseController
    {
        #region Variables

        private GameData _gameData;

        [SerializeField] private Button _nextLevelButton;

        [SerializeField] private TextMeshProUGUI _nextLevelText;
        [SerializeField] private TextMeshProUGUI _totalMoneyText;
        [SerializeField] private TextMeshProUGUI _currentMoneyText;

        [SerializeField] private MoneyEventSO _moneyEventSO;

        #endregion Variables

        #region Properties

        private GameData GameData { get => _gameData; set => _gameData = value; }
		
        private Button NextLevelButton { get => _nextLevelButton; set => _nextLevelButton = value; }

		private TextMeshProUGUI NextLevelText { get => _nextLevelText; set => _nextLevelText = value; }
		private TextMeshProUGUI TotalMoneyText { get => _totalMoneyText; set => _totalMoneyText = value; }
		private TextMeshProUGUI CurrentMoneyText { get => _currentMoneyText; set => _currentMoneyText = value; }

        private MoneyEventSO MoneyEventSO { get => _moneyEventSO; set => _moneyEventSO = value; }

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

            NextLevelButton.onClick.AddListener(NextLevelButtonClick);
        }

		public override void SubscribeEvents()
		{
			base.SubscribeEvents();

            MoneyEventSO.OnMoneyUpdated += OnMoneyUpdated;
        }

		public override void UnSubscribeEvents()
		{
			base.UnSubscribeEvents();

            MoneyEventSO.OnMoneyUpdated -= OnMoneyUpdated;
        }

		public void ActivateScreen() => gameObject.SetActive(true);

        private void NextLevelButtonClick()
        {
            LevelOperationEventSO.RaiseOnNextLevelLoadRequested();
        }

        private void GetGameData()
        {
            GameData = LevelDataEventSO.RaiseOnGameDataRequested();
        }

        private void UpdateLevelText()
        {
            NextLevelText.text = "Level: " + (GameData?.levelText + 1).ToString();
        }

        private void OnMoneyUpdated(float moneyAmount)
		{
            UpdateTotalMoneyText(moneyAmount);
        }

        private void UpdateTotalMoneyText(float totalMoney)
		{
            TotalMoneyText.text = totalMoney.ToString();
		}

        public void UpdateCurrentMoneyText()
		{
            CurrentMoneyText.text = "+" + 100f;
		}

        #endregion Functions
    }
}