using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using _3dplayground.Graphics;
using _3dplayground.Graphics.D3;

using Microsoft.Xna.Framework;


namespace _3dplayground
{
    /// <summary>
    /// This class basically wraps the calls to the drawing buffer. 
    /// </summary>
    /// <remarks>This means that we can change the buffer later, update this and it should not impact anything else.</remarks>
    sealed class GameGraphicsManager
    {
        Player mPlayer;
        Grid mGrid = new Grid();
        GraphicsDeviceManager mGraphics;
       
        DrawingBufferManager mBuffer = DrawingBufferManager.GetInstance();

        /// <summary>
        /// Initialises the object responsible for all of the 3D drawing an dpossibly 2D drawing
        /// </summary>
        /// <param name="theSpace">This is the gamespace unit collection</param>
        /// <param name="thePlayer"></param>
        public GameGraphicsManager(Player thePlayer, GraphicsDeviceManager gdm)
        {
           
            mPlayer = thePlayer;
            mGraphics=gdm;
            mGrid.ConstructGrid();
            
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
                // and also simply sends its input to the server. This way we ensure trust in our data - or more ot the point, trivialise trust.
                // Once this happens the problem changes into figuring out what information to send to a player via netcode,
                // the client will then not have iterate through the entire set of game objects - just its internal collection.
                // This is simply another instance of the problem that gameSapceUnit is trying to solve.

                foreach (IGameDrawable d in mPlayer.Ship.Space.DrawableObjects.Values)
                {
                    d.Draw(timeSpan, theCamera);
                }
                
                //Draw any 3D UI Elements
                mGrid.Draw(mPlayer.Ship.DrawPosition,mGraphics.GraphicsDevice,mPlayer.Camera  );

            }



        }


        
    }
}
