using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _3dplayground.Physics;

namespace _3dplayground.Physics
{
    /// <summary>
    /// This structure contains the Displacement vectors - NOT THE ABSOLUTE VECTORS.
    /// It is used to allow the containing space object to rectify any physics collisions and then apply the changes back to the object in quetsion.
    /// </summary>
    public struct DisplacementStructure
    {
        /// <summary>
        /// Takes 2 dsplacement structures and returns a displacement structure representing their combination
        /// uses the IaMInSpace of s1 ... calculates s2-s1
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static DisplacementStructure CombineStructure(DisplacementStructure s1, DisplacementStructure s2)
        {

            DisplacementStructure retVal;

            Vector3 position;
            Vector3 rotation;


            

            
            //ToDo: implement me
            throw new NotImplementedException();  
        
            //morevoer consider rewriting to use one of the variables as an output parameter - gotta be more efficient to reuse.

        }

        public static DisplacementStructure Zero(IAmInSpace source)
        {
            return new DisplacementStructure(source, Vector3.Zero, Quaternion.Identity);
        }


        // if we use an object reference it will point to somewhere bad, if we clone then we copy too much data ...
         IAmInSpace  mIamInSpace;

         Vector3 mPosition;
         Quaternion mRotation;


         Vector3 mDeltaPosition;
         Quaternion mDeltaRotation; 

        /// <summary>
        /// Constructs the DisplacementStructurewith no velocities
        /// </summary>
        /// <param name="theObject">The object that implements the interface.</param>
         /// <param name="TheDeltaPosition">The vector describing the change in position.</param>
         /// <param name="theDeltaRotiation">The vector describing the change in rotation.</param>                                     
        public DisplacementStructure(IAmInSpace theObject, Vector3 TheDeltaPosition,  Quaternion theDeltaRotiation)
        {
           mIamInSpace = theObject;
           mDeltaPosition = TheDeltaPosition;
           mDeltaRotation = theDeltaRotiation;
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
