﻿using System;
using Microsoft.Xna.Framework;

using _3dplayground.Graphics.D3;
using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// This is an abstract base for any class wishing to inherit from a gravitational mass.
    /// Thi sclass probably doesn;t wan to be abstract.
    /// </summary>
    abstract class GravityEffectedModel : PhysicsModel , IGetEffectedByGravity 
    {

        protected IFieldPhysics mFieldPhysics;
        protected DisplacementStructure  mGravityDisplacement;

        public GravityEffectedModel(string theName,GameSpaceUnit theSpace, int theMass, DVector3 thePosition,
            DVector3 theVelocity, Quaternion theRotation, Quaternion theAngularVelocity, Vector3 theUpVector, 
            Vector3 theCameraOffset, IModel theModel, IFieldPhysics theFieldPhysics)
            : base(theName,theSpace, theMass, thePosition, theVelocity, theRotation,theAngularVelocity,theUpVector,theCameraOffset, theModel)
        {
            mFieldPhysics = theFieldPhysics;
        }
        #region IGetEffectedByGravity Members
        public DisplacementStructure GravityDisplacement
        {
            get { return mGravityDisplacement; }
        }  

        /// <summary>
        /// This method is run prior to the objects update to calculate the gravity. 
        /// </summary>
        /// <param name="theTime"></param>
        public void CalculateGravityDisplacement(float   theTime)
        {
            New_pos_and_vel disp;
            disp = mFieldPhysics.dothe_phys(theTime , this);
            mGravityDisplacement.DeltaPosition+=disp.position;
            mGravityDisplacement.DeltaVelocity+=disp.velocity;
        }

        #endregion



        protected override void UpdateDetail(float  UpdateTime)
        {
            mTotalDisplacement.DeltaPosition = mGravityDisplacement.DeltaPosition;
            mTotalDisplacement.DeltaVelocity = mGravityDisplacement.DeltaVelocity;

        }

        public override void ResetDisplacementStructures()
        {
            base.ResetDisplacementStructures();

            mGravityDisplacement.DeltaPosition = DVector3.Zero;
            mGravityDisplacement.DeltaRotation = Quaternion.Identity;
            mGravityDisplacement.DeltaVelocity = DVector3.Zero;
        }


    }
}
