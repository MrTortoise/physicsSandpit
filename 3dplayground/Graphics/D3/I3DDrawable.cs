using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground.Graphics.D3
{
    /// <summary>
    /// Thi sinterface is implemented by the actual object being drawn.
    /// Thi sobject will be contained in an object implementing IGameDrawable.
    /// It needs to have the position and rotation passed into it.
    /// </summary>
   public  interface I3DDrawable : IHasName, IGameDrawable  
    {          
        Vector3  DrawPosition
        { get; }

        Quaternion DrawRotation
        { get; }       

    }

}
