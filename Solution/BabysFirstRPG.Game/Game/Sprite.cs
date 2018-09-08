using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Game
{
    public class Sprite: GameObject
    {
        protected Rectangle SpriteRect;
        protected Dictionary<string, Animation> Animations;

        public Sprite(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Animations = new Dictionary<string, Animation>();
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            var rotation = GetRotation(OrientationDegree);
            var origin = GetOrigin(OrientationDegree);
            spriteBatch.Draw(Texture, Position, null, SpriteRect, origin, rotation, null, Color.White);
        }
    }
}
