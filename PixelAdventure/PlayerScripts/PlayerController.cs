using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelAdventure.ObjectsScripts.OtherObjects;
using PixelAdventure.ObjectsScripts.Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PixelAdventure
{
    internal class PlayerController
    {
        public PlayerViewer Viewer { get; private set; }
        public Player player { get; private set; }
        public PlayerController()
        {
            player = new Player(0, 1000);
            Viewer = new PlayerViewer();
        }

        public void Update(Platform[] platforms, List<Coin> coins, float gravity)
        {
            player.Vector.Y += gravity;

            player.Move();

            player.CollideWithPlatforms(platforms, gravity);

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

        public void AnimationGo(SpriteBatch _spriteBatch)
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
