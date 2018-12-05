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
            Range = MainGame.TileSize / 2;
            
            AttackTimer = new Timer(1);

            Animations.Add("WalkLeft", new Animation { Row = 2 });
            Animations.Add("WalkRight", new Animation { Row = 3 });
            Animations.Add("WalkUp", new Animation { Row = 4 });
            Animations.Add("WalkDown", new Animation { Row = 1 });

            SpriteRect = new Rectangle((2 * Width) - Width, (1 * Height) - Height, Width, Height);
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
                foreach (var enemy in game.Objects.OfType<Enemy>())
                {
                    if (IsWithinRange(enemy)) enemy.Health -= Damage;
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

        protected override void CheckState(GameTime gameTime, MainGame game)
        {
            if (Health <= 0) IsRemoved = true;
        }
    }
}
