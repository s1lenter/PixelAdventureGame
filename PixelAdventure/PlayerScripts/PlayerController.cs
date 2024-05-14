using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelAdventure.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure.PlayerScripts
{
    internal class PlayerController
    {
        public PlayerViewer Viewer;
        public Player player;
        private Rectangle rect;
        public PlayerController()
        {
            player = new Player(0, 1000);
            rect = new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10);
            Viewer = new PlayerViewer(rect);
        }

        public void Update(GameTime gameTime, Platform[] platforms, List<Coin> coins, float gravity)
        {
            player.Vector.Y += gravity;

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
            else if (player.IsJump)
            {
                Viewer.currentAnimation = Viewer.jump;
                Viewer.currentAnimation.StartAnimation(gameTime);
            }
        }

        public void AnimationGo(SpriteBatch _spriteBatch, Rectangle drawingRectangle)
        {

            if (!player.GoLeft && player.IsMove && !player.IsJump)
                Viewer.DrawWalkRight(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
            else if (player.GoLeft && player.IsMove && !player.IsJump)
                Viewer.DrawWalkLeft(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
            else if (!player.IsMove && !player.GoLeft && !player.IsJump)
                Viewer.DrawIdleRight(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
            else if (!player.IsMove && player.GoLeft && !player.IsJump)
                Viewer.DrawIdleLeft(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));

            if (player.IsJump && player.GoLeft)
                Viewer.DrawJumpLeft(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
            else if (player.IsJump && !player.GoLeft)
                Viewer.DrawJumpRight(_spriteBatch, new Rectangle((int)player.Vector.X, (int)player.Vector.Y - 10, player.Size.X + 10, player.Size.Y + 10));
        }
    }
}
