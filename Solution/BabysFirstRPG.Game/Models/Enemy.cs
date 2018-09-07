using System;
using System.Linq;
using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BabysFirstRPG.Game.Models
{
    public class Enemy: Entity
    {
        private readonly Animation _walkLeft;
        private readonly Animation _walkRight;
        private readonly Animation _walkUp;
        private readonly Animation _walkDown;

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Velocity = 1;
            Health = 100;
            Damage = 1;
            Range = MainGame.TileSize / 4;

            _walkLeft = new Animation { Row = 2 };
            _walkRight = new Animation { Row = 3 };
            _walkUp = new Animation { Row = 4 };
            _walkDown = new Animation { Row = 1 };

            SpriteRect = new Rectangle((2 * Width) - Width, (1 * Height) - Height, Width, Height);
        }

        protected override void Movement(GameTime gameTime, MainGame game)
        {
            var player = game.Objects.SingleOrDefault(o => o is Player);
            if (player == null) return;

            var deltaX = Math.Abs(Position.X - player.Position.X); 
            var deltaY = Math.Abs(Position.Y - player.Position.Y);

            if (deltaX > deltaY)
            {
                if (Position.X < player.Position.X)
                {
                    Position.X = Position.X + Velocity;
                    SpriteRect = _walkRight.Play(gameTime);
                }
                else
                {
                    Position.X = Position.X - Velocity;
                    SpriteRect = _walkLeft.Play(gameTime);
                }
            }
            else
            {
                if (Position.Y > player.Position.Y)
                {
                    Position.Y = Position.Y - Velocity;
                    SpriteRect = _walkUp.Play(gameTime);
                }
                else
                {
                    Position.Y = Position.Y + Velocity;
                    SpriteRect = _walkDown.Play(gameTime);
                }
            }
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SpriteRect, Color.White);
        }

        protected override void Attack(GameTime gameTime, MainGame game)
        {
            foreach (var player in game.Objects.OfType<Player>())
            {
                if (IsWithinRange(player)) player.Health -= Damage;
            }
        }

        protected override void CheckState(GameTime gameTime, MainGame game)
        {
            if (Health <= 0) IsRemoved = true;
        }
    }
}
