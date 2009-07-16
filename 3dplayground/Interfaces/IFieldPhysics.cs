using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    /// <summary>
    /// This is the interface that a component will implement if it is to provide an implementation of mFieldPhysics physics.    /// 
    /// </summary>
    interface IFieldPhysics
    {
        Vector3 NewPosition(Vector3 inputargs, GameTime here);
        //ToDo: Rick: i have no idea what imput parameters you need or the return type. 
        //If you need to return an args object so be it - although output params maybe ...
        // anyway ... returning a some kind of a vector would be nice.

        // so you would define any properties such an object needs to expose (wether read or write)
        // also any events - not sure yet wether you need any.
    }
}
