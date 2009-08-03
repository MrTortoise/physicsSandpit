using System;
using Microsoft.Xna.Framework;

using _3dplayground.Graphics.D3;
using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// This is an abstract base for any class wishing to inherit from a gravitational mass.
    /// Thi sclass probably doesn;t wan to be abstract.
    /// </summary>
    abstract class GravityModel : PhysicsModel , IGetEffectedByGravity 
    {

        protected IFieldPhysics mFieldPhysics;
        protected DisplacementStructure  mGravityDisplacement;

        public GravityModel(string theName,GameSpaceUnit theSpace, int theMass, DVector3 thePosition,
            DVector3 theVelocity,Quaternion theRotation,Quaternion theAngularVelocity, IModel theModel, IFieldPhysics theFieldPhysics)
            : base(theName,theSpace, theMass, thePosition, theVelocity, theRotation,theAngularVelocity, theModel)
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
            mGravityDisplacement = new DisplacementStructure(this, mPosition, disp.position, mVelocity, disp.velocity);
            mTotalDisplacement =mTotalDisplacement+ mGravityDisplacement;


        }

        #endregion

        public override void ResetDisplacementStructures()            
        {
            base.ResetDisplacementStructures();
            mGravityDisplacement = DisplacementStructure.ZeroDeltas( this);
        }

    }
}
