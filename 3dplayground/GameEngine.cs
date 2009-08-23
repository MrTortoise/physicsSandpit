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
    /// </summary>
    sealed  class GameEngine                                           
    { 
       

        private Dictionary<int, IHasName> mObjects = new Dictionary<int, IHasName>();

        private GameSpaceUnit mSpace = new GameSpaceUnit();

        private DrawingBufferManager mBuffer = DrawingBufferManager.GetInstance();

        private GameGraphicsManager mGraphics;
        private GameSpacePhysics  mPhysics;

        private Camera mCamera;

        public GameEngine(GameSpaceUnit GameSpace,Camera theCamera)
        {
            mSpace = GameSpace;
            mPhysics = new GameSpacePhysics(mSpace);
            mGraphics = new GameGraphicsManager(mSpace);
            mCamera = theCamera;
        }

        public void Update(float timePeriod)
        {
            mSpace.UpdateSpace(timePeriod, mBuffer);
            mBuffer.UpdateFinished();
        }

        public void Draw(float timeSpan)
        {
            mGraphics.Draw(timeSpan, mCamera);
        }






    }
}
