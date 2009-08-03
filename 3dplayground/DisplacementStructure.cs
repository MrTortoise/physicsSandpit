using System;

using Microsoft.Xna.Framework;
using _3dplayground.Physics;
using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// This structure contains the Displacement vectors - NOT THE ABSOLUTE VECTORS.
    /// It is used to allow the containing space object to rectify any physics collisions and then apply the changes back to the object in quetsion.
    /// </summary>
    public struct DisplacementStructure
    {
        #region Static Methods
        /// <summary>
        /// Takes 2 dsplacement structures and returns a displacement structure representing their combination
        /// uses the IaMInSpace of s1 ... calculates s2+s1
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static DisplacementStructure CombineStructure(DisplacementStructure s1,DisplacementStructure s2)
        {

            if ((s1.Position != s2.Position) || (s1.Velocity != s2.Velocity) )
            {
                ArgumentException e = new ArgumentException("Exception whilst combining 2 Displacement Structures. They do not contain the same base locations.");
                throw e;

            }

            DisplacementStructure retVal = new DisplacementStructure(s1.mIamInSpace,
                s1.Position, s2.DeltaPosition + s1.DeltaPosition,
                s1.Velocity, s2.Velocity + s1.Velocity);
     

            return retVal;

        }

        /// <summary>
        /// Creates a displacement structure that preserves the origional position but has - as a value for all deltas.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DisplacementStructure ZeroDeltas(IAmInSpace source)
        {
            return new DisplacementStructure(source, source.Position, DVector3.Zero);
        }

        public static DisplacementStructure ZeroDeltas(ICanMove  source)
        {
            return new DisplacementStructure(source, source.Position, source.Velocity  );
        }

        #endregion
        #region Fields
        // if we use an object reference it will point to somewhere bad, if we clone then we copy too much data ...
         IAmInSpace  mIamInSpace;

        DVector3  mPosition;
        DVector3 mDeltaPosition;

        DVector3 mVelocity;
        DVector3 mDeltaVelocity;


        #endregion
        #region Constructor
                                    
        public DisplacementStructure(IAmInSpace theObject,
            DVector3 TheOrigionalPosition, DVector3 TheDeltaPosition,
            DVector3 TheOrigionalVelocity, DVector3 TheDeltaVelocity)
        {
            mIamInSpace = theObject;

            mPosition = TheOrigionalPosition;
            mVelocity = TheOrigionalVelocity;


            mDeltaPosition = TheDeltaPosition;
            mDeltaVelocity = TheDeltaVelocity;
        }

        public DisplacementStructure(IAmInSpace theObject, DVector3 thePosition, DVector3 theVelocity)
        {
            mIamInSpace = theObject;
            mPosition = thePosition;
            mVelocity = theVelocity;
     

      
            mDeltaPosition = DVector3.Zero;
            mDeltaVelocity = DVector3.Zero;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the position of the object (prior to the change vector)
        /// </summary>
        public DVector3 Position
        { get { return mPosition; } }
        /// <summary>
        /// Gets the change in position 
        /// </summary>
        public DVector3 DeltaPosition
        { get { return mDeltaPosition; } }

        /// <summary>
        /// Gets the velocity of the object (prior to the change vector)
        /// </summary>
        public DVector3 Velocity
        { get { return mVelocity ; } }
        /// <summary>
        /// Gets the change in Velocity 
        /// </summary>
        public DVector3 DeltaVelocity
        { get { return mDeltaVelocity; } }


         /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name
        { get { return mIamInSpace.Name; } }

        #endregion
    }
}
