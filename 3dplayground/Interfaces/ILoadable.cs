using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _3dplayground
{
    interface ILoadable : IHasName 
    {
        string ContentName
        { get; set; }

        void LoadContent(ContentManager theContentManager);

    }
}
