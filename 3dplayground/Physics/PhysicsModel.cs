using System;
using Microsoft.Xna.Framework;

using _3dplayground.Graphics.D3;
using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// This is an abstract class that is the basic implementation of a Physical body with a model.
    /// </summary>
    abstract class  PhysicsModel : PhysicalBody  
    {


        protected IModel mModel;   

        public PhysicsModel(string theName,GameSpaceUnit theSpace, int theMass, DVector3 thePosition, DVector3 theVelocity,
            Quaternion theRotation, Quaternion theAngularVelocity, Vector3 theupVector, Vector3 theCameraOffset, IModel theModel)
            : base(theName,theSpace, theMass, thePosition,theVelocity, theRotation,theAngularVelocity,theupVector,theCameraOffset  )
        {
            mModel = theModel;
        }

        public override void Draw(float GameTime, Camera theCamera)
        {
            if (mIsDrawActive)
            {
                mModel.Draw( theCamera,mDrawPosition,mDrawRotation );
            }
        }


    }
}
