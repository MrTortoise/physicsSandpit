using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _3dplayground.Graphics.D3
{
   public  class BasicModel  : IModel 
    {

       protected  Model mModel;        

        #region IModel Members 

        public void draw(Vector3 position, Quaternion rotation, Camera theCamera)
        {
            // havent implemented rotation ... not really plyed with quaterions before.
            Matrix[] transforms = new Matrix[mModel.Bones.Count];
            mModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in mModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = theCamera.View;
                    effect.Projection = theCamera.Projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(position);
                }
                mesh.Draw();
            }
        }

        #endregion

        #region ILoadable Members

        public void LoadContent(ContentManager theContentManager, string ContentName)
        {
            mModel = theContentManager.Load<Model>(ContentName);
        }

        #endregion
    }
}
