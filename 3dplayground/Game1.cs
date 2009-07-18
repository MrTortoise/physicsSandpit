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



namespace _3dplayground
{
    

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameObjectDictionary mObjects;

        phys_planet mSphere = new phys_planet(1000000, Vector3.Zero);
        List<phys_planet> theSphereList = new List<phys_planet>();   

        private Camera mCamera = new Camera();
       // private Matrix gameWorldRotation = 0.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;

            mObjects = GameObjectDictionary.GetInstance();


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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            IModel theSphere;
            theSphere = new Sphere();
            theSphere.LoadContent(Content, "sphere");

            Planet mPlanet;
            mPlanet = new Planet(theSphere, "planet1", 10000000, Vector3.Zero, Vector3.Zero, Quaternion.Identity);

            mObjects.AddGameObject(mPlanet);

            IFieldPhysics  mFPC;
            mFPC=new FieldPhysicsComponent();

            Moon mMoon;
            mMoon = new Moon(theSphere, mFPC, "Moon1", 100000,new Vector3(100,100,100), Vector3.Zero, Quaternion.Identity);

            mObjects.AddGameObject(mMoon);
                

            mCamera.AspectRatio=graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            mCamera.FarClippingPlane=10000.0f;
            mCamera.FieldOfView=MathHelper.ToRadians(45f);
            mCamera.NearClippingPlane = 1.0f;
            mCamera.Position = new Vector3(600, 600, 0);
            mCamera.Target = Vector3.Zero;
            mCamera.UpVector = Vector3.UnitZ;     

                     
        
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            foreach (IUpdateable u in mObjects.UpdateableObjects.Values )
            {
                u.Update(gameTime);
            }

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
  
    /*
        public void RK4(List<phys_planet> some_obj_list,FUNC_PTR diff_func, double step)
        {
            double hstep = step / 2;
            foreach (phys_planet i in some_obj_list) //k1 stage
            {
                i.K1pos = i.speed;
                i.K1vel = gravity_force(some_obj_list, i, i.Position);
            }
            foreach (phys_planet i in some_obj_list) //k2 stage
            {
                i.K2pos = i.K1vel * (float)hstep + i.speed;
                i.K2vel = gravity_force(some_obj_list, i, i.Position + i.K1pos*(float)hstep);
            }

            foreach (phys_planet i in some_obj_list) //k3 stage
            {
                i.K3pos = i.K2vel * (float)hstep + i.speed;
                i.K3vel = gravity_force(some_obj_list, i, i.Position + i.K2pos * (float)hstep);
            }
            foreach (phys_planet i in some_obj_list) //k4 stage
            {
                i.k4pos = i.K3vel * (float)step + i.speed;
                i.k4vel = gravity_force(some_obj_list, i, i.Position + i.K3pos * (float)step);
            }
            foreach (phys_planet i in some_obj_list) //k4 stage
            {
                i.Position = i.Position + (float)(step / 6) * (i.K1pos + i.K2pos / 2 + i.K3pos / 2 + i.k4pos);
                i.speed = i.speed + (float)(step / 6) * (i.K1vel + i.K2vel / 2 + i.K3vel / 2 + i.k4vel);
            }
        }

      */


    }
}
