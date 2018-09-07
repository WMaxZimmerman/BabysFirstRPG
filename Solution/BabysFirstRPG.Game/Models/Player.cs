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
        protected Rectangle spriteRect;
        protected int currentFrame;
        protected float timer = 0f;
        protected float interval = 100f;
        public int Direction { get; set; }
        private int spriteDirection;

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            spriteDirection = 1;
            Velocity = 4;
            Layer = 1;
            Health = 500;
            Damage = 50;
            Range = MainGame.TileSize / 2;
            currentFrame = 2;
            Direction = 1;
            
            AttackTimer = new Timer(1);
        }


        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, spriteRect, Color.White);
        }

        protected override void Movement(GameTime gameTime, MainGame game)
        {
            var currentPosition = Position;
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var isIdle = false;

            if (game.Inputs.IsKeyDown(Keys.Down))
            {
                Direction = 1;
                Position.Y = Position.Y + Velocity;
            }
            else if (game.Inputs.IsKeyDown(Keys.Left))
            {
                Direction = 2;
                Position.X = Position.X - Velocity;
            }
            else if (game.Inputs.IsKeyDown(Keys.Up))
            {
                Direction = 4;
                Position.Y = Position.Y - Velocity;
            }
            else if (game.Inputs.IsKeyDown(Keys.Right))
            {
                Direction = 3;
                Position.X = Position.X + Velocity;
            }
            else
            {
                isIdle = true;
            }

            if (isIdle)
            {
                currentFrame = 2;
            }
            else
            {
                if (timer > interval)
                {
                    currentFrame += spriteDirection;
                    if (currentFrame >= 3)
                    {
                        spriteDirection *= -1;
                    }
                    else if (currentFrame <= 1)
                    {
                        spriteDirection *= -1;
                    }

                    timer = 0f;
                }
            }

            foreach (var obstacle in game.Objects.OfType<Obstacle>())
            {
                if (CollidesWith(obstacle))
                {
                    Position = currentPosition;
                }
            }

            game.CenterCamera(this);

            spriteRect = new Rectangle((currentFrame * Width) - Width, (Direction * Height) - Height, Width, Height);
        }

        protected override void Attack(GameTime gameTime, MainGame game)
        {
            AttackTimer.Increment(gameTime.ElapsedGameTime);

            if (game.Inputs.IsKeyDown(Keys.Space) && AttackTimer.IsReady)
            {
                AttackTimer.Reset();
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
