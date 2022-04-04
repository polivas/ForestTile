﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ForestTile
{

    public enum ActionMode
    {
        Idle = 0,
        Right = 2,
        Left = 2,
        Attack = 3,
        Dead = 4
    }


    public class Enemy
    {
        Texture2D texture;
        Rectangle source;
        Vector2 position;
        Vector2 orgin;
        float rotation = 0f;
        Vector2 velocity;

        bool right;
        bool flipped;
        float distance;
        float oldDistance;
        float playerDistance;

        /// <summary>
        /// Actions of enemy
        /// </summary>
        public ActionMode ActionMode;

        /// <summary>
        /// Timer for animation sequence
        /// </summary>
        private double animationTimer;

        /// <summary>
        /// Frame for animations
        /// </summary>
        private short animationFrame = 0;


        public Enemy(Texture2D newTexture, Vector2 newPosition, float newDistance)
        {
            texture = newTexture;
            position = newPosition;
            distance = newDistance;

            oldDistance = distance;

            //position.Y += 50;
        }

        /// <summary>
        /// Loads the player texture atlas
        /// </summary>
        /// <param name="content">The ContentManager to use to load the content</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("rat and bat spritesheet calciumtrice");
        }

        public void Update(GameTime gameTime, Player player)
        {
            position += velocity;

            orgin = new Vector2(texture.Width / 2, texture.Height / 2);

            //flipped = player.Flipped;

            if (distance <= 0)
            {
                right = true;
            }
            else if (distance >= oldDistance)
            {
                right = false;
                velocity.X = -1f;
            }

            if (right) distance += 1; else distance -= 1;

            MouseState mouse = Mouse.GetState();

            playerDistance = (player.Position.X - position.X);
            playerDistance += 150;

            if (playerDistance >= -200 & playerDistance <= 200)
            {
                if (playerDistance < -1)
                {
                    velocity.X = -1f;
                    ActionMode = ActionMode.Left;
                }
                else if (playerDistance > 1)
                {
                    velocity.X = 1f;
                    ActionMode = ActionMode.Right;
                }
                else if (playerDistance == 0)
                {
                    velocity.X = 0f;
                    ActionMode = ActionMode.Attack;
                }//else actionmode.idle ?
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

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

            var source = new Rectangle(animationFrame * 32, (int)ActionMode * 32, 32, 32);
            //SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            if (velocity.X > 0)
            {
                spriteBatch.Draw(texture, position, source, Color.White, rotation, orgin, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
            /*else if(velocity.X == 0)
            {
               // source = new Rectangle(animationFrame * 32, 0 * 32, 32, 32);
               // spriteBatch.Draw(texture, position, source, Color.White, 0f, new Vector2(32, 32), 1.5f, SpriteEffects.None, 0);
            }*/
            else
            {
                spriteBatch.Draw(texture, position, source, Color.White, rotation, orgin, 1f, SpriteEffects.None, 0f);
            }

        }
    }
}
