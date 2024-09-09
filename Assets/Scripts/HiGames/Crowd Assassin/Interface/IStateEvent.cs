using Framework.Enums;

public interface IStateEvent
{
    #region Functions

    public abstract void SubscribeStateEnter(GameState gameState);
    public abstract void SubscribeStateExit(GameState gameState);

    #endregion Functions
}