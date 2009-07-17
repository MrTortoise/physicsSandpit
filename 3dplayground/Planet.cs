using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    class Planet : PhysicalBody, IEmitPointField 
    {

        protected IModel mModel;

        public Planet(IModel theModel, string theName, int theMass,Vector3 thePosition, Vector3 theVelocity, Quaternion theRotation )
            :base(theName,theMass,thePosition,theVelocity,theRotation)
        {
            mModel = theModel;

        }


        /// <summary>
        /// Implement gravity equation here i guess.
        /// </summary>
        /// <param name="thePosition"></param>
        /// <param name="magnitude"></param>
        /// <returns></returns>
        public Vector3 Force(Vector3 thePosition, int magnitude)
        {
            throw new NotImplementedException();
        }       



        #region  Overridden methods

        public override   void Draw(Camera theCamera)
        {
            mModel.draw(mPosition, mRotation, theCamera);
        }

 

        public override  void Update(Microsoft.Xna.Framework.GameTime timeInterval)
        {
            //ToDp: Implement some movement algorithm here maybe
            
        }


        #endregion


    }
}
