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

namespace SnakeGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        SpriteFont ControlFont;
        SpriteFont LargeFont;


        KeyboardState oldState;

        Texture2D SnakeBodyTexture;
        Texture2D SnakeHeadTexture;


        Texture2D MonsterTexture;
        Texture2D FoodTexture;

        Player PlayerOne;
        Food Food;







        Label LControls;
        Label LRestart;
        Label LPause;
        Label LArrow;
        Label LMovement;

        Vector2 textCenter;
        Label LGameOver;
        Label LPaused;


        Wall topWall;
        Wall bottomWall;
        Wall leftWall;
        Wall rightWall;






        public Game()
        {
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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 80);



            base.Initialize();

            //StartUp();
        }
        //private void StartUp()
        //{


        //    for (int i = 0; i <= label.Length - 1; i++)
        //    {
        //        label[i].vector = new Vector2(610, (10 + (15 * i)));
        //        label[i].text = "";
        //        label[i].color = Color.Black;
        //        label[i].visible = true;

        //    }





        //    RandomVector(ref Monster.position);

        //    Monster.facing = random.Next(1, 9);

        //    RandomVector(ref Food);

        //}


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SnakeBodyTexture = Content.Load<Texture2D>("snakepart");
            SnakeHeadTexture = Content.Load<Texture2D>("snakehead");
            MonsterTexture = Content.Load<Texture2D>("monster");
            FoodTexture = Content.Load<Texture2D>("food");
            ControlFont = Content.Load<SpriteFont>("Courier New");
            LargeFont = Content.Load<SpriteFont>("Large");
            Texture2D HWallTexture = Content.Load<Texture2D>("HWall");
            Texture2D VWallTexture = Content.Load<Texture2D>("VWall");


            topWall = new Wall(HWallTexture, Vector2.Zero);
            bottomWall = new Wall(HWallTexture, new Vector2(0, 600 - HWallTexture.Height));
            leftWall = new Wall(VWallTexture, Vector2.Zero);
            rightWall = new Wall(VWallTexture, new Vector2(600 - VWallTexture.Width, 0));




            LControls = new Label(new Vector2(610, (10 + (15 * 1))), "Controls:", ControlFont);
            LRestart = new Label(new Vector2(610, (10 + (15 * 2))), "R-Restart", ControlFont);
            LPause = new Label(new Vector2(610, (10 + (15 * 3))), "P-Pause", ControlFont);
            LArrow = new Label(new Vector2(610, (10 + (15 * 4))), "Arrow Keys:", ControlFont);
            LMovement = new Label(new Vector2(610, (10 + (15 * 5))), "Movement", ControlFont);

            textCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, 300f);
            LGameOver = new Label((textCenter - (LargeFont.MeasureString("GAME OVER") / 2)), "GAME OVER", LargeFont, false, Color.Crimson);
            LPaused = new Label((textCenter - (LargeFont.MeasureString("PAUSED") / 2)), "PAUSED", LargeFont, false, Color.Crimson);


            NewPlayer(ref PlayerOne);




            // TODO: use this.Content to load your game content here
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
            CheckRestart();
            if (PlayerOne.CheckSnakeFoodCollision(Food.BBFood))
                Food = new Food(FoodTexture);
            PlayerOne.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        private void CheckRestart()
        {

            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Escape))
            {

                Exit();

            }
            if (newState.IsKeyDown(Keys.R))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.R))
                {

                    NewPlayer(ref PlayerOne);

                }
            }
        }
        private Player NewPlayer(ref Player NewPlayer)
        {
            NewPlayer = new Player(SnakeHeadTexture, SnakeBodyTexture, 5);
            NewPlayer.AddEdges(leftWall.BBox, topWall.BBox, rightWall.BBox2, bottomWall.BBox2);

            Food = new Food(FoodTexture);
            return NewPlayer;

        }

        //private void BeginPause(bool UserInitiated)
        //{
        //    paused = true;
        //    pausedForGuide = !UserInitiated;
        //    label[6].visible = true;

        //    //TODO: Pause audio playback
        //    //TODO: Pause controller vibration
        //}
        //private void EndPause()
        //{
        //    //TODO: Resume audio
        //    //TODO: Resume controller vibration
        //    pausedForGuide = false;
        //    paused = false;
        //    label[6].visible = false;
        //}
        //private void UpdateText()
        //{
        //    if (first == true)
        //    {
        //        label[0].text = "Controls";
        //        label[1].text = "R-Restart";
        //        label[2].text = "P-Pause";
        //        label[3].text = "Arrow Keys:";
        //        label[4].text = "Movement";

        //    label[5].color = Color.Crimson;
        //    label[5].text = "GAME OVER";

        //    Vector2 textSize = LargeFont.MeasureString("GAME OVER");
        //    Vector2 textCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        //    label[5].vector = textCenter - (textSize / 2);
        //    label[5].visible = false;
        //    textSize = LargeFont.MeasureString("PAUSED");
        //    label[6].vector = textCenter - (textSize / 2);
        //    label[6].text = "PAUSED";
        //    label[6].color = Color.Crimson;
        //    label[6].visible = false;
        //    first = false;
        //    }
        //}
        //private void CheckPause()
        //{
        //    KeyboardState newState = Keyboard.GetState();
        //    if (newState.IsKeyDown(Keys.P))
        //    {
        //        // If not down last update, key has just been pressed.
        //        if (!oldState.IsKeyDown(Keys.P))
        //        {
        //            if (!paused)
        //                BeginPause(true);
        //            else
        //                EndPause();
        //        }
        //    }
        //}
        //private void CheckRestart()
        //{

        //    KeyboardState newState = Keyboard.GetState();
        //    if (newState.IsKeyDown(Keys.Escape))
        //    {

        //        Exit();

        //    }
        //    if (newState.IsKeyDown(Keys.R))
        //    {
        //        // If not down last update, key has just been pressed.
        //        if (!oldState.IsKeyDown(Keys.R))
        //        {

        //            StartUp();
        //        }
        //    }
        //}
        //private void UpdateInput()
        //{
        //    KeyboardState newState = Keyboard.GetState();
        //    // Is the UP key down?
        //    if (newState.IsKeyDown(Keys.Left))
        //    {
        //        // If not down last update, key has just been pressed.
        //        if (!oldState.IsKeyDown(Keys.Left))
        //        {
        //            Snake[0].facing = 1;//Left
        //        }
        //    } if (newState.IsKeyDown(Keys.Up))
        //    {
        //        // If not down last update, key has just been pressed.
        //        if (!oldState.IsKeyDown(Keys.Up))
        //        {
        //            Snake[0].facing = 2;//Up
        //        }
        //    }
        //    if (newState.IsKeyDown(Keys.Right))
        //    {
        //        // If not down last update, key has just been pressed.
        //        if (!oldState.IsKeyDown(Keys.Right))
        //        {
        //            Snake[0].facing = 3;//Right
        //        }
        //    }
        //    if (newState.IsKeyDown(Keys.Down))
        //    {
        //        // If not down last update, key has just been pressed.
        //        if (!oldState.IsKeyDown(Keys.Down))
        //        {
        //            Snake[0].facing = 4;//Down
        //        }
        //    }



        // Exits game

        //// Is the SPACE key down?
        //if (newState.IsKeyDown(Keys.Space))
        //{
        //    // If not down last update, key has just been pressed.
        //    if (!oldState.IsKeyDown(Keys.Space))
        //    {
        //        //backColor =
        //        //    new Color(backColor.R, backColor.G, (byte)~backColor.B);
        //    }
        //}
        //else if (oldState.IsKeyDown(Keys.Space))
        //{
        //    // Key was down last update, but not down now, so
        //    // it has just been released.
        //}

        // Update saved state.
        //    oldState = newState;
        //}


        //private void CheckSnakeWallCollision()
        //{
        //    ///TODO: check for wall collions against the snake
        //    ///
        //    BoundingBox BBSnake = new BoundingBox(new Vector3(Snake[0].position.X+1,Snake[0].position.Y+1,0),
        //                                          new Vector3(Snake[0].position.X+9,Snake[0].position.Y+9,0));
        //    switch (Snake[0].facing)
        //    {
        //        case 1:
        //            BoundingBox BBLeftWall = new BoundingBox(new Vector3(leftWall.Position.X+1, leftWall.Position.Y + 1, 0),
        //                                          new Vector3(leftWall.BoundingBox.Width - 1, leftWall.BoundingBox.Height-1, 0));
        //            if (BBSnake.Intersects(BBLeftWall))
        //            {
        //                label[5].visible = true;
        //                paused = true;
        //                gameover= true;

        //            }
        //            break;
        //        case 2:
        //            BoundingBox BBTopWall = new BoundingBox(new Vector3(topWall.Position.X+1, topWall.Position.Y + 1, 0),
        //                                          new Vector3(topWall.BoundingBox.Width - 1, topWall.BoundingBox.Height-1, 0));
        //            if (BBSnake.Intersects(BBTopWall))
        //            {
        //                label[5].visible = true;
        //                paused = true;
        //                gameover = true;
        //            }
        //            break;
        //        case 3:

        //            BoundingBox BBRightWall = new BoundingBox(new Vector3(rightWall.Position.X+1, rightWall.Position.Y + 1, 0),
        //                                          new Vector3(rightWall.Position.X + 9, rightWall.Position.Y + 599, 0));
        //            if (BBSnake.Intersects(BBRightWall))
        //            {
        //                label[5].visible = true;
        //                paused = true;
        //                gameover = true;
        //            }
        //            break;
        //        case 4:
        //            BoundingBox BBBottomWall = new BoundingBox(new Vector3(bottomWall.Position.X+1, bottomWall.Position.Y + 1, 0),
        //                                          new Vector3(bottomWall.Position.X + 599, bottomWall.Position.Y + 9, 0));
        //            if (BBSnake.Intersects(BBBottomWall))
        //            {
        //                label[5].visible = true;
        //                paused = true;
        //                gameover = true;
        //            }
        //            break;

        //    }



        //private void CheckSnakeFoodCollision()
        //{
        //    BoundingBox BBFood = new BoundingBox(new Vector3(Food.X+1, Food.Y+1,0),
        //                                         new Vector3(Food.X + 9, Food.Y + 9, 0));

        //    BoundingBox BBSnake = new BoundingBox(new Vector3(Snake[0].position.X+1,Snake[0].position.Y+1,0),
        //                                          new Vector3(Snake[0].position.X+9,Snake[0].position.Y+9,0));

        //    if (BBSnake.Intersects(BBFood))
        //    {
        //        SnakeLength += 1;
        //        Snake[SnakeLength].position = Snake[SnakeLength - 1].position;
        //        Snake[SnakeLength].facing = Snake[SnakeLength-1].facing;
        //        //Snake[SnakeLength].BBox = new BoundingBox(new Vector3(Snake[SnakeLength].position.X, Snake[SnakeLength].position.Y, 0),
        //                        //new Vector3(Snake[SnakeLength].position.X + 10, Snake[SnakeLength].position.Y + 10, 0));
        //        RandomVector(ref Food);
        //    }

        //    ///TODO: check for wall collions against the snake

        //}

        //private void CheckSnakeMonsterCollision()
        //{
        //    ///TODO: check for wall collions against the snake

        //}
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            topWall.Draw(spriteBatch);
            bottomWall.Draw(spriteBatch);
            leftWall.Draw(spriteBatch);
            rightWall.Draw(spriteBatch);



            if (PlayerOne.Dead)
            {
                LGameOver.Visible = true;
            }
            else
            {
                LGameOver.Visible = false;
                if (PlayerOne.Paused)
                    LPaused.Visible = true;
                else
                    LPaused.Visible = false;
            }
            LControls.Draw(spriteBatch);
            LRestart.Draw(spriteBatch);
            LPause.Draw(spriteBatch);
            LArrow.Draw(spriteBatch);
            LMovement.Draw(spriteBatch);
            LGameOver.Draw(spriteBatch);
            LPaused.Draw(spriteBatch);


            PlayerOne.Draw(spriteBatch);
            Food.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);

        }

    }
}
