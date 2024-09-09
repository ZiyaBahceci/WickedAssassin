using UnityEngine;
using Framework.UI;
using CrowdAssassin.Event;
using Framework.Data;

namespace Framework.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        #region Variables

        private const string _levelPrefix = "Level: ";

        [SerializeField] private UIWinController _uIWinController;
        [SerializeField] private UIHudController _uIHudController;
        [SerializeField] private UILoseController _uILoseController;
        [SerializeField] private UITTPController _uITTPController;
        [SerializeField] private UITutorialController _uITutorialController;
        [SerializeField] private UIFeedbackController _uIFeedbackController;

        [SerializeField] private GameStateCanvasEventSO _gameStateCanvasEventSO;
        [SerializeField] private LevelDataEventSO _levelDataEventSO;

        #endregion Variables

        #region Properties

        public static string LevelPrefix => _levelPrefix;

        private UIWinController UIWinController
        {
            get => _uIWinController;
            set => _uIWinController = value;
        }

        private UIHudController UIHudController
        {
            get => _uIHudController;
            set => _uIHudController = value;
        }

        private UILoseController UILoseController
        {
            get => _uILoseController;
            set => _uILoseController = value;
        }

        private UITTPController UITTPController
        {
            get => _uITTPController;
            set => _uITTPController = value;
        }

        private UITutorialController UITutorialController
        {
            get => _uITutorialController;
            set => _uITutorialController = value;
        }

        private UIFeedbackController UIFeedbackController
        {
            get => _uIFeedbackController;
            set => _uIFeedbackController = value;
        }

        private GameStateCanvasEventSO GameStateCanvasEventSO
        {
            get => _gameStateCanvasEventSO;
            set => _gameStateCanvasEventSO = value;
        }

        #endregion Properties

        #region Awake - OnDestroy

        void Awake()
        {
            Initialize();
            SubscribeEvents();
        }

        void OnDestroy()
        {
            UnSubscribeEvents();
        }

        #endregion Awake - OnDestroy

        #region Functions

        private void Initialize()
        {
            UIWinController.Initialize();
            UIHudController.Initialize();
            UILoseController.Initialize();
            UITTPController.Initialize();
            UITutorialController.Initialize();
            UIFeedbackController.Initialize();
        }

        private void SubscribeEvents()
        {
            UIWinController.SubscribeEvents();

            GameStateCanvasEventSO.OnGameWin += OnWin;
            GameStateCanvasEventSO.OnGameLose += OnLose;
            GameStateCanvasEventSO.OnGameStart += OnGameStart;
        }

        private void UnSubscribeEvents()
        {
            UIWinController.UnSubscribeEvents();

            GameStateCanvasEventSO.OnGameWin -= OnWin;
            GameStateCanvasEventSO.OnGameLose -= OnLose;
            GameStateCanvasEventSO.OnGameStart -= OnGameStart;
        }

        private void ActivateHUDScreen()
        {
            UIHudController.gameObject.SetActive(true);
            UIHudController.ActivateHUD();
        }

        private void DeactivateHUDScreen()
        {
            UIHudController.DeactivateHUD();
        }

        public void OnGameStart()
        {
            GameData gameData = _levelDataEventSO.RaiseOnGameDataRequested();
            if (gameData.levelText <= 3)
                ActivateHUDScreen();
        }

        public void OnWin()
        {
            DeactivateHUDScreen();
            UIWinController.ActivateScreen();
            UIWinController.UpdateCurrentMoneyText();
        }

        public void OnLose()
        {
            DeactivateHUDScreen();
            UILoseController.ActivateScreen();
        }

        #endregion Functions
    }
}