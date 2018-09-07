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
        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Velocity = 1;
            Health = 100;
            Damage = 1;
            Range = MainGame.TileSize / 4;
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
