using System;
using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public class Entity: GameObject
    {
        public int Velocity { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }

        public Entity(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Layer = 1;
            Range = 1;
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            Movement(gameTime, game);
            Attack(gameTime, game);
            CheckState(gameTime, game);
        }

        protected virtual void Movement(GameTime gameTime, MainGame game)
        {
            //Can Be Overriden to do stuff.
        }

        protected virtual void Attack(GameTime gameTime, MainGame game)
        {
            //Can Be Overriden to do stuff.
        }

        protected virtual void CheckState(GameTime gameTime, MainGame game)
        {
            //Can Be Overriden to do stuff.
        }

        public virtual bool IsWithinRange(GameObject obj)
        {
            var deltaX = Math.Abs(Position.X - obj.Position.X);
            var deltaY = Math.Abs(Position.Y - obj.Position.Y);

            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return distance < Range;
        }
    }
}
