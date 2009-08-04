using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// abstract implementation of a pysical body.
    /// Update and draw are intended to be overriden.
    /// </summary>
    public abstract  class  PhysicalBody : IPhysicsObject 
    {
        public event EventHandler<DisplacementArgs> RequestMove;

        protected  string mName;
        protected DVector3 mPosition;
        protected DVector3 mVelocity;
        

        protected Quaternion mAngularVelocity;
        protected Quaternion mRotation;


        protected int mMass;
        protected GameSpaceUnit mSpace;

        protected bool mIsDrawActive = true;  
        
        protected DisplacementStructure mTotalDisplacement;


        #region Constructor

        public PhysicalBody(string theName, GameSpaceUnit theSpace, int theMass, 
            DVector3 thePosition, DVector3 theVelocity, 
            Quaternion theRotation, Quaternion theAngularVelocity)
        {
            mName = theName;
            mPosition = thePosition;
            mRotation = theRotation;
            mMass = theMass;
            mVelocity = theVelocity;
           
            mAngularVelocity = theAngularVelocity;
            mSpace = theSpace;

            mTotalDisplacement = DisplacementStructure.ZeroDeltas(this);
            
        }
        #endregion

        #region Properties
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
        /// Getsw the objects name
        /// </summary>
        public string Name
        {
            get { return mName; }
        }

        /// <summary>
        /// Gets the objects mass
        /// </summary>
        public int Mass
        {
            get { return mMass; }
        }

        /// <summary>
        /// Returns wether drawing is currently enablesd for this objewct
        /// </summary>
        public bool IsDrawActive
        {
            get { return mIsDrawActive; }
        }

        /// <summary>
        /// Returns the Space that the object inahbits
        /// </summary>
        public GameSpaceUnit Space
        {
            get { return mSpace; }
        }         



        #endregion 

        #region IDrawable Methods

        public abstract void Draw(Camera theCamera, Vector3   thePosition, Quaternion theRotation); 

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
        public void Update(TimeSpan UpdateTime)
        {
            if ((mVelocity.X==0)&&(mVelocity.Y==0)&&(mVelocity.Z==0))
            {
                DVector3 deltaPosition = mPosition + (mVelocity * (UpdateTime.Milliseconds  / 1000D)); 

                mTotalDisplacement.Position = mPosition;
                mTotalDisplacement.DeltaPosition = deltaPosition;
                mTotalDisplacement.Velocity = mVelocity;  
            }
            // Sorry should of written the opposite test but cba, its late.
            if (!mTotalDisplacement.HasNoDeltas())
            {
                RaiseRequestMove(new DisplacementArgs(mTotalDisplacement));
            }
        }

        #endregion 

    }
}
