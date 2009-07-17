using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{

    class Moon : PhysicalBody,  IGetEffectedByField   
    {             

        protected  IFieldPhysics mFieldPhysics;


        protected IModel mModel;


        

        public Moon(IModel theModel, IFieldPhysics theFPC,string theName,int theMass, Vector3 thePosition, Vector3 theVelocity, Quaternion  theRotation)
            :base(theName,theMass,thePosition,theVelocity,theRotation)
        {            
            mFieldPhysics = theFPC;
            mModel = theModel;
        }

        public override void Update(GameTime timeInterval)
        {           

          
            //ToDo: This is where you hook up to the field physics component. Pass your data in and get your data out to update this object.
        }

        public override void Draw(Camera theCamera)
        {   
            if (mIsDrawActive)
            {
                mModel.draw(mPosition, mRotation, theCamera);
            }    
        }

        


 




    }
}
