using System;
using System.Collections.Generic;
using _3dplayground.Physics;
using _3dplayground.Maths;
using _3dplayground.Graphics.D3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3dplayground.Ships
{
    class Ship  : GravityModel,IAmShip  
    {
        protected IAmShipEngine mEngine;


        public Ship(string theName, GameSpaceUnit theSpace, int theMass, DVector3 thePosition,
            DVector3 theVelocity, Quaternion theRotation, Quaternion theAngularVelocity, IModel theModel, IFieldPhysics theFieldPhysics,
            IAmShipEngine theEngine)
            : base(theName, theSpace, theMass, thePosition, theVelocity, theRotation, theAngularVelocity, theModel, theFieldPhysics)
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

        }


        #region IAmShip Members

        public void SetEngine(IAmShipEngine theEngine)
        {
            mEngine = theEngine;

        }

        public void TurnEngineOff()
        {
            mEngine.IsActive = false;
        }

        public void TurnEngineOn()
        {
            mEngine.IsActive = true;
        }

        public void SetRotationUnitVector(DVector3 theRotation, float magnitude)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected virtual double GetShipsMassForEngine()
        {
            return mMass;

        }
    }
}
