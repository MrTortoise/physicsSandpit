using System;
using Microsoft.Xna.Framework;

using _3dplayground.Physics;
using _3dplayground.Graphics.D3;
using _3dplayground.Maths;

namespace _3dplayground
{
    class Planet : PhysicalBody, IEmitPointField 
    {

        protected IModel mModel;

        public Planet(IModel theModel, GameSpaceUnit theSpace, string theName, int theMass,
            DVector3 thePosition, DVector3 theVelocity,
            Quaternion theRotation, Quaternion theAngularVelocity)
            :base(theName,theSpace, theMass,thePosition,theVelocity,theRotation,theAngularVelocity,DVector3.UnitZ )
        {
            mModel = theModel;

        }


        /// <summary>
        /// Implement gravity equation here i guess.
        /// </summary>
        /// <param name="thePosition">The poisition in space of the body in the field</param>
        /// <returns></returns>
        public DVector3  PointFieldAcceleration(DVector3  thePosition)
        {
            DVector3 sumforce = new DVector3();
            sumforce = DVector3.Zero;
            //DVector3 displacementVector;
           // displacementVector =mPosition - thePosition;
           // double distance = displacementVector.Length();
                
            double tmpR = 0;
            double R = 0;

                    tmpR = Math.Pow((mPosition.X - thePosition.X), 2) + Math.Pow((mPosition.Y - thePosition.Y), 2) + Math.Pow((mPosition.Z - thePosition.Z), 2);
                    
                     tmpR = this.Mass * Config.G  / Math.Pow(tmpR, 1.5);

            // I have no ideas which will work out faster - math.pow for x^2 is half as fast - but god knows how vector3.Length() will work.
            // http://mindstudies.psy.soton.ac.uk/dmitri/blog/index.php/archives/175

           /// tmpR= mMass  * Constants.G  /( distance*distance );

                   sumforce.X = (mPosition.X - thePosition.X) * (float)tmpR; //sigh vector3 uses floats - teh suq
                   sumforce.Y =  (mPosition.Y - thePosition.Y) * (float)tmpR;
                   sumforce.Z =  (mPosition.Z - thePosition.Z) * (float)tmpR;

                   // sumforce.X  =  displacementVector.X  * tmpR;
                   // sumforce.Y = displacementVector.Y * tmpR;
                   // sumforce.Z = displacementVector.Z * tmpR;
            
            return sumforce;
        }          

        #region  Overridden methods

        public override void Draw(float GameTime, Camera theCamera)
        {
            mModel.Draw( theCamera,mDrawPosition,mDrawRotation );
        }

        #endregion


    }
}
