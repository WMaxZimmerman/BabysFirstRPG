using System;
using System.Linq;
using BabysFirstRPG.Game.Enums;
using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BabysFirstRPG.Game.Models
{
    public class Npc: Entity
    {
        private Direction _direction;
        private Rectangle _bounds;

        public Npc(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Velocity = 1;
            Health = 100;
            Damage = 1;
            Range = MainGame.TileSize / 4;

            Animations.Add("WalkLeft", new Animation { Row = 2 });
            Animations.Add("WalkRight", new Animation { Row = 3 });
            Animations.Add("WalkUp", new Animation { Row = 4 });
            Animations.Add("WalkDown", new Animation { Row = 1 });

            _direction = Direction.Down;
            _bounds = new Rectangle(0, 0, 320, 320);

            SpriteRect = new Rectangle((2 * Width) - Width, (1 * Height) - Height, Width, Height);
        }

        protected override void Movement(GameTime gameTime, MainGame game)
        {
            Wander(gameTime, game);
        }

        private void Wander(GameTime gameTime, MainGame game)
        {
            var currentPos = Position;
            var upperChoice = 160;
            var choice = MainGame.Rand.Next(1, upperChoice + 1);

            if (choice == upperChoice)
            {
                choice = MainGame.Rand.Next(1, 4 + 1);

                switch (choice)
                {
                    case 1:
                        _direction = Direction.Up;
                        break;
                    case 2:
                        _direction = Direction.Right;
                        break;
                    case 3:
                        _direction = Direction.Down;
                        break;
                    case 4:
                        _direction = Direction.Left;
                        break;
                }
            }

            switch (_direction)
            {
                case Direction.Up:
                    Position.Y = Position.Y - Velocity;
                    SpriteRect = Animations["WalkUp"].Play(gameTime);
                    break;
                case Direction.Right:
                    Position.X = Position.X + Velocity;
                    SpriteRect = Animations["WalkRight"].Play(gameTime);
                    break;
                case Direction.Down:
                    Position.Y = Position.Y + Velocity;
                    SpriteRect = Animations["WalkDown"].Play(gameTime);
                    break;
                case Direction.Left:
                    Position.X = Position.X - Velocity;
                    SpriteRect = Animations["WalkLeft"].Play(gameTime);
                    break;
            }

            if (!_bounds.Intersects(GetRect()))
            {
                Position = currentPos;
                _direction = (Direction)((int)_direction *  -1);
            }
            else
            {
                foreach (var obstacle in game.Objects.OfType<Obstacle>())
                {
                    if (CollidesWith(obstacle))
                    {
                        Position = currentPos;
                        _direction = (Direction)((int)_direction * -1);
                    }
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
