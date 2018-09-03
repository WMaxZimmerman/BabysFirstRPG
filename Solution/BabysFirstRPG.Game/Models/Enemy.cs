using System;
using System.Linq;
using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public class Enemy: Entity
    {
        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Velocity = 1;
            Health = 100;
            Damage = 1;
        }

        protected override void Movement(GameTime gameTime, MainGame game)
        {
            var player = game.Objects.Single(o => o is Player);

            var deltaX = Math.Abs(Position.X - player.Position.X); 
            var deltaY = Math.Abs(Position.Y - player.Position.Y);

            if (deltaX > deltaY)
            {
                if (Position.X < player.Position.X)
                {
                    Position.X = Position.X + Velocity;
                }
                else
                {
                    Position.X = Position.X - Velocity;
                }
            }
            else
            {
                if (Position.Y > player.Position.Y)
                {
                    Position.Y = Position.Y - Velocity;
                }
                else
                {
                    Position.Y = Position.Y + Velocity;
                }
            }
        }
    }
}
