using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    /// <summary>
    /// Implement this for the bare minimum required to Emit a Field
    /// </summary>
    interface IEmitPointField : IAmInSpace 
    { 

        /// <summary>
        /// Given a position and a magnitude of the scalar the force operates on will output whatever is useful -?rick what
        /// </summary>
        /// <param name="thePosition">The position of the object in space</param>
        /// <param name="magnitude">The magnitude of whatever scalar the component needs to calculate its force</param>
        /// <returns></returns>
        Vector3 Force(Vector3 thePosition, int magnitude); 
    }
}
