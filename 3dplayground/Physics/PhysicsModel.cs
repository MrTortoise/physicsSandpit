using System;
using Microsoft.Xna.Framework;

using _3dplayground.Graphics.D3;
using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    abstract class  PhysicsModel : PhysicalBody  
    {


        protected IModel mModel;   

        public PhysicsModel(string theName,GameSpaceUnit theSpace, int theMass, DVector3 thePosition, DVector3 theVelocity,
            Quaternion theRotation, Quaternion theAngularVelocity, IModel theModel)
            : base(theName,theSpace, theMass, thePosition,theVelocity, theRotation,theAngularVelocity )
        {
            mModel = theModel;
        }  

        public override  void Draw(Camera theCamera,Vector3  thePosition, Quaternion theRotation)
        {
            if (mIsDrawActive)
            {
                mModel.Draw(theCamera,thePosition,theRotation);
            }
        } 
    }
}
