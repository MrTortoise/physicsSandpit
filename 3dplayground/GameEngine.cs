using System;
using System.Collections.Generic;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using _3dplayground.Graphics;
using _3dplayground.Physics;

namespace _3dplayground
{
    /// <summary>
    /// This is the game engine. All objecs to be constructed and loaded externally..
    /// I call this game engine, but really its just the 3d part of the game.
    /// The 2D UI will have its own engine for example.
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

        private bool mIsRunning = true;
        private DateTime mLastUpdateTime = DateTime.Now ;

        public GameEngine(GameSpaceUnit GameSpace,Camera theCamera)
        {
            mSpace = GameSpace;
            mPhysics = new GameSpacePhysics(mSpace);
            mGraphics = new GameGraphicsManager(mSpace);
            mCamera = theCamera;
        }

        public void StartUpdate()
        {
            mLastUpdateTime = DateTime.Now;


        }

        public void StopUpdate()
        {

        }

        public  void Update()
        {
            DateTime Current;
            TimeSpan theTimePeriod;
           // while (mIsRunning)
           // {
                Current = DateTime.Now;
                theTimePeriod = Current - mLastUpdateTime;                
                float updateTime = ((float)theTimePeriod.Ticks) / Constants.TimeScale;
                mPhysics.Update(updateTime, mBuffer);
                mBuffer.UpdateFinished();
                mLastUpdateTime = Current;
          //  }
        }

        public void Draw(float timeSpan)
        {
            mGraphics.Draw(timeSpan, mCamera);
        }






    }
}
