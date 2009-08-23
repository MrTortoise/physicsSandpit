﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using _3dplayground.Maths;
using _3dplayground.Graphics;




namespace _3dplayground.Physics
{
    /// <summary>
    /// abstract implementation of a pysical body.
    /// Update and draw are intended to be overriden.
    /// </summary>
    public abstract  class  PhysicalBody : IPhysicsObject
    {
        #region Fields

        protected  string mName;
        protected int mID = GlobalIDGenerator.GetNextID();

        #region UpdateFields
        public event EventHandler<DisplacementArgs> RequestMove;
        protected bool mIsCanMoveActive = true;

        protected DVector3 mPosition;
        protected DVector3 mVelocity;        

        protected Quaternion mAngularVelocity;
        protected Quaternion mRotation;

        protected int mMass;
        protected GameSpaceUnit mSpace;

        protected DisplacementStructure mTotalDisplacement;

        #endregion

        #region DrawFields


        protected bool mIsDrawActive = true;

        protected Vector3 mDrawPosition;
        protected Quaternion  mDrawRotation;

        #endregion

        #endregion

        #region Constructor

        public PhysicalBody(string theName, GameSpaceUnit theSpace, int theMass, 
            DVector3 thePosition, DVector3 theVelocity, 
            Quaternion theRotation, Quaternion theAngularVelocity)
        {
            mName = theName;
            mPosition = thePosition;
            mDrawPosition = thePosition.ToVector3();
            mRotation = theRotation;
            mDrawRotation = theRotation;
            mMass = theMass;
            mVelocity = theVelocity;
           
            mAngularVelocity = theAngularVelocity;
            mSpace = theSpace;

            mTotalDisplacement = DisplacementStructure.ZeroDeltas(this);
            
        }
        #endregion

        #region Properties

        public int ID
        { get { return mID; } }

        /// <summary>
        /// Getsw the objects name
        /// </summary>
        public string Name
        {
            get { return mName; }
        }

        #region Update
        /// <summary>
        /// Gets the position of the Object in space
        /// </summary>
        public DVector3 Position
        {
            get { return mPosition; }
        }

        /// <summary>
        /// Gets the Velocity of the Object
        /// </summary>
        public DVector3 Velocity
        { get { return mVelocity; } } 
 

        /// <summary>
        /// Gets the Angular Velocity of the object.
        /// </summary>
        public Quaternion AngularVelocity
        { get { return mAngularVelocity; } }

       

        /// <summary>
        /// Gets the rotation of the body about its center in space
        /// </summary>
        public Quaternion Rotation
        {
            get { return mRotation; }
        }


        /// <summary>
        /// Gets the objects mass
        /// </summary>
        public int Mass
        {
            get { return mMass; }
        }
        /// <summary>
        /// Returns the Space that the object inahbits
        /// </summary>
        public GameSpaceUnit Space
        {
            get { return mSpace; }
        }  

        #endregion
        #region Draw

        /// <summary>
        /// Returns wether drawing is currently enablesd for this objewct
        /// </summary>
        public bool IsDrawActive
        {
            get { return mIsDrawActive; }
        }

        /// <summary>
        /// Returns the position a draw call would place the object in space
        /// </summary>
        public Vector3  DrawPosition
        {
            get { return mDrawPosition; }
        }

        /// <summary>
        /// Returns the rotation of the object that a draw call would use.
        /// </summary>
        public Quaternion DrawRotation
        {
            get { return mDrawRotation; }
        }

        #endregion        

        #endregion    

        #region ICanMove Members


        public virtual  void ResetDisplacementStructures()
        {

            mTotalDisplacement = DisplacementStructure.ZeroDeltas(this);
        }

        public DisplacementStructure GetDisplacementStructure
        {
            get { return mTotalDisplacement; }
        }

        /// <summary>
        /// Once a move request has been adjusted it is executed on the object
        /// </summary>
        /// <param name="theStructure">the displacement structure to apply to this object</param>
        public virtual void ExecuteDisplacementStructure(DisplacementStructure theStructure)
        {
            mPosition += theStructure.DeltaPosition;
            mVelocity += theStructure.DeltaVelocity;
        }

        public void RaiseRequestMove(DisplacementArgs theArgs)
        {
            if (RequestMove != null)
            {
                RequestMove(this, theArgs);
            }
        }
        /// <summary>
        /// This is the Update Wrapper that is to be called eals with resetting, executing the Detail and raising event.
        /// </summary>
        /// <param name="UpdateTime"></param>
        public  void Update(float UpdateTime)
        {   
            ResetDisplacementStructures();

            UpdateDetail(UpdateTime);
                
            
            // Sorry should of written the opposite test but cba, its late.
            if (!mTotalDisplacement.HasNoDeltas())
            {
                RaiseRequestMove(new DisplacementArgs(mTotalDisplacement));
            }
        }

        /// <summary>
        /// This is the class to override to provide additional update logic.
        /// </summary>
        /// <param name="UpdateTime"></param>
        protected virtual void UpdateDetail(float  UpdateTime)
        {
          DVector3 deltaPosition = mVelocity * UpdateTime; 

                mTotalDisplacement.Position = mPosition;
                mTotalDisplacement.DeltaPosition += deltaPosition;
                mTotalDisplacement.Velocity = mVelocity;
                mTotalDisplacement.DeltaVelocity = DVector3.Zero;
        }


        public bool IsCanMoveActive
        {
            get { return mIsCanMoveActive; }
        }

        #endregion       

        #region IGameDrawable Members

        public abstract void Draw(float GameTime, Camera theCamera); 
        public virtual void ApplyDrawingstructure(DrawingStructure theBufferItem)
        {
            mIsDrawActive = theBufferItem.IsActive;
            mDrawPosition = theBufferItem.Position;
            mDrawRotation = theBufferItem.Rotation;
            
        }


        public virtual  DrawingStructure ConstructDrawingStuct(DisplacementStructure theSource)
        {
            Vector3 pos = (theSource.Position + theSource.DeltaPosition).ToVector3();
            Quaternion rot = mRotation;

            //ToDo: implement rotation

            DrawingStructure retVal = new DrawingStructure(mID,theSource.IPhysicsObject.IsDrawActive, pos, rot);
            return retVal;

        }

        #endregion


    }
}
