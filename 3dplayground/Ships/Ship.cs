using System;
using System.Collections.Generic;
using _3dplayground.Physics;
using _3dplayground.Maths;
using _3dplayground.Graphics.D3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3dplayground.Ships
{
    class Ship  : GravityEffectedModel,IAmShip  
    {
        protected IAmShipEngine mEngine;


        public Ship(string theName, GameSpaceUnit theSpace, int theMass, DVector3 thePosition,
            DVector3 theVelocity, Quaternion theRotation, Quaternion theAngularVelocity, Vector3 theUpVector, Vector3 theCameraOffset, 
            IModel theModel, IFieldPhysics theFieldPhysics, IAmShipEngine theEngine)
            : base(theName, theSpace, theMass, thePosition, theVelocity, theRotation, theAngularVelocity,theUpVector,theCameraOffset, theModel, theFieldPhysics)
        {
            mEngine = theEngine;

        }

        protected override void UpdateDetail(float  UpdateTime)
        {
            //Does the gravity update
            base.UpdateDetail(UpdateTime);

            //Perform and ship thrust calcs

            double acceleration = mEngine.GetAcceleration(GetShipsMassForEngine(), UpdateTime);
           //ToDo: implement updatedetail for the ship
            mRotation.Normalize();
            double xVel = mRotation.X * acceleration * UpdateTime;
            double yVel = mRotation.Y * acceleration * UpdateTime;
            double zVel = mRotation.Z * acceleration * UpdateTime;

            double xPos = xVel * UpdateTime / 2;
            double yPos = xVel * UpdateTime / 2;
            double zPos = zVel * UpdateTime / 2;

            DVector3 dVel = new DVector3(xVel, yVel, zVel);
            DVector3 dPos = new DVector3(xPos, yPos, zPos);
            // add these displacements to the total displacement
            mTotalDisplacement.DeltaPosition += dPos;
            mTotalDisplacement.DeltaVelocity += dVel;                                                                           
        }


        #region IAmShip Members

        public void SetEngine(IAmShipEngine theEngine)
        {
            mEngine = theEngine;

        }

        public void Accelerate()
        {
            mEngine.IsActive = true;
            mEngine.IsAccelerating = true;
        }

        public void Decelerate()
        {
            mEngine.IsActive = true;
            mEngine.IsAccelerating = false;
        }
        public void TurnEngineOff()
        {
            mEngine.IsActive = false;   
        }


        public void RotateLaterally(float value)
        {
            Quaternion temp =  Quaternion.CreateFromAxisAngle(Vector3.Right, value); 
            mTotalDisplacement.DeltaRotation+= temp;
            mUpVector += new Vector3(temp.X, temp.Y, temp.Z);
            
        }

        public void RotateLongitudionally(float value)
        {
            mTotalDisplacement.DeltaRotation += Quaternion.CreateFromAxisAngle(Vector3.Up, value);
        }


        #endregion

        protected virtual double GetShipsMassForEngine()
        {
            return mMass;

        }


    }
}
