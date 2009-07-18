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
    struct DisplacementStructure
    {
         IAmInSpace  IamInSpace;
         Vector3 DeltaPosition;
         Vector3 DeltaVelocity;
         Quaternion DeltaRotation;

        /// <summary>
        /// Constructs the DisplacementStructure
        /// </summary>
        /// <param name="theObject">The object that implements the interface.</param>
        /// <param name="ThePosition">The vector describing the change in position.</param>
        /// <param name="theVelocity">The vectors describing the change in velocity.</param>
        /// <param name="theRotiation">The vector describing the change in rotation.</param>                                     
        public DisplacementStructure(IAmInSpace theObject, Vector3 ThePosition, Vector3 theVelocity, Quaternion theRotiation)
        {
           IamInSpace = theObject;
           DeltaPosition = ThePosition;
           DeltaVelocity = theVelocity;
           DeltaRotation = theRotiation;

        }




        public Vector3 Position
        { get { return IamInSpace.Position; } }

        public Quaternion Rotation
        { get { return IamInSpace.Rotation; } }

        public Vector3 Velocity
        { get { return IamInSpace.Velocity; } }

        public string Name
        { get { return IamInSpace.Name; } }


    }
}
