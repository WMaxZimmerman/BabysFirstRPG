using BabysFirstRPG.Game.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public class Item : GameObject
    {
        public Item(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }

        public override void Destroy(MainGame game, GameObject sender)
        {
            base.Destroy(game, sender);
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            base.Draw(ref spriteBatch);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            base.Update(gameTime, game);
        }
    }
}
