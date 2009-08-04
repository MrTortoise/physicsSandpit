using System;

using Microsoft.Xna.Framework;
using _3dplayground.Physics;
using _3dplayground.Maths;

namespace _3dplayground
{
    /// <summary>
    /// This structure contains the Displacement vectors .
    /// It is used to allow the containing space object to rectify any physics collisions and then apply the changes back to the object in quetsion.
    /// </summary>
    public struct DisplacementStructure : IEquatable<DisplacementStructure>
    {
         
        #region Static Methods
        /// <summary>
        /// Takes 2 dsplacement structures and returns a displacement structure representing their combination.
        /// Their absolutes should be the same, the deltas will be added
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <exception cref="ArgumentException">Occurs when both structures vary by position or velocity</exception>
        /// <returns></returns>
        public static DisplacementStructure Add(DisplacementStructure s1,DisplacementStructure s2)
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

        public static DisplacementStructure operator +(DisplacementStructure s1, DisplacementStructure s2)
        {
            return Add(s1, s2);
        }
        
        public static DisplacementStructure ZeroDeltas(IPhysicsObject source)
        {
            return new DisplacementStructure(source, source.Position, source.Velocity  );
        }

        #endregion
        #region Fields
        // if we use an object reference it will point to somewhere bad, if we clone then we copy too much data ...
         IPhysicsObject   mIamInSpace;

        DVector3  mPosition;
        DVector3 mDeltaPosition;

        DVector3 mVelocity;
        DVector3 mDeltaVelocity;


        #endregion
        #region Constructor
                                    
        public DisplacementStructure(IPhysicsObject  theObject,
            DVector3 TheOrigionalPosition, DVector3 TheDeltaPosition,
            DVector3 TheOrigionalVelocity, DVector3 TheDeltaVelocity)
        {
            mIamInSpace = theObject;

            mPosition = TheOrigionalPosition;
            mVelocity = TheOrigionalVelocity;


            mDeltaPosition = TheDeltaPosition;
            mDeltaVelocity = TheDeltaVelocity;
        }

        public DisplacementStructure(IPhysicsObject theObject, DVector3 thePosition, DVector3 theVelocity)
        {
            mIamInSpace = theObject;
            mPosition = thePosition;
            mVelocity = theVelocity;
     

      
            mDeltaPosition = DVector3.Zero;
            mDeltaVelocity = DVector3.Zero;
        }
        #endregion

        #region Properties

        public IPhysicsObject IAmInSpace
        { get { return mIamInSpace; } }

        /// <summary>
        /// Gets / Sets the position of the object (prior to the change vector)
        /// </summary>
        public DVector3 Position
        { get { return mPosition; }
            set { mPosition = value; }
        }
        /// <summary>
        /// Gets/ Sets the change in position 
        /// </summary>
        public DVector3 DeltaPosition
        {
            get { return mDeltaPosition; }
            set { mDeltaPosition = value; }
        }

        /// <summary>
        /// Gets / Sets the velocity of the object (prior to the change vector)
        /// </summary>
        public DVector3 Velocity
        { get { return mVelocity ; }
            set { mVelocity = value; }
        }
        /// <summary>
        /// Gets / Sets the change in Velocity 
        /// </summary>
        public DVector3 DeltaVelocity
        { get { return mDeltaVelocity; }
            set { mDeltaVelocity = value; }
        }        

         /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name
        { get { return mIamInSpace.Name; }         }

        public double Mass
        { get { return mIamInSpace.Mass; } }

        #endregion

        #region IEquatable<DisplacementStructure> Members

        public bool Equals(DisplacementStructure other)
        {
            bool retVal = false;
            //Looks odd but i rekon this is the order of most likley to least likley to fail.

            if (other.Position == mPosition)
            {
                if (other.Velocity == mVelocity)
                {
                    if (other.DeltaPosition == mDeltaPosition)
                    {
                        if (other.mDeltaVelocity == mDeltaVelocity)
                        {
                            if (other.Name == mIamInSpace.Name)
                            {
                                retVal = true;
                            }
                        } 
                    }
                }
            }
            return retVal;

        }

        public bool HasNoDeltas()
        {
            bool retVal = false ;
            if ((mDeltaPosition == DVector3.Zero) && (mDeltaVelocity == DVector3.Zero))
            {
                retVal = true;
            }
            return retVal;
        }

        #endregion
    }
}
