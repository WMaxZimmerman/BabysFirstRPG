using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabysFirstRPG.Game.Game
{
    public class GameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public int OrientationDegree { get; set; }
        public int Layer { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool IsRemoved { get; set; }

        private Vector2 InitialPos { get; }

        public GameObject(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            InitialPos = position;
            OrientationDegree = 0;

            Height = texture.Height;
            Width = texture.Width;

            Layer = 1;
        }

        public void MoveToInitialPos()
        {
            Position = InitialPos;
        }

        public bool CollidesWith(GameObject obj)
        {
            var objRect = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Width, obj.Height);
            var thisRect = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            return thisRect.Intersects(objRect);
        }

        public virtual void Update(GameTime gameTime, MainGame game)
        {
            //Can Be Overriden to do stuff.
        }

        public virtual void Draw(ref SpriteBatch spriteBatch)
        {
            var rotation = GetRotation(OrientationDegree);
            var origin = GetOrigin(OrientationDegree);
            spriteBatch.Draw(Texture, Position, null, null, origin, rotation, null, Color.White);
            //spriteBatch.Draw(Texture, Position, Color.White);
        }

        private float GetRotation(int degrees)
        {
            var radians = degrees / 180f;
            return (float)(Math.PI * radians);
        }

        private Vector2 GetOrigin(int degrees)
        {
            var radians = degrees / 180f;

            switch (radians)
            {
                case 0f:
                    return Vector2.Zero;
                case .5f:
                    return new Vector2(0, Height / 1f);
                case 1f:
                    return new Vector2(Width / 1f, Height / 1f);
                case 1.5f:
                    return new Vector2(Width / 1f, 0);
            }

            return Vector2.Zero;
        }

        public virtual void Destroy(MainGame game, GameObject sender)
        {
            IsRemoved = true;
        }
    }
}
