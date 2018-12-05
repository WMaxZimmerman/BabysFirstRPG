using System;
using System.Collections.Generic;
using System.Linq;
using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BabysFirstRPG.Game.Models
{
    public class Player: Entity
    {
        private List<Item> _inventroy;

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Velocity = 4;
            Layer = 1;
            Health = 500;
            Damage = 50;
            Range = MainGame.TileSize / 2;
            
            AttackTimer = new Timer(1);

            Animations.Add("WalkLeft", new Animation { Row = 2 });
            Animations.Add("WalkRight", new Animation { Row = 3 });
            Animations.Add("WalkUp", new Animation { Row = 4 });
            Animations.Add("WalkDown", new Animation { Row = 1 });

            SpriteRect = new Rectangle((2 * Width) - Width, (1 * Height) - Height, Width, Height);
            _inventroy = new List<Item>();
        }

        protected override void Movement(GameTime gameTime, MainGame game)
        {
            var currentPosition = Position;
            
            if (game.Inputs.IsKeyDown(Keys.Down))
            {
                Position.Y = Position.Y + Velocity;
                SpriteRect = Animations["WalkDown"].Play(gameTime);
            }
            else if (game.Inputs.IsKeyDown(Keys.Left))
            {
                Position.X = Position.X - Velocity;
                SpriteRect = Animations["WalkLeft"].Play(gameTime);
            }
            else if (game.Inputs.IsKeyDown(Keys.Up))
            {
                Position.Y = Position.Y - Velocity;
                SpriteRect = Animations["WalkUp"].Play(gameTime);
            }
            else if (game.Inputs.IsKeyDown(Keys.Right))
            {
                Position.X = Position.X + Velocity;
                SpriteRect = Animations["WalkRight"].Play(gameTime);
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
                var dmg = _inventroy.OfType<Weapon>().Where(w => w.IsEquiped).Sum(w => w.Damage);
                if (dmg < Damage)
                {
                    dmg = Damage;
                }

                foreach (var enemy in game.Objects.OfType<Enemy>())
                {
                    if (IsWithinRange(enemy)) enemy.Health -= dmg;
                }
            }
	        else if (game.Inputs.IsKeyDown(Keys.E) && AttackTimer.IsReady)
	        {
		        var npc = game.Objects.OfType<Npc>().FirstOrDefault(n => IsWithinRange(n));
		        if (npc != null)
		        {
		            npc.Interact(game);
		        }
	        }
        }

        public override bool IsWithinRange(GameObject obj)
        {
            var deltaX = Math.Abs(Position.X - obj.Position.X);
            var deltaY = Math.Abs(Position.Y - obj.Position.Y);

            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            var rng = _inventroy.OfType<Weapon>().Where(w => w.IsEquiped).Max(w => w.Range);
            if (rng < Range)
            {
                rng = Range;
            }

            return distance < rng;
        }

        protected override void CheckState(GameTime gameTime, MainGame game)
        {
            if (Health <= 0) IsRemoved = true;
        }
    }
}
