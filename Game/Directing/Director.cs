using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;


namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        Score score = new Score();
        Mineral mineral = new Mineral();
        Actor actor = new Actor();

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the player.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor player = cast.GetFirstActor("player");
            Point velocity = keyboardService.GetDirection();
            player.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the player's position and resolves any collisions with minerals.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor player = cast.GetFirstActor("player");
            Actor ActorScore = cast.GetFirstActor("score");
            List<Actor> minerals = cast.GetActors("minerals");

            string currentScore = score.GetScore().ToString();
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            foreach (Actor mineral in minerals)
            {
                mineral.MoveNext(maxX, maxY, 5);
            }
            player.MoveNext(maxX, maxY, 0);

            foreach (Actor actor in minerals)
            {
                if (player.GetPosition().Equals(actor.GetPosition()))
                {
                    Mineral mineral = (Mineral) actor;
                    if (mineral.GetText() == "*")
                    {
                        score.HitGem();
                    }
                    else if (mineral.GetText() == "[]")
                    {
                        score.HitRock();
                    }
                    score.SetText($"Score = {currentScore}");
                }
            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.DrawActor(score);
            videoService.FlushBuffer();
        }

    }
}