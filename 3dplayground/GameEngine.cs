using System;
using System.Collections.Generic;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using _3dplayground.EventManagement;
using _3dplayground.Graphics;
using _3dplayground.Physics;
using _3dplayground.Ships;


namespace _3dplayground
{
    /// <summary>
    /// This is the game engine. All objecs to be constructed and loaded externally..
    /// This class will also be running the update thread.
    /// </summary>
    sealed  class GameEngine                                           
    { 
       

        private Dictionary<int, IHasName> mObjects = new Dictionary<int, IHasName>();

        private GameSpaceUnit mSpace = new GameSpaceUnit();

        private DrawingBufferManager mBuffer = DrawingBufferManager.GetInstance();

        private GameGraphicsManager mGraphics;
        private GameSpacePhysics  mPhysics;

        private Camera mCamera;

        private EventManager mEventManager = EventManager.GetInstance();

        private bool mIsRunning = true;
        private DateTime mLastUpdateTime = DateTime.Now ;

        private Player mPlayer;
      

        public GameEngine(GameSpaceUnit GameSpace,Player thePlayer)
        {
            mSpace = GameSpace;
            mPhysics = new GameSpacePhysics(mSpace);
            mGraphics = new GameGraphicsManager(mSpace);
            mCamera = thePlayer.Camera;
            mPlayer = thePlayer;
            thePlayer.SubscribeToInputEvents(); 
        }

        public void StartUpdate()
        {
            mLastUpdateTime = DateTime.Now;  
        }

        public void StopUpdate()
        {

        }

        public void Update()
        {
            DateTime Current;
            TimeSpan theTimePeriod;
            // while (mIsRunning)
            // {
            Current = DateTime.Now;
            theTimePeriod = Current - mLastUpdateTime;
            float updateTime = ((float)theTimePeriod.Ticks) / Config.TimeScale;
            // Process any player input and raises the input events - ai will use this mechanism to pass commands to their vessels
            mEventManager.ProcessInput();
            // Perform the physics update
            mPhysics.Update(updateTime, mBuffer);
            // tell buffer that the update has finished to set the next drawing frame
            mBuffer.UpdateFinished();
            mLastUpdateTime = Current;
            //  }
        }

        public void Draw(float timeSpan)
        {
            mCamera.Compile();
            mGraphics.Draw(timeSpan, mCamera);
        }






    }
}
