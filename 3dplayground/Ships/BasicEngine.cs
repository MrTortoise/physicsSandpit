using System;
using System.Collections.Generic;

using _3dplayground.Maths;


namespace _3dplayground.Ships
{
    class BasicEngine : IAmShipEngine 
    {
        protected string mName;
        protected int mID = GlobalIDGenerator.GetNextID();

        protected bool mIsActive = false;
        protected bool mIsAccelerating = true;
        protected float mFuelLevel;
        protected float mFuelCapacity;
        protected float mFuelConsumption;
        protected double mThrust;
        protected int mMass;

        public BasicEngine(string theName, float theFuelCapacity, float theFuelConsumption, float theFuelLevel, double theThrust, int theMass)
        {
            mName = theName;
            mFuelCapacity = theFuelCapacity;
            mFuelConsumption = theFuelConsumption;
            mFuelLevel = theFuelLevel;
            mThrust = theThrust;
            mMass = theMass;
        }
 
        

        #region IAmShipEngine Members

        public bool IsActive
        {
            get
            {
                return mIsActive;
            }
            set
            {
                mIsActive = value;
            }
        }

        public float FuelLevel
        {
            get { return mFuelLevel; }
        }

        public float FuelCapacity
        {
            get { return mFuelCapacity; }
        }

        public float FuelConsumption
        {
            get { return mFuelConsumption; }
        }

        public double GetThrust
        {
            get { return mThrust; }
        }

        public void AddFuel(float  Amount)
        {
            mFuelLevel += Amount;
            if (mFuelLevel > mFuelCapacity)
            { mFuelLevel = mFuelCapacity; }
        }

        public double GetAcceleration(double mass,  double timeSpan)
        {
            double retVal;
            if (mIsActive)
            {
                if (mIsAccelerating)
                {
                    retVal = mThrust * timeSpan / (mass + mMass);
                }
                else
                {
                    retVal = -1 * mThrust * timeSpan / (mass + mMass);
                }
                mFuelLevel -= (float)(mFuelConsumption * timeSpan);
            }
            else
            { retVal = 0; }

            return retVal;
        }

        public bool IsAccelerating
        {
            get
            {
                return mIsAccelerating;
            }
            set
            {
                mIsAccelerating = value;
            }
        }

        #endregion

        #region IHasMass Members

        public int Mass
        {
            get { return mMass; }
        }

        #endregion

        #region IHasName Members

        public string Name
        {
            get { return mName; }
        }

        public int ID
        { get { return mID; } }

        #endregion

        #region IAmShipEngine Members




        #endregion
    }
}
