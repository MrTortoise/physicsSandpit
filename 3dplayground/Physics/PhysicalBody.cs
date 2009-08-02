using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
        protected Vector3 mPosition;
        protected Vector3 mVelocity;
        protected Quaternion mAngularVelocity;
        protected Quaternion mRotation;
        protected int mMass;
        protected GameSpaceUnit mSpace;

        protected bool mIsDrawActive = true;  
        
        protected DisplacementStructure mTotalDisplacement;


        #region Constructor

        public PhysicalBody(string theName,GameSpaceUnit theSpace, int theMass, Vector3 thePosition,Vector3 theVelocity, Quaternion theRotation, Quaternion theAngularVelocity)
        {
            mName = theName;
            mPosition = thePosition;
            mRotation = theRotation;
            mMass = theMass;
            mVelocity = theVelocity;
            mAngularVelocity = theAngularVelocity;
            mSpace = theSpace;
            
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Velocity of the Object
        /// </summary>
        public Vector3 Velocity
        { get { return mVelocity; } }

        /// <summary>
        /// Gets the Angular Velocity of the object.
        /// </summary>
        public Quaternion AngularVelocity
        { get { return mAngularVelocity; } }

        /// <summary>
        /// Gets the position of the Object in space
        /// </summary>
        public Vector3 Position
        {
            get { return mPosition; }
        }

        /// <summary>
        /// Gets the rotation of the body about its volumetric center in space
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

        #region Public Methods

        public abstract void Draw(Camera theCamera, Vector3 thePosition, Quaternion theRotation); 



        #endregion  
    

        #region ICanMove Members


        public virtual  void ResetDisplacementStructures()
        {
            mTotalDisplacement = new DisplacementStructure(this, Vector3.Zero, Vector3.Zero, Quaternion.Identity, Quaternion.Identity);
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
            mRotation += theStructure.DeltaRotation;
            mAngularVelocity += theStructure.DeltaAngularVelocity;
        }

        public void RaiseRequestMove(DisplacementArgs theArgs)
        {
            if (RequestMove != null)
            {
                RequestMove(this, theArgs);
            }
        }

        #endregion  

    
        #region IAmInSpace Members




        #endregion




    }
}
