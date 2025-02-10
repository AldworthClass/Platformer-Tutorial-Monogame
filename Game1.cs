using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Platformer_Tutorial_Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        KeyboardState keyboardState, prevKeyboardState;

        Texture2D rectangleTexture;
        Rectangle player;
        Vector2 speed;

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
            player = new Rectangle( 10, 10, 50, 50);
            platforms = new List<Rectangle>();
            platforms.Add(new Rectangle(0, 400, 800, 20));

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
            speed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A))
                speed.X += -1f;
            if (keyboardState.IsKeyDown(Keys.D))
                speed.X += 1f;
            
            
            // TODO: Add your update logic here
            player.Offset(speed);

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