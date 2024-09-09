using UnityEngine;
using Framework.Data;
using CrowdAssassin.Data;
using CrowdAssassin.Event;
using Framework.Extension;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Framework.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        #region Variables

        private const string GAME_DATA_FILE = "/GameData.json";

        private string _filePath;

        private int _characterDataIndex;

        private GameData _gameData;

        [SerializeField] private List<CharacterDataSO> _characterDataSOList;

        [SerializeField] private MoneyEventSO _moneyEventSO;
        [SerializeField] private LevelDataEventSO _levelDataEventSO;
        [SerializeField] private LevelOperationEventSO _levelOperationEventSO;

        #endregion Variables

        #region Properties

        private string FilePath
        {
            get => _filePath;
            set => _filePath = value;
        }

        private int CharacterDataIndex
        {
            get => _characterDataIndex;
            set => _characterDataIndex = value;
        }

        private GameData GameData
        {
            get => _gameData;
            set => _gameData = value;
        }

        private List<CharacterDataSO> CharacterDataSOList
        {
            get => _characterDataSOList;
            set => _characterDataSOList = value;
        }

        private MoneyEventSO MoneyEventSO
        {
            get => _moneyEventSO;
            set => _moneyEventSO = value;
        }

        private LevelDataEventSO LevelDataEventSO
        {
            get => _levelDataEventSO;
            set => _levelDataEventSO = value;
        }

        private LevelOperationEventSO LevelOperationEventSO
        {
            get => _levelOperationEventSO;
            set => _levelOperationEventSO = value;
        }

        #endregion Properties

        #region OnApplicationQuit

        private void OnApplicationQuit()
        {
            SaveData();
        }

        #endregion OnApplicationQuit

        #region Start

        void Start()
        {
            SubscribeEvents();
            Initialize();
        }

        void OnDestroy()
        {
            UnSubscribeEvents();
        }

        #endregion Start

        #region Functions

        protected override void Awake()
        {
            base.Awake();
            FilePath = Application.persistentDataPath;
            LoadData();
        }

        private void Initialize()
        {
            CharacterDataIndex = Random.Range(0, GameData.levelText % CharacterDataSOList.Count);
            SkipMainScene();
        }

        private void SubscribeEvents()
        {
            MoneyEventSO.OnMoneyUpdated += OnMoneyUpdated;

            LevelDataEventSO.OnGameDataRequested += OnGameDataRequested;
            LevelDataEventSO.OnCharacterDataRequested += GetCharacterData;

            LevelOperationEventSO.OnLevelLoadRequested += LoadCurrentScene;
            LevelOperationEventSO.OnNextLevelLoadRequested += LoadNextScene;
        }

        private void UnSubscribeEvents()
        {
            MoneyEventSO.OnMoneyUpdated -= OnMoneyUpdated;

            LevelDataEventSO.OnGameDataRequested -= OnGameDataRequested;
            LevelDataEventSO.OnCharacterDataRequested -= GetCharacterData;

            LevelOperationEventSO.OnLevelLoadRequested -= LoadCurrentScene;
            LevelOperationEventSO.OnNextLevelLoadRequested -= LoadNextScene;
        }

        private void OnMoneyUpdated(float moneyAmount)
        {
            GameData.money = moneyAmount;
        }

        private GameData OnGameDataRequested()
        {
            return GameData;
        }

        private CharacterDataSO GetCharacterData()
        {
            return CharacterDataSOList[CharacterDataIndex];
        }

        public void SaveData()
        {
            FloatData floatData = LevelDataEventSO.RaiseOnMoneyDataRequested();
            GameData.money = floatData.Value;

            Save.SaveGame(GameData, FilePath, GAME_DATA_FILE);
        }

        private void LoadData()
        {
            GameData = Load.LoadGameData(FilePath, GAME_DATA_FILE);
        }

        public void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNextScene()
        {
            GameData.levelText++;

            LoadCurrentScene();
        }

        private void SkipMainScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion Functions
    }
}