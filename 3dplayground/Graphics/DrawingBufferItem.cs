using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace _3dplayground.Graphics
{

    
    /// <summary>
    /// An item in the draeing buffer that represents any changes that need to be written to the drawable data.
    /// Provides access to a method on the IDrawable to apply thi sstruct to itself.
    /// </summary>
    public struct  DrawingBufferItem  
    {
        public  IGameDrawable mDrawable; 
        public  DrawingStructure mStructure;   


        public DrawingBufferItem(IGameDrawable theDrawable,bool theIsDrawActive, Vector3 thePosition, Quaternion theRotation)
        {
            mStructure.ID = theDrawable.ID;
            mStructure.IsActive = theIsDrawActive;
            mStructure.Position = thePosition;
            mStructure.Rotation = theRotation;
            mDrawable=theDrawable;
            
        }

        public DrawingBufferItem(IGameDrawable thedrawable, DrawingStructure theDrawableData)
        {
            mDrawable = thedrawable;
            mStructure = theDrawableData;

        }

        /// <summary>
        /// the name of the Item to be drawn
        /// </summary>
        public string Name
        { get { return mDrawable.Name; } }

        /// <summary>
        /// Returns the global id of the object to be affected.
        /// </summary>
        public int ID
        { get { return mStructure.ID; } }

        /// <summary>
        /// The position in space the object is to be drawn
        /// </summary>
        public Vector3 Position
        {
            get { return mStructure.Position ; }
            set { mStructure.Position = value; }
        }

        /// <summary>
        /// The rotation to be appleied to the object being drawn
        /// </summary>
        public Quaternion Rotation
        {
            get { return mStructure.Rotation ; }
            set { mStructure.Rotation = value; }
        }

        /// <summary>
        /// A reference to the IDrawable object
        /// </summary>
        public IGameDrawable  Drawable
        { get { return mDrawable; } }


        /// <summary>
        /// This class implementing IDrawable provides custom behaviour here to cast this into different derivatives of this base class.
        /// </summary>
        public void ApplyBuffer()
        { mDrawable.ApplyDrawingstructure(mStructure); }


        #region ICloneable Members

        public object Clone()
        {
            DrawingBufferItem retVal;
            retVal.mDrawable = mDrawable;
            retVal.mStructure = mStructure;
            return retVal;
        }

        #endregion
    }
}
