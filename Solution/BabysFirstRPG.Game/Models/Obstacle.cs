using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Models
{
    public class Obstacle: GameObject
    {
        public Obstacle(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Layer = 1;
        }
    }
}
