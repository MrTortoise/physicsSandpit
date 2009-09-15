using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using _3dplayground.Maths;

namespace _3dplayground
{
    /// <summary>
    /// General Interface to define an object in 3d space and retrieval of its basic properties.
    /// </summary>
    public interface IAmInSpace : IHasName 
    {
        DVector3  Position
        { get; } 

        Quaternion  Rotation
        { get; }

        GameSpaceUnit Space
        { get; }

        DVector3 UpVector
        { get; }



    }
}
