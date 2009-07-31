using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _3dplayground.Graphics.D3;

namespace _3dplayground.Physics
{
    abstract class GravityModel : PhysicsModel , IGetEffectedByGravity 
    {

        protected IFieldPhysics mFieldPhysics;
        protected DisplacementStructure  mGravityDisplacement;

        public GravityModel(string theName, int theMass, Vector3 thePosition,
            Vector3 theVelocity, Quaternion theRotation,Quaternion theAngularVelocity, IModel theModel, IFieldPhysics theFieldPhysics)
            : base(theName, theMass, thePosition, theVelocity, theRotation,theAngularVelocity, theModel)
        {
            mFieldPhysics = theFieldPhysics;
        }
        #region IGetEffectedByGravity Members
        public DisplacementStructure GravityDisplacement
        {
            get { return mGravityDisplacement; }
        }



        public void ExecuteGravityDisplacement(TimeSpan  theTime)
        {
            New_pos_and_vel disp;
            disp = mFieldPhysics.dothe_phys(theTime.Milliseconds , this);
            mGravityDisplacement = new DisplacementStructure((IAmInSpace)this, disp.position, disp.velocity, mRotation, Quaternion.Identity);
            mTotalDisplacement = DisplacementStructure.CombineStructure(mTotalDisplacement, mGravityDisplacement);


        }

        #endregion

    }
}
