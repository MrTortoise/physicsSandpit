using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using _3dplayground.Graphics;
using _3dplayground.Graphics.D3;


namespace _3dplayground
{
    /// <summary>
    /// This class basically wraps the calls to the drawing buffer. 
    /// </summary>
    /// <remarks>This means that we can change the buffer later, update this and it should not impact anything else.</remarks>
    sealed class GameGraphicsManager
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
            if (mBuffer.IsFrameReady)
            {
                mBuffer.ApplyDrawBuffer();

                // now we can draw everything using the given camera

                // At one point is was going to make the camera responsible for the decisions regarding visibility.
                // Howevever I cannot see anyway to avoid this being a n(n) operation - what we can vary is the expense.
                // We cannot do this as part of the update because we ultimatley want to seperate client and server.
                // The server does the update and the client simply recieves the results of that 
                // and also simply sends its input to the server. This way we ensure trust in our data.
                // Once this happens the problem changes into figuring out what information to send to a player via netcode,
                // the client will then not have iterate through the entire set of game objects - just its internal collection.
                // This is simply another instance of the problem that gameSapceUnit is trying to solve.

                foreach (IGameDrawable d in mSpace.DrawableObjects.Values)
                {
                    d.Draw(timeSpan, theCamera);
                }
            }

        }


        
    }
}
