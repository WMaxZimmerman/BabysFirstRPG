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
        private readonly Animation _walkLeft;
        private readonly Animation _walkRight;
        private readonly Animation _walkUp;
        private readonly Animation _walkDown;

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Velocity = 4;
            Layer = 1;
            Health = 500;
            Damage = 50;
            Range = MainGame.TileSize / 2;
            
            AttackTimer = new Timer(1);

            _walkLeft = new Animation {Row = 2};
            _walkRight = new Animation {Row = 3};
            _walkUp = new Animation {Row = 4};
            _walkDown = new Animation {Row = 1};

            SpriteRect = new Rectangle((2 * Width) - Width, (1 * Height) - Height, Width, Height);
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SpriteRect, Color.White);
        }

        protected override void Movement(GameTime gameTime, MainGame game)
        {
            var currentPosition = Position;
            
            if (game.Inputs.IsKeyDown(Keys.Down))
            {
                //Direction = 1;
                Position.Y = Position.Y + Velocity;
                SpriteRect = _walkDown.Play(gameTime);
            }
            else if (game.Inputs.IsKeyDown(Keys.Left))
            {
                //Direction = 2;
                Position.X = Position.X - Velocity;
                SpriteRect = _walkLeft.Play(gameTime);
            }
            else if (game.Inputs.IsKeyDown(Keys.Up))
            {
                //Direction = 4;
                Position.Y = Position.Y - Velocity;
                SpriteRect = _walkUp.Play(gameTime);
            }
            else if (game.Inputs.IsKeyDown(Keys.Right))
            {
                //Direction = 3;
                Position.X = Position.X + Velocity;
                SpriteRect = _walkRight.Play(gameTime);
            }

            foreach (var obstacle in game.Objects.OfType<Obstacle>())
            {
                if (CollidesWith(obstacle))
                {
                    Position = currentPosition;
                }
            }

            game.CenterCamera(this);
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
