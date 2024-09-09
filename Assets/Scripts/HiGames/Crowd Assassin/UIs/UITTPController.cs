using TMPro;
using UnityEngine;
using Framework.Data;
using UnityEngine.UI;
using Framework.Enums;
using CrowdAssassin.Event;
using Framework.Managers;

namespace Framework.UI
{
    public class UITTPController : UIBaseController
    {
        #region Variables

        private GameData _gameData;

        [SerializeField] private Button _tTPButton;
        [SerializeField] private TextMeshProUGUI _currentLevelText;

        [SerializeField] private GameStateEventSO _gameStateEventSO;
        
        
        [SerializeField] private GameStateCanvasEventSO _gameStateCanvasEventSO;

        #endregion Variables

        #region Properties
		
        private GameData GameData { get => _gameData; set => _gameData = value; }

        private Button TTPButton { get => _tTPButton; set => _tTPButton = value; }
        private TextMeshProUGUI CurrentLevelText { get => _currentLevelText; set => _currentLevelText = value; }
		
        private GameStateEventSO GameStateEventSO { get => _gameStateEventSO; set => _gameStateEventSO = value; }
        
        private GameStateCanvasEventSO GameStateCanvasEventSO { get => _gameStateCanvasEventSO; set => _gameStateCanvasEventSO = value; }

		#endregion Properties

		#region Functions

		public override void Initialize()
        {
            base.Initialize();

            GetGameData();
            UpdateLevelText();
            TTPButton.onClick.AddListener(OnTTPButtonClick);
        }

        private void OnTTPButtonClick()
        {
            GameStateEventSO.ChangeState(GameState.Game);
            _gameStateCanvasEventSO.RaiseOnGameStart();
            CameraManager.Instance.ActivateGameplayCamera();
            gameObject.SetActive(false);
        }

        private void GetGameData()
        {
            GameData = LevelDataEventSO.RaiseOnGameDataRequested();
        }

        private void UpdateLevelText()
        {
            CurrentLevelText.text = "Level: " + GameData?.levelText.ToString();
        }

        #endregion Functions
    }
}
