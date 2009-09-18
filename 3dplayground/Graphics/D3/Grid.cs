using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _3dplayground.Maths;

namespace _3dplayground.Graphics.D3
{
    /// <summary>
    /// This will draw a grind in 3D space on the x,y axis with lines a given distance apart for each axis.
    /// </summary>
    class Grid  
    {
        private bool mIsVisible = true;

        private int mXInterval = 10;
        private int mYInterval = 10;

        private int mWidth = 1000;
        private int mLength = 1000;

        private Color mColour = Color.White;

        private string mName = "Grid";
        private int mID = GlobalIDGenerator.GetNextID();

        VertexPositionColor[] mVerticies;
        int mNoVerticies;

        short[] mLineList;
        int mNoLines;
        


        public Grid()
        {
        
        }

        #region Properties
        /// <summary>
        /// The distance between each grid line
        /// </summary>
        public int xGridInterval
        {
            get { return mXInterval; }
            set { mXInterval = value; }
        }
        /// <summary>
        /// The distance between each grid line
        /// </summary>
        public int yGridInterval
        {
            get { return mYInterval; }
            set { mYInterval = value; }
        }
        /// <summary>
        /// The size of one of the grids dimensions
        /// </summary>
        public int Width
        {
            get { return mWidth; }
            set { mWidth = value; }
        }
        /// <summary>
        /// The size of one of the grids dimensions
        /// </summary>
        public int Length
        {
            get { return mLength; }
            set { mLength = value; }
        }
        /// <summary>
        /// Wether or not the grid will be drawn
        /// </summary>
        public bool IsVisible
        {
            get { return mIsVisible ; }
            set { mIsVisible = value; }
        }
        /// <summary>
        /// The colour the grid will be drawn with
        /// </summary>
        public Color Colour
        {
            get { return mColour; }
            set { mColour = value; }
        }

        #endregion



        public void Draw(Vector3 Position, GraphicsDevice gd, Camera thecamera)
        {
            //tranform the verticies to the position the camera is at   
            gd.VertexDeclaration = new VertexDeclaration(gd, VertexPositionColor.VertexElements);
           // gd.RenderState.PointSize = 10;

            BasicEffect temp = new BasicEffect(gd, null);
            temp.VertexColorEnabled = true; 

            temp.World = Matrix.CreateTranslation(Vector3.Zero );

           // temp.World = Matrix.CreateTranslation(Position);
            temp.View = Matrix.CreateLookAt(
               thecamera.Position   ,
               thecamera.Target ,
               thecamera.UpVector 
               );

            temp.Projection = thecamera.Projection;
                
                /*Matrix.CreateOrthographicOffCenter(
                0,
                (float)gd.Viewport.Width,
                (float)gd.Viewport.Height,
                0,
                1.0f, 1000.0f);
                  */
            temp.Begin();
            foreach (EffectPass pass in temp.CurrentTechnique.Passes)
            {
                pass.Begin();

                gd.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.LineList,
                    mVerticies,
                    0,  // vertex buffer offset to add to each element of the index buffer
                    mNoVerticies,  // number of vertices in pointList
                    mLineList,  // the index buffer
                    0,  // first index element to read
                    mNoLines   // number of primitives to draw
                    );

                pass.End();

            }
            temp.End();
        }

        /// <summary>
        /// If the grids properties are changed this should be run after all changes are oomplete to regenerate the grid.
        /// </summary>
        public  void ConstructGrid()
        {
            int NoAccross = mWidth / mXInterval;
            int NoAlong = mLength / mYInterval;

            //construct the Point arrays for its sides
            //we know that each vertex pair starts on te even numbers (inc 0)

            mVerticies  = new VertexPositionColor[(NoAccross+NoAlong)*2];
            

            for (int x = 0; x<NoAccross ; x++)
            {
                mVerticies[2 * x] = new VertexPositionColor(new Vector3((float)x * mXInterval /*- ((float)mWidth / 2f)*/,0, 0), Color.Yellow );
                mVerticies[(2 * x) + 1] = new VertexPositionColor(new Vector3((float)x * mXInterval /*- ((float)mWidth / 2f)*/,(float)mLength, 0), Color.Blue );
            }

            for (int y = NoAccross; y < (NoAlong + NoAccross); y++)
            {
                mVerticies[2 * y] = new VertexPositionColor(new Vector3(0, (float)(y-NoAccross) * mYInterval, 0), Color.White);
                mVerticies[(2 * y) + 1] = new VertexPositionColor(new Vector3((float)mWidth, (float)(y-NoAccross) * mYInterval, 0), Color.Green );
            }

            mNoVerticies = (NoAccross + NoAlong) * 2;

            //Join up the dots to get the lines

            mLineList = new short[(NoAccross + NoAlong)*2];

            for (int i = 0; i < (NoAccross + NoAlong)*2; i++)
            {
                mLineList[i] = (short)i;
            }
            mNoLines = NoAccross + NoAlong;

            
        }


    }
}
