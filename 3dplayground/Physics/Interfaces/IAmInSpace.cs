using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground.Physics
{
    /// <summary>
    /// General Interface to define an object in 3d space and retrieval of its basic properties.
    /// </summary>
    public interface IAmInSpace : IHasName 
    {
        Vector3 Position
        { get; } 

        Quaternion  Rotation
        { get; }

        GameSpaceUnit Space
        { get; } 


    }
}
