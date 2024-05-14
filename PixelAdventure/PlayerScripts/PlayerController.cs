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
        public Player Player { get; private set; }
        public PlayerController()
        {
            Player = new Player(0, 1000);
            Viewer = new PlayerViewer();
        }

        public void Update(Platform[] platforms, List<Coin> coins, float gravity)
        {
            Player.Vector.Y += gravity;

            Player.Move();

            Player.CollideWithPlatforms(platforms, gravity);

            Player.CollideWithCoins(coins);
        }

        public void AnimationController(GameTime gameTime)
        {
            if (Player.IsMove)
            {
                Viewer.currentAnimation = Viewer.walk;
                Viewer.currentAnimation.StartAnimation(gameTime);
            }
            else if (!Player.IsMove)
            {
                Viewer.currentAnimation = Viewer.idle;
                Viewer.currentAnimation.StartAnimation(gameTime);
            }
            else if (Player.IsJump)
            {
                Viewer.currentAnimation = Viewer.jump;
                Viewer.currentAnimation.StartAnimation(gameTime);
            }
        }

        public void AnimationGo(SpriteBatch _spriteBatch)
        {

            if (!Player.GoLeft && Player.IsMove && !Player.IsJump)
                Viewer.DrawWalkRight(_spriteBatch, new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10));
            else if (Player.GoLeft && Player.IsMove && !Player.IsJump)
                Viewer.DrawWalkLeft(_spriteBatch, new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10));
            else if (!Player.IsMove && !Player.GoLeft && !Player.IsJump)
                Viewer.DrawIdleRight(_spriteBatch, new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10));
            else if (!Player.IsMove && Player.GoLeft && !Player.IsJump)
                Viewer.DrawIdleLeft(_spriteBatch, new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10));

            if (Player.IsJump && Player.GoLeft)
                Viewer.DrawJumpLeft(_spriteBatch, new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10));
            else if (Player.IsJump && !Player.GoLeft)
                Viewer.DrawJumpRight(_spriteBatch, new Rectangle((int)Player.Vector.X, (int)Player.Vector.Y - 10, Player.Size.X + 10, Player.Size.Y + 10));
        }
    }
}
