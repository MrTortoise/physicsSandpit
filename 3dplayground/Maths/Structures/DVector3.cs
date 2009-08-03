using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground.Maths
{
    struct DVector3 : IEquatable<DVector3>
    {

        /// <summary>
        /// Takes references to the 2 input structures and returns a new struct containing the answer.
        /// </summary>
        /// <param name="SubtractFrom"></param>
        /// <param name="ValueToSubtract"></param>
        /// <returns></returns>
        public static DVector3  Subtract(ref DVector3 SubtractFrom, ref DVector3 ValueToSubtract)
        {
            DVector3 retVal = new DVector3();

            retVal.mX = SubtractFrom.X - ValueToSubtract.mX;
            retVal.mY = SubtractFrom.mY - ValueToSubtract.mY;
            retVal.mZ = SubtractFrom.mZ - ValueToSubtract.mZ;

            return retVal;
        }


        /// <summary>
        /// Takes a reference to the 2 vectors to add and returns a new struct containing the answer.
        /// </summary>
        /// <param name="V1"></param>
        /// <param name="V2"></param>
        /// <returns></returns>
        public static DVector3 Add(ref DVector3 V1, ref DVector3 V2)
        {
            DVector3 retVal = new DVector3();

            retVal.mX = V1.mX + V2.mX;
            retVal.mY = V1.mY + V2.mY;
            retVal.mZ = V1.mZ + V2.mZ;
            return retVal;
        }


        public static DVector3 operator -(DVector3 SubtractFrom, DVector3 ValueToSubtract)
        {
            DVector3 retVal = new DVector3();

            retVal.mX = SubtractFrom.mX - ValueToSubtract.mX;
            retVal.mY = SubtractFrom.mY - ValueToSubtract.mY;
            retVal.mZ = SubtractFrom.mZ - ValueToSubtract.mZ;

            return retVal;
        }
        public static DVector3 operator +(DVector3 v1, DVector3 v2)
        {
            DVector3 retVal = new DVector3();

            retVal.mX = v1.mX - v2.mX;
            retVal.mY = v1.mY - v2.mY;
            retVal.mZ = v1.mZ - v2.mZ;

            return retVal;
        }

        //Todo: Implement dot product, vector product and any other Vector Function here as a static method.
        //ToDo: implement division / multiplication by a double, normlaise
        //ToDO: implement any vector functionality we need in here
        //ToDo: implement a better hasch code for DVector3 ... mine is crappy and is probably slow
        //ToDo: implement code to find angles ... sure you know the kind of functions we need better than me. *But make them fast*


        double mX;
        double mY;
        double mZ;

        public DVector3(double x, double y, double z)
        {
            mX = x;
            mY = y;
            mZ = z;
        }


        #region Properties
        public double X
        {
            get { return mX; }
            set { mX = value; }
        }   
        public double Y
        {
            get { return mY; }
            set { mY = value; }
        } 
        public double Z
        {
            get { return mZ; }
            set { mZ = value; }
        }
        #endregion

        #region IEquatable<DVector3> Members

        /// <summary>
        /// Overriden to provide equality funcitonality by Value. 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DVector3 other)
        {
            bool retVal=false;

                if ((other.mX == mX) && (other.mY == mY) && (other.mZ == mZ))
                {
                    retVal = true;
                } 
   
            return retVal;
        }




        #endregion

        #region GeneralOverrides
        public override bool Equals(object obj)
        {

            bool retVal = false;
            DVector3 val;
            try
            {
                val = (DVector3)obj;
                retVal = this.Equals(val);
            }
            catch (Exception e)
            {
                Exception ex = new Exception("Failed to convert obj into DVector3 whilst testing for equality.", e);
                throw ex;
            }

            return retVal;
        }

        public override int GetHashCode()         {  
           
            return mX.GetHashCode() ^ mY.GetHashCode() ^ mZ.GetHashCode();
        }

        public override string ToString()
        {
            string retVal = "DVector3 x=" + mX + ", y={" + mY + " , z=" + mZ;
            return retVal;
        }

        #endregion
    }
}
