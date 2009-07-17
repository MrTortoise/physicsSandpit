using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    /// <summary>
    /// abstract implementation of a pysical body.
    /// Update and draw are intended to be overriden.
    /// </summary>
    public abstract  class  PhysicalBody : IPhysicsObject 
    {
        protected  string mName;
        protected Vector3 mPosition;
        protected Vector3 mVelocity;
        protected Quaternion mRotation;
        protected int mMass;

        protected bool mIsDrawActive = true;
        protected bool mIsUpdateActive = true;

        #region Constructor

        public PhysicalBody(string theName, int theMass, Vector3 thePosition, Vector3 theVelocity, Quaternion theRotation)
        {
            mName = theName;
            mPosition = thePosition;
            mVelocity = theVelocity;
            mRotation = theRotation;
            mMass = theMass;
        }
        #endregion

        #region Properties


        public Vector3 Position
        {
            get { return mPosition; }
        }

        public Vector3 Velocity
        {
            get { return mVelocity; }
        }

        public Quaternion Rotation
        {
            get { return mRotation; }
        }

        public string Name
        {
            get { return mName; }
        }

        public int Mass
        {
            get { return mMass; }
        }

        public bool IsDrawActive
        {
            get { return mIsDrawActive; }
        }

        #endregion 

        #region Public Methods

        public abstract void Draw(Camera theCamera); 

        public abstract void Update(GameTime timeInterval);


        public bool IsUpdateActive
        {
            get { return mIsUpdateActive; }
        }

        #endregion  
    
  
    }
}
