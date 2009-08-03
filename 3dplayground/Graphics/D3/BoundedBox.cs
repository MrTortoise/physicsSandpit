﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _3dplayground.Graphics.D3
{
    /// <summary>
    /// This class assumes that the model is centered at 0,0,0 in model space.
    /// </summary>
    class BoundedBox : BoundedSphere, IHasBoundedBox   
    { 
        BoundingBox mBoundingBox;

        public BoundedBox(string theName)
            : base(theName) { }

        #region ILoadable Members

        public override  void LoadContent(ContentManager  theContentManager, string ContentName)
        {
            base.LoadContent(theContentManager, ContentName);
            CalculateBox();    

            
        }

        #endregion

        #region IHasBoundedBox Members

        public BoundingBox boundingBox
        {
            get { return mBoundingBox; }
        } 

        public Vector3 Volume
        {
            get { return Vector3.Subtract(mBoundingBox.Max, mBoundingBox.Min); }
        }

        public Vector3 Max
        { get { return mBoundingBox.Max; } }

        public Vector3 Min
        { get { return mBoundingBox.Min; } }

        #endregion

        #region IHasBoundedBox Members


        public void CalculateBox()
        {
            //the 2 vecotors that go int the bounding box constructor
            Vector3 Max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            Vector3 Min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

            foreach (ModelMesh mesh in mModel.Meshes)
            {
                // get the transform for each bone in model space.
                Matrix transform = mModel.Bones[mesh.ParentBone.Index].Transform;    // could add world matrix in here for a gereric function
                //iterate through each mesh part to Get at the models VertexBuffer buffers
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    int stride = part.VertexStride;
                    int numberv = part.NumVertices;
                    byte[] vertexData = new byte[stride * numberv];

                    mesh.VertexBuffer.GetData(vertexData);
                    // iterate through the buffer stripping out the floats and then testing for new max and min values to plug into the vectors
                    for (int ndx = 0; ndx < vertexData.Length; ndx += stride)
                    {
                        float floatvaluex = BitConverter.ToSingle(vertexData, ndx);
                        float floatvaluey = BitConverter.ToSingle(vertexData, ndx + 4); // a float is 32 bit or 4 bytes
                        float floatvaluez = BitConverter.ToSingle(vertexData, ndx + 8);
                        Vector3 vectCurrentVertex = new Vector3(floatvaluex, floatvaluey, floatvaluez);
                        Vector3 vectWorldVertex = Vector3.Transform(vectCurrentVertex, transform);

                        if (vectWorldVertex.X < Min.X) Min.X = vectWorldVertex.X;
                        if (vectWorldVertex.X > Max.X) Max.X = vectWorldVertex.X;
                        if (vectWorldVertex.Y < Min.Y) Min.Y = vectWorldVertex.Y;
                        if (vectWorldVertex.Y > Max.Y) Max.Y = vectWorldVertex.Y;
                        if (vectWorldVertex.Z < Min.Z) Min.Z = vectWorldVertex.Z;
                        if (vectWorldVertex.Z > Max.Z) Max.Z = vectWorldVertex.Z;
                    }
                }
            }

            mBoundingBox = new BoundingBox(Min, Max);
        }

        #endregion
    }
}
