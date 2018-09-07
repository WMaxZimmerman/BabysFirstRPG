using System.Collections.Generic;
using System.Linq;
using BabysFirstRPG.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BabysFirstRPG.Game.Game
{
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public List<GameObject> Objects { get; set; }
        //public GameState State { get; set; }
        public Dictionary<string, Texture2D> Textures { get; set; }
        public Dictionary<string, SpriteFont> Fonts { get; set; }
        public KeyboardState Inputs { get; set; }
        public int Level { get; set; }

        public float WorldX { get; set; }
        public float WorldY { get; set; }
        public static int TileSize { get; set; }

        private bool _sceneSwitched;
        private int _newScene;

        private List<GameObject> _newObjects = new List<GameObject>();

        public MainGame()
        {
            TileSize = 32;
            _graphics = new GraphicsDeviceManager(this);
            Objects = new List<GameObject>();
            Content.RootDirectory = "Content";
            //_graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            Level = 0;
            //State = GameState.Pause;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Textures = new Dictionary<string, Texture2D>();

            Textures.Add("Player", Content.Load<Texture2D>("Player"));
            Textures.Add("Enemy", Content.Load<Texture2D>("enemy"));
            Textures.Add("Demon", Content.Load<Texture2D>("Demon"));
            Textures.Add("Corner", Content.Load<Texture2D>("Corner"));
            Textures.Add("Wall", Content.Load<Texture2D>("Wall"));
            Textures.Add("Floor", Content.Load<Texture2D>("Floor"));
            Textures.Add("WallTransitionRight", Content.Load<Texture2D>("WallTransitionRight"));
            Textures.Add("WallTransitionLeft", Content.Load<Texture2D>("WallTransitionLeft"));
            Textures.Add("FloorTransition", Content.Load<Texture2D>("FloorTransition"));

            Fonts = new Dictionary<string, SpriteFont>();

            //Fonts.Add("Banner", Content.Load<SpriteFont>("Banner"));
            //Fonts.Add("Score", Content.Load<SpriteFont>("Score"));

            SceneSwitch(0);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            Inputs = Keyboard.GetState();

            foreach (var gameObject in Objects)
            {
                gameObject.Update(gameTime, this);
            }

            Objects.RemoveAll(o => o.IsRemoved);

            Objects.AddRange(_newObjects);
            _newObjects = new List<GameObject>();

            if (_sceneSwitched) SceneSwitch();
        }

        public void SceneSwitch(int level)
        {
            _newScene = level;
            _sceneSwitched = true;
        }

        private void SceneSwitch()
        {
            _sceneSwitched = false;
            Objects.RemoveAll(o => true);
            Level = _newScene;

            if (_newScene == 0)
            {
                MapGenerator.GenerateMap(this);

                Objects.Add(new Enemy(Textures["Demon"], new Vector2(32, 32)));
                Objects.Add(new Player(Textures["Player"], new Vector2(64, 64)));
            }
        }

        public void CenterCamera(GameObject obj)
        {
            var width = GraphicsDevice.Viewport.Width;
            var height = GraphicsDevice.Viewport.Height;
            var offset = TileSize / 2;

            var x = -obj.Position.X + ((width / 2) - offset);
            var y = -obj.Position.Y + ((height / 2) - offset);

            WorldX = x;
            WorldY = y;
        }

        public void AddObject(GameObject newObject)
        {
            _newObjects.Add(newObject);
        }

        protected override void Draw(GameTime gameTime)
        {
            var layers = Objects.Select(o => o.Layer).Distinct();

            GraphicsDevice.Clear(Color.Black);

            foreach (var layer in layers.OrderBy(l => l))
            {
                _spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Matrix.CreateTranslation(WorldX, WorldY, 0));

                foreach (var gameObject in Objects.Where(o => o.Layer == layer))
                {
                    gameObject.Draw(ref _spriteBatch);
                }

                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
