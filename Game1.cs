using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Platformer_Tutorial_Monogame
{
    public enum PlayerState
    {
        OnGround,
        Jumping,
        Falling
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        KeyboardState keyboardState, prevKeyboardState;

        Texture2D rectangleTexture;
        Rectangle player;
        Vector2 playerPosition;
        Vector2 speed;

        float gravity = 0.3f;
        float jumpSpeed = 8f;

        bool onGround = true;


        List<Rectangle> platforms;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            speed = Vector2.Zero;
            playerPosition = new Vector2(10, 10);
            player = new Rectangle(10, 10, 50, 50);
            platforms = new List<Rectangle>();
            platforms.Add(new Rectangle(0, 400, 800, 20));
            platforms.Add(new Rectangle(100, 350, 100, 20));
            platforms.Add(new Rectangle(350, 250, 75, 20));
            //platforms.Add(new Rectangle(0, 400, 800, 20));
            //platforms.Add(new Rectangle(0, 400, 800, 20));



            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("rectangle");
        }

        protected override void Update(GameTime gameTime)
        {
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // Horizontal Movement
            speed.X = 0f;
            if (keyboardState.IsKeyDown(Keys.A))
                speed.X += -2f;
            if (keyboardState.IsKeyDown(Keys.D))
                speed.X += 2f;

            playerPosition.X += speed.X;
            player.Location = playerPosition.ToPoint();
            foreach (Rectangle platform in platforms)
                if (player.Intersects(platform))
                {
                    playerPosition.X -= speed.X;
                    player.Location = playerPosition.ToPoint();
                }


            // Vertical Movement

            if (!onGround)
            {
                speed.Y += gravity;
                if (speed.Y < 0 && keyboardState.IsKeyUp(Keys.Space)) // ends jump early if space is not pressed
                    speed.Y /= 1.5f;
                
            }
            else if (keyboardState.IsKeyDown(Keys.Space) && onGround)
            {
                speed.Y = -jumpSpeed;
                onGround = false;
            }
            else if (onGround)
            {
                speed.Y += gravity;
            }
           


            // TODO: Add your update logic here
            playerPosition.Y += speed.Y;
            player.Location = playerPosition.ToPoint();
            foreach (Rectangle platform in platforms)
                if (player.Intersects(platform)) // Player hits platform
                {
                    if (speed.Y > 0f)// player lands on platform
                    {
                        onGround = true;
                        speed.Y = 0;
                        playerPosition.Y = platform.Y - player.Height;// Set player on platform
                    }
                    else // hits bottom of platform
                    {
                        speed.Y = 0;
                        playerPosition.Y = platform.Bottom;
                    }
                    player.Location = playerPosition.ToPoint();


                }
                
            player.Location = playerPosition.ToPoint();

            foreach(Rectangle platform in platforms)

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(rectangleTexture, player, Color.Red);
            foreach (Rectangle platform in platforms)
                _spriteBatch.Draw(rectangleTexture, platform, Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}