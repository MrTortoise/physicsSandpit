using System;

using Microsoft.Xna.Framework;

using _3dplayground.Graphics;

namespace _3dplayground.Graphics.D3
{
    /// <summary>
    /// This class contains all the args required to draw a particular object. 
    /// If more args are reuired then inherit and make the implementing objct take care of the differences.
    /// The object that creates these args in its update will also be consuming it during its draw.
    /// </summary>
    class  D3DrawableArgs   
    {

      // ned to be able to extend tis. . dont want to use a bit field - seems ugly (but fast).
        // also have to ask how to debug properly ... working from a binary stream or byte array 
        // to figure out what snt it and why the data fails doesn;t sound like sunday afternoon fun.

        // so we will have to use a class ... (thi showever uses garbage collection)
        // does this mean that we need to have a reusable collection of these objects?


        public Camera Camera;
        public Vector3  Position; 
        public Quaternion  Rotation;
        public float TimeSpan;


        public D3DrawableArgs(Camera theCamera, Vector3 thePosition, Quaternion theRotation, float theTimeSpan)
        {
            Camera = theCamera;
            Position = thePosition;
            Rotation = theRotation;
            TimeSpan = theTimeSpan;
        }





    }
}
