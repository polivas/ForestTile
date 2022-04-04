using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;



namespace ForestTile
{
    public enum TextureMode
    {
        idle = 0,
        Right = 2,
        Left = 2,
        Attack = 3
    }

    public class Player
    {
        // The texture atlas for Mage Character
        private Texture2D _texture;

        /// <summary>
        /// the direction of the hunter
        /// </summary>
        public TextureMode TextureMode;

        // The bounds of the player within the texture atlas
        private Rectangle _playerBounds = new Rectangle(0,192,32,32);

        // The position of the player
        private Vector2 _position;

        /// <summary>
        /// The current position of the player
        /// </summary>
        public Vector2 Position => _position;

        /// <summary>
        /// If button is being pressed
        /// </summary>
        private bool pressing = false;

        /// <summary>
        /// Timer for animation sequence
        /// </summary>
        private double animationTimer;

        /// <summary>
        /// Frame for animations
        /// </summary>
        private short animationFrame = 0;

        /// <summary>
        /// Checks if the player is flipped, to flip the drawn sprite
        /// </summary>
        public bool Flipped;

        /// <summary>
        /// Loads the player texture atlas
        /// </summary>
        /// <param name="content">The ContentManager to use to load the content</param>
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("wizard spritesheet");
        }

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            var keyboardState = Keyboard.GetState();
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            pressing = false;


            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                _position += new Vector2(-1, 0);
                TextureMode = TextureMode.Left;
                Flipped = true;
                pressing = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                _position += new Vector2(1, 0);
                TextureMode = TextureMode.Right;
                Flipped = false;
                pressing = true;
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                TextureMode = TextureMode.Attack;
                pressing = true;
            }else if (mouseState.LeftButton == ButtonState.Released)
            {
                TextureMode = TextureMode.Right;
            }

        }

        /// <summary>
        /// Draws the player sprite
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        /// <param name="spriteBatch">The SpriteBatch to draw the player with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _position.Y = 320;
            // spriteBatch.Draw(_texture, _position, _playerBounds, Color.White);

            //Update animation Timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (animationTimer > 0.3)
            {
                animationFrame++;
                if (animationFrame > 9) animationFrame = 0;
                animationTimer -= 0.3;
            }
            if (animationTimer > 0.3) animationTimer -= 0.3;


            var source = new Rectangle(animationFrame * 32, (int)TextureMode * 32, 32, 32);

            SpriteEffects spriteEffects = (Flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            //spriteBatch.Draw(_texture, Position, source, Color.White, 0f, new Vector2(32, 32), 1.5f, spriteEffects, 0);
           if(!pressing)
            {
                source = new Rectangle(animationFrame * 32, 0 * 32, 32, 32);
                spriteBatch.Draw(_texture, Position, source, Color.White, 0f, new Vector2(32, 32), 1.5f, spriteEffects, 0);
            }
            else
            {
                spriteBatch.Draw(_texture, Position, source, Color.White, 0f, new Vector2(32, 32), 1.5f, spriteEffects, 0);
            }

        }
    }
}
