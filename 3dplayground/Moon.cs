using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _3dplayground.Graphics.D3;
using _3dplayground.Physics;

namespace _3dplayground
{

    class Moon : PhysicalBody,  IGetEffectedByGravity   
    {             

        protected  IFieldPhysics mFieldPhysics;
        protected IModel mModel;
        protected DisplacementStructure mGravityDisplacement;      

        public Moon(IModel theModel, IFieldPhysics theFPC,string theName,int theMass, 
            Vector3 thePosition, Vector3 theVelocity, Quaternion  theRotation, Quaternion theAngularVelocity)
            :base(theName,theMass,thePosition,theVelocity,theRotation,theAngularVelocity )
        {            
            mFieldPhysics = theFPC;
            mModel = theModel;
        }        

        public override void Draw(Camera theCamera)
        {   
            if (mIsDrawActive)
            {
                mModel.draw(mPosition, mRotation, theCamera);
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
            disp = mFieldPhysics.dothe_phys(theTime.Milliseconds, this);
            mGravityDisplacement = new DisplacementStructure((IAmInSpace)this, disp.position, disp.velocity, mRotation, Quaternion.Identity);
            mTotalDisplacement = DisplacementStructure.CombineStructure(mTotalDisplacement, mGravityDisplacement);


        }

        #endregion
    }
}
