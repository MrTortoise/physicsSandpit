using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground
{
    /// <summary>
    /// A global int counter class that increments on every GetNExtID call
    /// For internal use only - not data storage.
    /// </summary>
    public  class GlobalIDGenerator
    {
        static private int mCount = 0;

        // the idea is that eventually we will need Id reuse when objects are destroyed.
        // this class can manage that ... we can force objects that get an id to implement IDisposable or summit)


        protected GlobalIDGenerator()
        { }

        static public int GetNextID()
        {
            mCount++;
            return mCount;
        }

        static public int GetCurrentID()
        {
            return mCount;
        }
    }
}
