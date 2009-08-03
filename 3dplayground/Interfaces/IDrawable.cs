﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using _3dplayground.Maths;

namespace _3dplayground
{
   public  interface IDrawable : IHasName 
    {
        void Draw(Camera theCamera, Vector3  thePosition, Quaternion  theRotation);

        bool IsDrawActive
        { get; }
    }
}
