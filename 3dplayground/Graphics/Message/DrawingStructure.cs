using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.Xna.Framework;

namespace _3dplayground.Graphics
{


    /// <summary>
    /// This structure is an amorphous struct. I am specifying the field offsets to override its memory footprint
    /// This allows me to use the same structure in multiple ways - I can't inherit it.
    /// It needs to be a structure or else the garbage collection overhead will huge.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct  DrawingStructure 
    {

        [FieldOffset(0)]
        public int ID;

        [FieldOffset(4)]
        public bool IsActive;

        [FieldOffset(5)]
        public Vector3 Position;

        [FieldOffset(17)]
        public Quaternion Rotation;



       /// <summary>
       /// This constructor populates ID, Position and Velocity. Good for a basic physics drawing object with no dependancies on any other updateable fields.
       /// </summary>
       /// <param name="theID"></param>
       /// <param name="thePosition"></param>
       /// <param name="theRotation"></param>
        public DrawingStructure(int theID,bool theIsDrawActive, Vector3 thePosition, Quaternion theRotation)
        {
            IsActive = theIsDrawActive;
            ID = theID;
            Position = thePosition;
            Rotation = theRotation;

        }



    }
}
