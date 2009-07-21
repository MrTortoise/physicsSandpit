using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3dplayground
{
    /// <summary>
    /// If an object wants to be moved around then it needs to implement this interface.
    /// </summary>
    public interface ICanMove
    {
        Vector3 Velocity
        { get; }

        Quaternion AngularVelocity
        { get; }
                 

        event EventHandler<DisplacementArgs> RequestMove;
        void RaiseRequestMove(DisplacementArgs theArgs);

        void ExecuteDisplacementStructure(DisplacementStructure theStructure);


    }
}
