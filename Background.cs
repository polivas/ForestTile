using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ForestTile
{
    public class Background : Game
    {
        public Texture2D texture;
        public Rectangle rectangle;

        // Layer textures
        private Texture2D _sky;
        private Texture2D _ground;
       // private Texture2D _grass;


        //Tile stuff
        private Tilemap _tilemap;
        private Tilemap _tilemapProps;

        //Sprites
        private Enemy[] _enemy2 = new Enemy[5];
        private Player _player;

        // The position of the sprite
        private Vector2 _position;

        /// <summary>
        /// The current position
        /// </summary>
        public Vector2 Position => _position;



        /// <summary>
        /// Loads sprites into the game
        /// </summary>
        /// <param name="content">The content that is to be loaded fro monogame</param>
        public void LoadContent(ContentManager content)
        {

            _sky = content.Load<Texture2D>("Sky");
            _ground = content.Load<Texture2D>("BackGround");
            //_grass = content.Load<Texture2D>("");
            _tilemap = new Tilemap("map.txt");
            _tilemapProps = new Tilemap("overlay.txt");

            for(int i = 0; i < 5;  i++)
            {
                _enemy2[i] = new Enemy(content.Load<Texture2D>("rat and bat spritesheet calciumtrice"), new Vector2(i * 200, 450), 0);
            }


            _tilemap.LoadContent(content);
            _tilemapProps.LoadContent(content);
        }

        /// <summary>
        /// Updates background
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public void Update(GameTime gameTime, Player player)
        {
            _player = player;

            for (int i = 0; i < 5; i++)
            {
                _enemy2[i].Update(gameTime, _player);
            }
        }

        /// <summary>
        /// Draws the sprites in the order sky-> gorund + player -> grass
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        /// <param name="spriteBatch">The SpriteBatch to draw the player with</param>
        /// <param name="player">Player in the world</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Player player, Enemy enemy)
        {
            float playerX = MathHelper.Clamp(player.Position.X, 300, Constants.GAME_MAX_WIDTH);
            float offsetX = 300 - playerX;

            Matrix transform;

            // Background Sky

            spriteBatch.Begin(samplerState: SamplerState.LinearWrap);
            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(_sky,
                   new Rectangle(i * Constants.GAME_WIDTH, 0, Constants.GAME_WIDTH, Constants.GAME_HEIGHT),
                  new Rectangle(0, 0, _sky.Width, _sky.Height),
                 Color.White);
            }

            spriteBatch.End(); 

            //Ground Layer
            transform = Matrix.CreateTranslation(offsetX, 0, 0);

            spriteBatch.Begin(transformMatrix: transform);

            for (int i = 0; i < 5; i++)
            {
                spriteBatch.Draw(_ground,
                  new Rectangle(i * Constants.GAME_WIDTH, 0, Constants.GAME_WIDTH, Constants.GAME_HEIGHT -150),
                  new Rectangle(0, 0, _ground.Width, _ground.Height),
                 Color.White) ;
            }

            player.Draw(gameTime, spriteBatch);
            enemy.Draw(gameTime, spriteBatch);

            for (int i = 0; i < 5; i++)
            {
                _enemy2[i].Draw(gameTime, spriteBatch);
            }

            _tilemap.Draw(gameTime, spriteBatch);
            _tilemapProps.Draw(gameTime, spriteBatch);

            spriteBatch.End();



            /**
            //Grass layer
            transform = Matrix.CreateTranslation(offsetX * 1.25f, 0, 0);


            spriteBatch.Begin(transformMatrix: transform, samplerState: SamplerState.LinearWrap);
            // _spriteBatch.Draw(_ground, Vector2.Zero, Color.White);

            player.Draw(gameTime, spriteBatch);

            for (int i = 0; i < 6; i++)
            {
                spriteBatch.Draw(_grass,
                  new Rectangle(i * Constants.GAME_WIDTH, 0, Constants.GAME_WIDTH, Constants.GAME_HEIGHT),
                  new Rectangle(0, 0, 272, 160),
                 Color.White);
            }
            spriteBatch.End();
            */
        }

    }
}
