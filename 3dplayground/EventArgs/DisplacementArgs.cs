using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground
{
   public class DisplacementArgs : EventArgs 
    {

        protected DisplacementStructure mDisplacement;

        public DisplacementArgs(DisplacementStructure theDisplacement)
        { mDisplacement = theDisplacement; }

        public DisplacementStructure Displacement
        { get { return mDisplacement; } }
    }
}
