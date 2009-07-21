using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    /// <summary>
    /// This structure contains the Displacement vectors - NOT THE ABSOLUTE VECTORS
    /// </summary>
    public struct DisplacementStructure
    {
         IAmInSpace  mIamInSpace;
         Vector3 mDeltaPosition;
         Vector3 mDeltaVelocity;
         Quaternion mDeltaRotation;
         Quaternion mDeltaAngularRotation;

        /// <summary>
        /// Constructs the DisplacementStructurewith no velocities
        /// </summary>
        /// <param name="theObject">The object that implements the interface.</param>
        /// <param name="ThePosition">The vector describing the change in position.</param>
        /// <param name="theRotiation">The vector describing the change in rotation.</param>                                     
        public DisplacementStructure(IAmInSpace theObject, Vector3 ThePosition,  Quaternion theRotiation)
        {
           mIamInSpace = theObject;
           mDeltaPosition = ThePosition;
           mDeltaRotation = theRotiation;
           mDeltaVelocity = Vector3.Zero;
            mDeltaAngularRotation = Quaternion.Identity;

        }

        /// <summary>
        /// constructs the displacement structure with velicoties.
        /// </summary>
        /// <param name="theObject">The object that implements the IAmInSpace interface.</param>
        /// <param name="ThePosition">The chang ein position of the object</param>
        /// <param name="theVelocity">The change in velocity of the object</param>
        /// <param name="theRotiation">The change in rotation of te object</param>
        /// <param name="theAngularVelocity">the change in the angular velocity of the object.</param>
        public DisplacementStructure(IAmInSpace theObject, Vector3 ThePosition, Vector3 theVelocity,
            Quaternion theRotiation, Quaternion theAngularVelocity)
        {
            mIamInSpace = theObject;
            mDeltaPosition = ThePosition;
            mDeltaRotation = theRotiation;
            mDeltaVelocity = theVelocity;
            mDeltaAngularRotation = theAngularVelocity;

        }

        /// <summary>
        /// Gets the change in position 
        /// </summary>
        public Vector3 DeltaPosition
        { get { return mDeltaPosition; } }

        /// <summary>
        /// Gets the change in rotation
        /// </summary>
        public Quaternion DeltaRotation
        { get { return mDeltaRotation; } }

        /// <summary>
        /// Gets the change in velocity
        /// </summary>
        public Vector3 DeltaVelocity
        { get { return mDeltaVelocity; } }

        /// <summary>
        /// Getst he change in angular velocity
        /// </summary>
        public Quaternion  DeltaAngularVelocity
        { get { return mDeltaAngularRotation; } }

        /// <summary>
        /// Gets the position of the object (prior to the change vector)
        /// </summary>
        public Vector3 Position
        { get { return mIamInSpace.Position; } }

        /// <summary>
        /// Gets the rotation of the object prior to the change vecotr
        /// </summary>
        public Quaternion Rotation
        { get { return mIamInSpace.Rotation; } }

        /// <summary>
        /// Gets the velocity of the object prior to the change vector.
        /// </summary>
        public Vector3 Velocity
        {
            get
            {
                Vector3 retVal;
                ICanMove temp;
                temp = mIamInSpace as ICanMove;
                if (temp != null)
                { retVal = temp.Velocity; }
                else
                { retVal = Vector3.Zero; }
                return retVal;
            }
        }
         

        /// <summary>
        /// Gets the angular velocity of the object prior to the change vector.
        /// </summary>
        public Quaternion AngularVelocity
        {
            get
            {
                Quaternion retVal;
                ICanMove temp;
                temp = mIamInSpace as ICanMove;
                if (temp != null)
                { retVal = temp.AngularVelocity; }
                else
                { retVal = Quaternion.Identity; }
                return retVal;
            }
        }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name
        { get { return mIamInSpace.Name; } }


    }
}
