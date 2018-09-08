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
            Range = MainGame.TileSize / 4;
            
            Animations.Add("WalkLeft", new Animation { Row = 2 });
            Animations.Add("WalkRight", new Animation { Row = 3 });
            Animations.Add("WalkUp", new Animation { Row = 4 });
            Animations.Add("WalkDown", new Animation { Row = 1 });

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
                    SpriteRect = Animations["WalkRight"].Play(gameTime);
                }
                else
                {
                    Position.X = Position.X - Velocity;
                    SpriteRect = Animations["WalkLeft"].Play(gameTime);
                }
            }
            else
            {
                if (Position.Y > player.Position.Y)
                {
                    Position.Y = Position.Y - Velocity;
                    SpriteRect = Animations["WalkUp"].Play(gameTime);
                }
                else
                {
                    Position.Y = Position.Y + Velocity;
                    SpriteRect = Animations["WalkDown"].Play(gameTime);
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
