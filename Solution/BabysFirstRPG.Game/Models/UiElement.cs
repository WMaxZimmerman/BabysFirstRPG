using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public class UiElement: GameObject
    {
        protected Vector2 _relativePosition;

        public UiElement(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _relativePosition = position;
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            Position.X = _relativePosition.X - game.WorldX;
            Position.Y = _relativePosition.Y - game.WorldY;
        }
    }
}
