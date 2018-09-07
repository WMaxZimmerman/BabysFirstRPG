using Microsoft.Xna.Framework;

namespace BabysFirstRPG.Game.Game
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public Timer AnimTimer { get; set; }
        public int Row { get; set; }
        public int SpriteDirection { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Animation()
        {
            CurrentFrame = 1;
            AnimTimer = new Timer(.1m);
            Row = 1;
            SpriteDirection = 1;
            Height = MainGame.TileSize;
            Width = MainGame.TileSize;
        }

        public Rectangle Play(GameTime gameTime)
        {
            AnimTimer.Increment(gameTime.ElapsedGameTime);

            if (AnimTimer.IsReady)
            {
                CurrentFrame += SpriteDirection;
                if (CurrentFrame >= 3)
                {
                    SpriteDirection *= -1;
                }
                else if (CurrentFrame <= 1)
                {
                    SpriteDirection *= -1;
                }

                AnimTimer.Reset();
            }

            return new Rectangle((CurrentFrame * Width) - Width, (Row * Height) - Height, Width, Height);
        }
    }
}
