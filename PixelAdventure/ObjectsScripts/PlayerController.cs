using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.ObjectsScripts
{
    internal class PlayerController
    {
        public PlayerViewer Viewer;
        public Player player;
        private Rectangle rect;
        public PlayerController(SpriteBatch _spriteBatch)
        {
            player = new Player(10, 800);
            rect = new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10);
            Viewer = new PlayerViewer(rect);
        }

        public void Update(GameTime gameTime, Platform[] platforms, List<Coin> coins, /*List<Enemy> enemies,*/ float gravity) 
        {
            //if (player.IsFall)
            //{
            //    gravity += 2f;
                player.Vector.Y += gravity;
            //}

            player.Move(gameTime);

            player.CollideWithPlatforms(platforms, gravity, gameTime);

            player.CollideWithCoins(coins);
        }

        public void AnimationController(GameTime gameTime)
        {
            if (player.IsMove)
            {
                Viewer.currentAnimation = Viewer.walk;
                Viewer.currentAnimation.StartAnimation(gameTime);
            }
            else if (!player.IsMove)
            {
                Viewer.currentAnimation = Viewer.idle;
                Viewer.currentAnimation.StartAnimation(gameTime);
            }
        }

        public void AnimationGo(SpriteBatch _spriteBatch, Rectangle drawingRectangle)
        {
            if (!player.GoLeft && player.IsMove)
                Viewer.DrawWalkRight(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
            else if (player.GoLeft && player.IsMove)
                Viewer.DrawWalkLeft(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
            else if (!player.IsMove && !player.GoLeft)
                Viewer.DrawIdleRight(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
            else if (!player.IsMove && player.GoLeft)
                Viewer.DrawIdleLeft(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
        }

        
    }
}
