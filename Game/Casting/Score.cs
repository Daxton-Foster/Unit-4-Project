namespace Unit04.Game.Casting
{

    public class Score : Actor
    {
        private int score = 0;
        // Constructs a new instance of Score
        public Score()
        {
        }
        public int GetScore()
        {
            return score;
        }
        public int HitRock()
        {
            score = score - 1;
            return score;
        }
        public int HitGem()
        {
            score = score + 1;
            return score;
        }
    }
}