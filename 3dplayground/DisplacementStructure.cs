using System;

using Microsoft.Xna.Framework;
using _3dplayground.Physics;
using _3dplayground.Maths;

namespace _3dplayground
{
    /// <summary>
    /// This structure contains the Displacement vectors .
    /// It is used to allow the containing space object to rectify any physics collisions and then apply the changes back to the object in quetsion.
    /// It is totally independant of draw logic, draw has its own objects
    /// </summary>
    public struct DisplacementStructure : IEquatable<DisplacementStructure>
    {

        //TpDo: Implement rotation into displacement structure

        //Feel free to add any other operators as needed.
         
        #region Static Methods
        /// <summary>
        /// Takes 2 dsplacement structures and returns a new displacement structure representing their combination.
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
                s1.Velocity, s2.DeltaVelocity  + s1.DeltaVelocity,
                s1.Rotation, s1.DeltaRotation + s2.DeltaRotation);
     

            return retVal;

        }

        public static DisplacementStructure operator +(DisplacementStructure s1, DisplacementStructure s2)
        {
            return Add(s1, s2);
        }
        
        /// <summary>
        /// constructs a Displacement structure that uses the Source object, its position and its velocity
        /// to create a displacement structure that represents no change int hese values.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DisplacementStructure ZeroDeltas(IPhysicsObject source)
        {
            return new DisplacementStructure(source, source.Position, source.Velocity,source.Rotation   );
        }

        #endregion
        #region Fields
        // if we use an object reference it will point to somewhere bad, if we clone then we copy too much data ...
         IPhysicsObject   mIamInSpace;

        DVector3  mPosition;
        DVector3 mDeltaPosition;

        DVector3 mVelocity;
        DVector3 mDeltaVelocity;

        Quaternion mRotation;
        Quaternion mDeltaRotation;


        #endregion
        #region Constructor
                                    
        public DisplacementStructure(IPhysicsObject  theObject,
            DVector3 TheOrigionalPosition, DVector3 TheDeltaPosition,
            DVector3 TheOrigionalVelocity, DVector3 TheDeltaVelocity,
            Quaternion theOrigionalRotation, Quaternion theDeltaRoation)
        {
            mIamInSpace = theObject;

            mPosition = TheOrigionalPosition;
            mVelocity = TheOrigionalVelocity;                          

            mDeltaPosition = TheDeltaPosition;
            mDeltaVelocity = TheDeltaVelocity;

            mRotation = theOrigionalRotation;
            mDeltaRotation = theDeltaRoation;
        }

        /// <summary>
        /// This constructor can be used in cases where there is no change in velocity or position
        /// </summary>
        /// <param name="theObject"></param>
        /// <param name="thePosition"></param>
        /// <param name="theVelocity"></param>
        /// <param name="theRotation"></param>
        public DisplacementStructure(IPhysicsObject theObject, DVector3 thePosition, DVector3 theVelocity, Quaternion theRotation)
        {
            mIamInSpace = theObject;

            mPosition = thePosition;
            mVelocity = theVelocity;
            mRotation = theRotation;

      
            mDeltaPosition = DVector3.Zero;
            mDeltaVelocity = DVector3.Zero;
            mDeltaRotation = Quaternion.Identity;
        }
        #endregion

        #region Properties

        public IPhysicsObject IPhysicsObject
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
        /// Gets / Sets the rotation quaterion
        /// </summary>
        public Quaternion Rotation
        {
            get { return mRotation; }
            set { mRotation = value; }
        }

        /// <summary>
        /// Gets / Sets the Change in Rotation to apply
        /// </summary>
        public Quaternion DeltaRotation
        {
            get { return mDeltaRotation; }
            set {  mDeltaRotation=value ; }
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
                            if (other.mRotation == mRotation)
                            {
                                if (other.mDeltaRotation == mDeltaRotation)
                                {
                                    if (other.Name == mIamInSpace.Name)
                                    {
                                        retVal = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return retVal;

        }

        public bool HasNoDeltas()
        {
            bool retVal = true ;
            if ((mDeltaPosition != DVector3.Zero) || (mDeltaVelocity != DVector3.Zero) || (mDeltaRotation!=Quaternion.Identity))
            {
                retVal = false;
            }
            return retVal;
        }

        #endregion
    }
}
