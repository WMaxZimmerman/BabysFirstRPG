using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public class UiText: GameObject
    {
        public UiText(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Layer = 2;
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            //base.Draw(ref spriteBatch);
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            base.Update(gameTime, game);
        }
    }
}
