using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground.Physics
{
    struct New_pos_and_vel
    {
        public Vector3 position;
        public Vector3 velocity;

        public New_pos_and_vel(Vector3 thePosition, Vector3 theVelocity)
        {
            position = thePosition;
            velocity = theVelocity;
        }

    }
}
