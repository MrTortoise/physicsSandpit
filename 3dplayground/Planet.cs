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
        /// <param name="thePosition">The poisition in space of the body in the field</param>
        /// <param name="magnitude">Currently unused, but represents the mass of the body in the field</param>
        /// <returns></returns>
        public Vector3 Force(Vector3 thePosition, int magnitude)
        {
            Vector3 sumforce = Vector3.Zero;
            Vector3 displacementVector;
            displacementVector = Vector3.Subtract(mPosition , thePosition );
            float distance = displacementVector.Length();
                
            double tmpR = 0;



                   // tmpR = Math.Pow((i.Position.X - pos.X), 2) + Math.Pow((i.Position.Y - pos.Y), 2) + Math.Pow((i.Position.Z - pos.Z), 2);
                   // tmpR = mMass  * Constants.G  / Math.Pow(tmpR, 1.5);

            // I have no ideas which will work out faster - math.pow for x^2 is half as fast - but god knows how vector3.Length() will work.
            // http://mindstudies.psy.soton.ac.uk/dmitri/blog/index.php/archives/175

            tmpR= mMass  * Constants.G  /( distance*distance );

              //      sumforce.X = sumforce.X + (i.Position.X - pos.X) * (float)tmpR; //sigh vector3 uses floats - teh suq
               //     sumforce.Y = sumforce.Y + (i.Position.Y - pos.Y) * (float)tmpR;
              //      sumforce.Z = sumforce.Z + (i.Position.Z - pos.Z) * (float)tmpR;

                    sumforce.X  =  displacementVector.X  * (float)tmpR;
                    sumforce.Y = displacementVector.Y * (float)tmpR;
                    sumforce.Z = displacementVector.Z * (float)tmpR;
            
            return sumforce;
        }          

        #region  Overridden methods

        public override   void Draw(Camera theCamera)
        {
            mModel.draw(mPosition, mRotation, theCamera);
        }

 

        public override  void Update(Microsoft.Xna.Framework.GameTime timeInterval)
        {
            //ToDp: Implement some movement algorithm here maybe
            
            //Will do this by writing an orbiter component (through  an IRobiter interface) that given timestep, distance and initial offset will
            //calculate orbit position. We can then use the interface to implement different kinds of orbiter.  
            // The object with the orbiter component wil have to have a reference to its center of orbit object.
            
        }


        #endregion


    }
}
