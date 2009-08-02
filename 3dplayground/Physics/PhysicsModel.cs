using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _3dplayground.Graphics.D3;

namespace _3dplayground.Physics
{
    abstract class  PhysicsModel : PhysicalBody  
    {


        protected IModel mModel;   

        public PhysicsModel(string theName,GameSpaceUnit theSpace, int theMass, Vector3 thePosition, Vector3 theVelocity,
            Quaternion theRotation, Quaternion theAngularVelocity, IModel theModel)
            : base(theName,theSpace, theMass, thePosition,theVelocity, theRotation,theAngularVelocity )
        {
            mModel = theModel;
        }  

        public override  void Draw(Camera theCamera,Vector3 thePosition, Quaternion theRotiation)
        {
            if (mIsDrawActive)
            {
                mModel.draw(thePosition, theRotiation, theCamera);
            }
        } 
    }
}
