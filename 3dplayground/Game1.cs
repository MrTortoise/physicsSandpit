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



namespace _3dplayground
{
    

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        GraphicsDeviceManager graphics;
       // SpriteBatch spriteBatch;

        GameSpaceUnit mObjects;

        //phys_planet mSphere = new phys_planet(1000000, Vector3.Zero);
       // List<phys_planet> theSphereList = new List<phys_planet>();   

        private Camera mCamera = new Camera();
       // private Matrix gameWorldRotation = 0.0f;

        EventManager mEventManager = EventManager.GetInstance();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.SynchronizeWithVerticalRetrace = false;


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
            theSphere = new BasicModel();
            theSphere.LoadContent(Content, "sphere");

            Planet mPlanet;
            mPlanet = new Planet(theSphere,mObjects, "planet1", 1000000000, Vector3.Zero, Vector3.Zero, Quaternion.Identity,Quaternion.Identity );

            mObjects.AddGameObject(mPlanet);

            IFieldPhysics  mFPC;
            mFPC=new FieldPhysicsComponent();

            Moon mMoon;
            mMoon = new Moon(theSphere, mFPC,mObjects, "Moon1", 100000,new Vector3(100,0,0), new Vector3(0,1,0), Quaternion.Identity,Quaternion.Identity );
            mObjects.AddGameObject(mMoon);
           /*
            Moon mMoon2;
            mMoon2 = new Moon(theSphere, mFPC, "Moon2", 100000000, new Vector3(0, 10, 0), new Vector3(-0.25f, 0,0 ), Quaternion.Identity);
            mObjects.AddGameObject(mMoon2);

            Moon mMoon3;
            mMoon3 = new Moon(theSphere, mFPC, "Moon23", 100000000, new Vector3(0, 0, 10), new Vector3(0, -0.25f, 0), Quaternion.Identity);
            mObjects.AddGameObject(mMoon3);

            Moon mMoon4;
            mMoon4 = new Moon(theSphere, mFPC, "Moon24", 100000000, new Vector3(10, 10, 0), new Vector3(-0.25f, -0.25f, 0), Quaternion.Identity);
            mObjects.AddGameObject(mMoon4);

            Moon mMoon5;
            mMoon5 = new Moon(theSphere, mFPC, "Moon5", 100000000, new Vector3(0, 10, 10), new Vector3(-0.25f, 0, -0.25f), Quaternion.Identity);
            mObjects.AddGameObject(mMoon5);
                
             */
            mCamera.AspectRatio=graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            mCamera.FarClippingPlane=10000.0f;
            mCamera.FieldOfView=MathHelper.ToRadians(30f);
            mCamera.NearClippingPlane = 1.0f;
            mCamera.Position = new Vector3(0, 0, 400);
            mCamera.Target = Vector3.Zero;
            mCamera.UpVector = Vector3.UnitY ;     

                     
        
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
        protected override void Update(GameTime gameTime)
        {

            mEventManager.ProcessInput();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mObjects.UpdateSpace(DateTime.Now);            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black );             

            foreach ( IDrawable d in mObjects.DrawableObjects.Values )
            {
                d.Draw(mCamera);
            }  

            base.Draw(gameTime);

            //I could of used this code here - Just to demonstrate interfaces
            //foreach (IDrawable d in mObjects.InSpaceObjects.Values)
            //{
            //    d.Draw(mCamera);
            //}  
            // Even though objects have been added to a list of interfaces that has nothing to do with the IDrawable Interface
            // I can still pull the objects that implement IDrawable out of the list.
            // I elected not to do this as it would have to traverse the whole list every time - better to do this once.

        }

        // I really don't get how your thing works
        // If the way i have exposed things isn't what you need 

        // you can do all of this in 1 for each loop i think
  
    /*


      */


    }
}
