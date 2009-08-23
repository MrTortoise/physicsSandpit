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

        public override void Draw(float GameTime, Camera theCamera)
        {   
            if (mIsDrawActive)
            {
                mModel.Draw(theCamera, mDrawPosition , mDrawRotation );
            }    
        }

         


        #region IGetEffectedByGravity Members
        public DisplacementStructure GravityDisplacement
        {
            get { return mGravityDisplacement; }
        }

                                                          

        public void ExecuteGravityDisplacement(float   theTime)
        {
            
            New_pos_and_vel disp;
            disp = mFieldPhysics.dothe_phys(theTime, this);
            mGravityDisplacement = new DisplacementStructure(this,mPosition, disp.position,mVelocity, disp.velocity);           
           
        }




        #endregion

        protected override void UpdateDetail(float  UpdateTime)
        {               
            mTotalDisplacement.DeltaPosition  = mGravityDisplacement.DeltaPosition;
            mTotalDisplacement.DeltaVelocity = mGravityDisplacement.DeltaVelocity;

        }
    }
}
