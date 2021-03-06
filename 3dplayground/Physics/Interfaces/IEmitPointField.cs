﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using _3dplayground.Maths;

namespace _3dplayground.Physics
{
    /// <summary>
    /// Implement this for the bare minimum required to Emit a Field
    /// </summary>
    public interface IEmitPointField : IAmInSpace 
    { 

        /// <summary>
        /// Given a position and a magnitude of the scalar the force operates on will output whatever is useful -?rick what
        /// </summary>
        /// <param name="thePosition">The position of the object in space</param>       
        /// <returns></returns>
        DVector3  PointFieldAcceleration(DVector3 thePosition); 
    }
}
