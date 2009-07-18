using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground
{
    /// <summary>
    /// If an object wants to be moved around then it needs to implement this interface.
    /// </summary>
    interface ICanMove
    {
        DisplacementStructure DequeueDisplacement();
        void ExecuteDisplacementStructure(DisplacementStructure theStructure);


    }
}
