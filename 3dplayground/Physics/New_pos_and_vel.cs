using System;

using Microsoft.Xna.Framework;

using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// Does this store deltas or absolutes?
    /// </summary>
    struct New_pos_and_vel
    {
        /// <summary>
        /// 
        /// </summary>
        public DVector3  position;

        /// <summary>
        /// 
        /// </summary>
        public DVector3 velocity;

        public New_pos_and_vel(DVector3 thePosition, DVector3 theVelocity)
        {
            position = thePosition;
            velocity = theVelocity;
        }

    }
}
