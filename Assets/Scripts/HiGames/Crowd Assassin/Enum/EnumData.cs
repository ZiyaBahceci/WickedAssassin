namespace Framework.Enums
{
	public enum GameState
	{
		None,
		Menu,
		Game
	}

	public enum RotationDirection
	{
		Up,
		Right,
		Left
	}

	public enum PlayerState
	{
		Idle,
		Walking
	}

	public enum FinishState
	{
		None,
		Fail,
		Success
	}
	
	public enum BulletOwner
	{
		None,
		Player,
		Enemy
	}
}