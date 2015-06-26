using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Jogo.classes;


namespace Jogo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {   
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int ScreenWidht;
        public static int ScreenHeight;
        public SpriteFont font;
        public static SoundEffect soundToc1 = null;
        SoundEffect soundToc2 = null;
  

        const int PADDLE_OFFSET = 70;
        const float BALL_START_SPEED = 8f;
          public static Player player1;
          public static Player player2;
              Ball ball;

        public Game1()
        {
            this.Window.Title = "Pong";
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenWidht = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            player1 = new Player();
            player2 = new Player();
            ball = new Ball();
            ball.speed = 1.0f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundToc1 = Content.Load<SoundEffect>("PongAssets/toc_2");
            // TODO: use this.Content to load your game content here
            soundToc2 = Content.Load<SoundEffect>("PongAssets/Pontos");
            player1.Texture = Content.Load<Texture2D>("PongAssets/Paddle");
            player2.Texture = Content.Load<Texture2D>("PongAssets/Paddle");
        
            font = Content.Load<SpriteFont>("PongAssets/font");
            player1.Position = new Vector2(PADDLE_OFFSET, ScreenHeight/2 - player1.Texture.Height / 2);
            player2.Position = new Vector2(ScreenWidht - player2.Texture.Width - PADDLE_OFFSET, ScreenHeight/2 - player2.Texture.Height/2 );

            ball.Texture = Content.Load<Texture2D>("PongAssets/Ball");
            ball.Launch(BALL_START_SPEED);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            ball.speed *= 1.0f;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            ScreenWidht = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ball.Move(ball.Velocity);
            ball.CheckWallCollision();
            ball.CheckPlayerCollision(player1);
            ball.CheckPlayer2Collision(player2);
            if (ball.Position.X + ball.Texture.Width < 0)
            { 
                
                ball.Launch(BALL_START_SPEED); player2.ScoreUp();
                soundToc2.Play();
            }

            if (ball.Position.X > ScreenWidht)

            {
                ball.Launch(BALL_START_SPEED); player1.ScoreUp();
                soundToc2.Play();
            }

           
            
            if(ball.Position.X + ball.Texture.Width < 0)
            {
                ball.Launch(BALL_START_SPEED);
            
            }
            if(ball.Position.X > ScreenWidht)
            {
                ball.Launch(BALL_START_SPEED);
            }

      
            KeyboardState keyState = Keyboard.GetState();
           
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();

           
           
            //
            if(player1.Position.Y >= 0 )
            {
                if (keyState.IsKeyDown(Keys.W))
                    player1.Position.Y -= 10.0f;
            }
            if (player1.Position.Y + 192/2  <= 370)
            {
                if (keyState.IsKeyDown(Keys.S))
                    player1.Position.Y += 10.0f;
            }

            if (player2.Position.Y >= 0)
            {
                
            if (keyState.IsKeyDown(Keys.Up))
                player2.Position.Y-=10.0f;
            }
            if (player2.Position.Y + 192 / 2 <= 370)
            {
                if (keyState.IsKeyDown(Keys.Down))
                    player2.Position.Y += 10.0f;
            }
          
            
          
            if (ball.Position.X  == player1.Position.X + 24)
            {
                // Inverte a direção X da bola
                ball.Position.X *= -1.0f;
            }
            // Jogador 2 (Raquete da direita)
            if (ball.Position.X == player2.Position.X + 24)
            {
                // Inverte a direção X da bola
                ball.Position.X *= -1.0f;
            }
            player1.Update(gameTime);




            player2.Update(gameTime);


            ball.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           

            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, player1.score.ToString(), new Vector2(250, 30), Color.Black);
            spriteBatch.DrawString(font, player2.score.ToString(), new Vector2(510, 30), Color.White);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
