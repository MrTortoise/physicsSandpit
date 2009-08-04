using System;
using Microsoft.Xna.Framework;

using _3dplayground.Graphics.D3;
using _3dplayground.Physics;
using _3dplayground.Maths;

namespace _3dplayground
{

    /// <summary>
    /// overrtides reset displacement structures
    /// TheGravity Displacement DOES NOT get reset ... this iwll allow its last value to be of use when multithreaded, besides it is a good approximation
    /// </summary>
    class Moon : PhysicalBody,  IGetEffectedByGravity   
    {             

        protected  IFieldPhysics mFieldPhysics;
        protected IModel mModel;
        protected DisplacementStructure mGravityDisplacement;

        public Moon(IModel theModel, IFieldPhysics theFPC, GameSpaceUnit theSpace, string theName, int theMass,
            DVector3 thePosition, DVector3 theVelocity, Quaternion theRotation, Quaternion theAngularVelocity)
            :base(theName,theSpace, theMass,thePosition,theVelocity,theRotation,theAngularVelocity )
        {            
            mFieldPhysics = theFPC;
            mModel = theModel;
        }        

        public override void Draw(Camera theCamera,Vector3 thePosition, Quaternion theRotation)
        {   
            if (mIsDrawActive)
            {
                mModel.Draw(theCamera, thePosition, theRotation);
            }    
        }

         


        #region IGetEffectedByGravity Members
        public DisplacementStructure GravityDisplacement
        {
            get { return mGravityDisplacement; }
        }

                                                          

        public void ExecuteGravityDisplacement(TimeSpan  theTime)
        {
            
            New_pos_and_vel disp;
            disp = mFieldPhysics.dothe_phys(theTime.Milliseconds/10000, this);
            mGravityDisplacement = new DisplacementStructure(this,mPosition, disp.position,mVelocity, disp.velocity);
            //Gravity is going to run first in the update loop - prior to any ai / user updates anyway.
           
        }




        #endregion

        protected override void UpdateDetail(TimeSpan UpdateTime)
        {
          
            mTotalDisplacement.DeltaPosition  = mGravityDisplacement.DeltaPosition;
            mTotalDisplacement.DeltaVelocity = mGravityDisplacement.DeltaVelocity;

        }
    }
}
