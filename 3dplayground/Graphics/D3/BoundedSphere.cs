using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _3dplayground.Graphics.D3
{
    /// <summary>
    /// This class forms the basis for all models that will use a spherical bounding box.
    /// </summary>
    public class BoundedSphere : BasicModel , IHasBoundingSphere  
    {   
        // This class needs several things. 
        // 2. From this calculation of the models size
        // 3. Write a scaling algorithm so we can specify the render size of the model.
        // 4. Implement the rotation stuff.

        // 5. Repeat in a new class, but for a suboid model.

        Model mModel;
        BoundingSphere mBoundingSphere;
        Matrix mScalingMatrix;

        public void LoadContent(ContentManager contentManager, string contentName)
        {
            base.LoadContent(contentManager, contentName);
            
            //Iterate through the meshes and force the bounding sphere to grow to fit them all.
            foreach (ModelMesh m in mModel.Meshes)
            {
                mBoundingSphere = BoundingSphere.CreateMerged(mBoundingSphere, m.BoundingSphere);
            }

            
        }

        #region IModel Members



        #endregion

        #region IHasBoundingSphere Members

        public BoundingSphere  boundingSphere
        {
            get { return mBoundingSphere; }
        }

        public float Radius
        {
            get { return mBoundingSphere.Radius; }
        }

        #endregion
    }





}
