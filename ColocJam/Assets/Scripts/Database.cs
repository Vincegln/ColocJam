public static class Database
{
	private static int combinaisonLength, score, lives;
	private static float levelTime;

	public static int CombinaisonLength 
	{
		get 
		{
			return combinaisonLength;
		}
		set 
		{
			combinaisonLength = value;
		}
	}
	
	public static int Score 
	{
		get 
		{
			return score;
		}
		set 
		{
			score = value;
		}
	}
	
	public static int Lives 
	{
		get 
		{
			return lives;
		}
		set 
		{
			lives = value;
		}
	}

	public static float LevelTime 
	{
		get 
		{
			return levelTime;
		}
		set 
		{
			levelTime = value;
		}
	}
}