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

        protected IFieldPhysics mPhys;

        public GravityModel(string theName, int theMass, Vector3 thePosition,
            Vector3 theVelocity, Quaternion theRotation,Quaternion theAngularVelocity, IModel theModel, IFieldPhysics theFieldPhysics)
            : base(theName, theMass, thePosition, theVelocity, theRotation,theAngularVelocity, theModel)
        {
            mPhys = theFieldPhysics;
        }




        #region IGetEffectedByGravity Members

        public DisplacementStructure GetGravityDisplacement(GameTime theTime)
        {
            New_pos_and_vel disp;
            disp = mPhys.dothe_phys(theTime.ElapsedGameTime.Milliseconds, this);

            DisplacementStructure theArgs;
            theArgs = new DisplacementStructure((IAmInSpace)this, disp.position, disp.velocity, mRotation,Quaternion.Identity );

            return theArgs;
        }

        #endregion
    }
}
