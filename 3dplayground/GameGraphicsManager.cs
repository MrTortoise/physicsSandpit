using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using _3dplayground.Graphics;
using _3dplayground.Graphics.D3;


namespace _3dplayground
{
    class GameGraphicsManager
    {

        GameSpaceUnit mSpace;
        DrawingBufferManager mBuffer = DrawingBufferManager.GetInstance();

        public GameGraphicsManager(GameSpaceUnit theSpace)
        {
            mSpace = theSpace;
        }   

        /// <summary>
        /// Loads the next drawing buffer, applies it and then draws the IGameDrawable objects using the camera.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="theCamera"></param>
        public void Draw(float timeSpan, Camera theCamera)
        {
            waitForNextFrame();


            mBuffer.ApplyDrawBuffer();

            // now we can draw everything using the given camera
            
            foreach (IGameDrawable   d in mSpace.DrawableObjects.Values  )
            {
                d.Draw(timeSpan, theCamera);
            }

        }

        protected void waitForNextFrame()
        {
            while (mBuffer.IsFrameReady == false)
            {
                
            }

        }
        
    }
}
