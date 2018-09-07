using System;
using System.Linq;
using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BabysFirstRPG.Game.Models
{
    public class Player: Entity
    {
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Velocity = 4;
            Layer = 1;
            Health = 500;
            Damage = 50;
            Range = 32;
        }

        protected override void Movement(GameTime gameTime, MainGame game)
        {
            var currPosX = Position.X;
            var currPosY = Position.Y;

            if (game.Inputs.IsKeyDown(Keys.Left))
            {
                Position.X = Position.X - Velocity;
            }
            else if (game.Inputs.IsKeyDown(Keys.Right))
            {
                Position.X = Position.X + Velocity;
            }
            else if (game.Inputs.IsKeyDown(Keys.Down))
            {
                Position.Y = Position.Y + Velocity;
            }
            else if (game.Inputs.IsKeyDown(Keys.Up))
            {
                Position.Y = Position.Y - Velocity;
            }

            foreach (var obstacle in game.Objects.OfType<Obstacle>())
            {
                if (CollidesWith(obstacle))
                {
                    Position.X = currPosX;
                    Position.Y = currPosY;
                }
            }

            MoveCamera(game);
        }

        private void MoveCamera(MainGame game)
        {
            var width = 1600;
            var height = 960;
            var offset = 0;

            var x = -Position.X + (width / 4);
            var y = -Position.Y + (height / 4);
            
            game.WorldX = x;
            game.WorldY = y;

            //game.GraphicsDevice.Viewport = new Viewport((int)Position.X, (int)Position.Y, width, height);
        }

        protected override void Attack(GameTime gameTime, MainGame game)
        {
            if (game.Inputs.IsKeyDown(Keys.Space))
            {
                foreach (var enemy in game.Objects.OfType<Enemy>())
                {
                    if (IsWithinRange(enemy)) enemy.Health -= Damage;
                }
            }
        }

        protected override void CheckState(GameTime gameTime, MainGame game)
        {
            if (Health <= 0) IsRemoved = true;
        }
    }
}
