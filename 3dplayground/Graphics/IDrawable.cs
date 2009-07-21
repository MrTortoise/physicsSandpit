﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dplayground
{
    interface IDrawable : IHasName 
    {
        void Draw(Camera theCamera);

        bool IsDrawActive
        { get; }
    }
}