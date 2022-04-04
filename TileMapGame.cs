using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ForestTile
{
    public class TileMapGame : Game
    {

        MouseState _priorMouse;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
      //  private Tilemap _tilemap;
      //  private Tilemap _tilemapProps;

        public Player _player;
        private Background _background;

        Enemy _enemy, _enemy2;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }


        public TileMapGame()
        {
            _graphics = new GraphicsDeviceManager(this);
           // _graphics.PreferredBackBufferWidth = Constants.GAME_WIDTH;
          //  _graphics.PreferredBackBufferHeight = Constants.GAME_HEIGHT;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           // _tilemap = new Tilemap("map.txt");
          //  _tilemapProps = new Tilemap("overlay.txt");

            _player = new Player();
            _background = new Background();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

          //  _tilemap.LoadContent(Content);
          //  _tilemapProps.LoadContent(Content);

            _player.LoadContent(Content);
            _enemy = new Enemy(Content.Load<Texture2D>("rat and bat spritesheet calciumtrice"), new Vector2(600, 450), 150);
            //_enemy2 = new Enemy(Content.Load<Texture2D>("rat and bat spritesheet calciumtrice"), new Vector2(620, 450), 0);


            _background.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState currentMouse = Mouse.GetState();
            Vector2 mousePosition = new Vector2(currentMouse.X, currentMouse.Y);

            _player.Update(gameTime);
            _background.Update(gameTime, _player);
            _enemy.Update(gameTime, _player);


            if (currentMouse.LeftButton == ButtonState.Pressed && _priorMouse.LeftButton == ButtonState.Released)
            {
               //attack the rats

            }

            Velocity = mousePosition - Position;
            Position = mousePosition;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _background.Draw(gameTime, _spriteBatch, _player, _enemy);

            _spriteBatch.Begin();

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
