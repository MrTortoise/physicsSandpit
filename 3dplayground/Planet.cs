﻿using System;
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
            DVector3 displacementVector;
            displacementVector =mPosition - thePosition;
            double distance = displacementVector.Length();
                
            double tmpR = 0;  

                   // tmpR = Math.Pow((i.Position.X - pos.X), 2) + Math.Pow((i.Position.Y - pos.Y), 2) + Math.Pow((i.Position.Z - pos.Z), 2);
                   // tmpR = mMass  * Constants.G  / Math.Pow(tmpR, 1.5);

            // I have no ideas which will work out faster - math.pow for x^2 is half as fast - but god knows how vector3.Length() will work.
            // http://mindstudies.psy.soton.ac.uk/dmitri/blog/index.php/archives/175

            tmpR= mMass  * Constants.G  /( distance*distance );

              //      sumforce.X = sumforce.X + (i.Position.X - pos.X) * (float)tmpR; //sigh vector3 uses floats - teh suq
               //     sumforce.Y = sumforce.Y + (i.Position.Y - pos.Y) * (float)tmpR;
              //      sumforce.Z = sumforce.Z + (i.Position.Z - pos.Z) * (float)tmpR;

                    sumforce.X  =  displacementVector.X  * tmpR;
                    sumforce.Y = displacementVector.Y * tmpR;
                    sumforce.Z = displacementVector.Z * tmpR;
            
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
