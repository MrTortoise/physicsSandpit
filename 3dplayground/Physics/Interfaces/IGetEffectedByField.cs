using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground.Physics
{
    /// <summary>
    /// Implement this interface to be added to the list of objects that can be effected by fields.
    /// </summary>
    /// <remarks>I anticipate that we are going to need a more general IGetEffectedByPointField interface. 
    /// We will then need to inherit from this to get to different types of Point Field</remarks>
    public interface IGetEffectedByGravity : IPhysicsObject   
    {
        
 
        void CalculateGravityDisplacement(float   theTime);

        DisplacementStructure GravityDisplacement
        { get; }


    }
}
