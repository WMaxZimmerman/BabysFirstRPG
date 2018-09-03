﻿using System.Linq;
using BabysFirstRPG.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BabysFirstRPG.Game.Game
{
    public class Player: Entity
    {
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Velocity = 4;
            Layer = 1;
            Health = 500;
            Damage = 50;
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
    }
}
