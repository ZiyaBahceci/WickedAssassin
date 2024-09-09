namespace Framework.Data
{
    public class GameData
    {
        #region Variables

        public float money;
        public int levelText;

        #endregion Variables

        #region Functions
        public GameData(float money, int levelText)
        {
            this.money = money;
            this.levelText = levelText;
        }

        #endregion Functions
    }
}