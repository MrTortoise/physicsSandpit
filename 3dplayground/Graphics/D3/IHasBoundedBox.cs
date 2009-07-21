using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _3dplayground.Graphics.D3
{
    interface IHasBoundedBox
    {

        BoundingBox boundingBox
        { get; }

        Vector3 Max
        { get; }

        Vector3 Min
        { get; }

        Vector3 Volume
        { get; }


    }
}
