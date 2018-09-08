using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public class Letter: UiElement
    {
        public char Text;
        private readonly Rectangle _viewRect;

        public Letter(Texture2D texture, Vector2 position, char letter) : base(texture, position)
        {
            Layer = 2;
            Width = 10;
            Height = 12;
            Text = letter;

            var letterPos = Settings.Alphabet.IndexOf(letter) + 1;

            _viewRect = new Rectangle((letterPos * Width) - Width, 0, Width, Height);
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            var rotation = GetRotation(OrientationDegree);
            var origin = GetOrigin(OrientationDegree);
            spriteBatch.Draw(Texture, Position, null, _viewRect, origin, rotation, null, Color.White);
        }
    }
}
