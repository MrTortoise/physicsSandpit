using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3dplayground.Graphics.D3
{
    interface IModel : ILoadable ,IDrawable 
    {
        /// <summary>
        /// Gets the smallest box that will fit the model and all its meshes
        /// </summary>
        BoundingBox  CompoundBoundingBox
        { get; }

        /// <summary>
        /// Gets the Bounded sphere that encompasses the model (rotation independant) and all its meshes
        /// </summary>
        BoundingSphere  CompoundBoundingSphere
        { get; }

        /// <summary>
        /// Gets the smallest box that will contain the model at any rotation as well as all its meshes
        /// </summary>
        BoundingBox CompoundSafeBoundingBox
        { get; }

        /// <summary>
        /// Recalculates all the bounding boxes fot the model
        /// </summary>
        void CalculateBoundingBoxes();

        void CalculateCompoundBoundingBox();
        void CalculateCompoundBoundingSphere();
        void CalculateCompoundSafeBoundingBox();
        





    }
}
