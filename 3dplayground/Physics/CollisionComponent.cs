using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground.Physics
{
    /// <summary>
    /// This Component Tests for and resolves Collisions between objects in sapce
    /// </summary>
    sealed class CollisionComponent
    {

        public void PerformCollisionResolution()
        {

        }

        /// <summary>
        /// This method takes 2 Displacement Structures as Args Its then Tests for a Collision and returns True if It occurs
        /// </summary>
        /// <param name="obj1">Any IPhysicsObject</param>
        /// <param name="obj2">Any IPhysicsObject</param>
        /// <returns>void, the values are written to the input parameter references.</returns>
        private  bool TestForCollision(DisplacementStructure obj1, DisplacementStructure obj2)
        {
            return false;
            //ToDo: Rick good one for you ;) 

            //although i think you will be pleasantly suprised when you look at the methods of the bounding box classes.

        }

        /// <summary>
        /// This function Resolves the collision between 2 spherical objects.
        /// This function accepts 2 displacement parameters a references, calculates the results and writes them to the references.
        /// </summary>
        /// <param name="obj1">A spherical object</param>
        /// <param name="obj2">A spherical object</param>
        private void ResolveSphericalCollision(ref DisplacementStructure obj1, ref DisplacementStructure obj2)
        { }


    



       
    }
}
