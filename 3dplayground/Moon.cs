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

        public DisplacementStructure GetGravityDisplacement(GameTime theTime)
        {
            New_pos_and_vel vals;
            vals = mFieldPhysics.dothe_phys((float)theTime.ElapsedGameTime.Milliseconds, this);

            DisplacementStructure theArgs;
            theArgs = new DisplacementStructure(this, vals.position, vals.velocity,mRotation, Quaternion.Identity);

            return theArgs;
        }



        #endregion

      
    }
}
