namespace ADX2LEDemo.Scripts.Score
{
    public class ScoreManager
    {
        public int score { get; private set; } = 0;
        public void AddScore(int addScore) => score += addScore;
    }
}