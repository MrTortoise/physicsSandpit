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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using _3dplayground.Graphics.D3;
using _3dplayground.Physics;
using _3dplayground.EventManagement;
using _3dplayground.Maths;
using _3dplayground.Ships;  

namespace _3dplayground
{
    

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        GraphicsDeviceManager graphics;
        GameSpaceUnit mObjects;
        GameEngine mEngine;

        private Camera mCamera = new Camera();
        EventManager mEventManager = EventManager.GetInstance();
        private Player mPlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 1024;          
            mObjects = new GameSpaceUnit();
            Components.Add(new CommonObjects.Components.FPS(this));
        
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

            base.Initialize();         


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw 2D textures.
           // spriteBatch = new SpriteBatch(GraphicsDevice);

            

            BasicModel  theSphere;
            theSphere = new BasicModel("BasicSphere","sphere");
            theSphere.LoadContent(Content);

            Planet mPlanet;
            mPlanet = new Planet(theSphere, mObjects, "planet1", 1000000000, DVector3.Zero, DVector3.Zero, Quaternion.Identity, Quaternion.Identity);

            mObjects.AddGameObject(mPlanet);

           // mPlanet = new Planet(theSphere, mObjects, "planet2", 1000000000, new DVector3(0, 100, 0), DVector3.Zero, Quaternion.Identity, Quaternion.Identity);

        //    mObjects.AddGameObject(mPlanet);
    //mPlanet = new Planet(theSphere, mObjects, "planet3", 1000000000, new DVector3(0, 0, 100), DVector3.Zero, Quaternion.Identity, Quaternion.Identity);

         //   mObjects.AddGameObject(mPlanet);

          
            IFieldPhysics  mFPC;
            mFPC=new FieldPhysicsComponent();

           Moon mMoon;
            mMoon = new Moon(theSphere, mFPC, mObjects, "Moon1", 100000, new DVector3(100, 0, 0), new DVector3(0, 0, 20), Quaternion.Identity, Quaternion.Identity,DVector3.UnitZ );
            mObjects.AddGameObject(mMoon);
           
            Moon mMoon2;
            mMoon2 = new Moon(theSphere, mFPC, mObjects, "Moon2", 100000, new DVector3(100, 100, 0), new DVector3(0, 20, 0), Quaternion.Identity, Quaternion.Identity, DVector3.UnitZ);
            mObjects.AddGameObject(mMoon2);

            Moon mMoon3;
            mMoon3 = new Moon(theSphere, mFPC, mObjects, "Moon3", 100000, new DVector3(200, 0, 100), new DVector3(0, 0, 20), Quaternion.Identity, Quaternion.Identity, DVector3.UnitZ);
            mObjects.AddGameObject(mMoon3);

            Moon mMoon4;
            mMoon4 = new Moon(theSphere, mFPC, mObjects, "Moon4", 100000, new DVector3(100, 100, 100), new DVector3(20, 0, 0), Quaternion.Identity, Quaternion.Identity, DVector3.UnitZ);
            mObjects.AddGameObject(mMoon4);

            Moon mMoon5;
            mMoon5 = new Moon(theSphere, mFPC, mObjects, "Moon5", 100000, new DVector3(200, 100, 0), new DVector3(0, 20, 0), Quaternion.Identity, Quaternion.Identity, DVector3.UnitZ);
            mObjects.AddGameObject(mMoon5);
                
             
            mCamera.AspectRatio=graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            mCamera.FarClippingPlane=10000.0f;
            mCamera.FieldOfView=MathHelper.ToRadians(30f);
            mCamera.NearClippingPlane = 1.0f;
            mCamera.Position = new Vector3(500, 500, 100);
            mCamera.Target = new Vector3(0, 0, 100);
            mCamera.UpVector = Vector3.UnitZ  ;
            mCamera.CameraMode = CameraMode.Attached;
           
            BasicModel shipModel = new BasicModel("theShip","Ship");
            shipModel.LoadContent(Content);
            BasicEngine shipEngine = new BasicEngine("basicEngine",1000.0f,0.001f,1000.0f,10000000.0f,10);
            Ship theShip = new Ship("testShip", mObjects, 1000, new DVector3(500, 500, 000), DVector3.Zero, new Quaternion(new Vector3(-1,-1,0),1),Quaternion.Identity,
                DVector3.UnitZ, shipModel, mFPC, shipEngine);
            mObjects.AddGameObject(theShip);

            mPlayer = new Player(mCamera);
            mPlayer.SetShip(theShip);
            mPlayer.SubscribeToInputEvents();

            mCamera.Compile();

            mEngine = new GameEngine(mObjects, mPlayer);

                     
        
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // dont need to unload anything from game1.cs atm - in full game this will be very importnat.
            // But i need to of written the IDispose interface for the game ... we probably need a reference counter etc
            // in order to aggressivley garbage collect to compensate for .net expensive collections.
            // the problem is that .net uses an entire thread for this stuff.
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected  override void Update(GameTime gameTime)
        {

           // mEventManager.ProcessInput();
            // Allows the game to exit
          //  if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            mEngine.Update();
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black );
            mEngine.Draw(gameTime.ElapsedRealTime.Ticks /Config.TimeScale );            

            base.Draw(gameTime);

        }




    }
}
