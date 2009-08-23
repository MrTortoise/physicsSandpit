using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _3dplayground.Graphics.D3
{
    /// <summary>
    /// This class provides basic model funcitonality and can be used as a component or as a basis for inheritence.
    /// </summary>
   public  class BasicModel  : IModel 
    {

       protected  Model mModel;
       protected bool mIsDrawActive = true;

       protected string mName;
       protected string mContentName;
       protected int mID;

       protected BoundingBox mBoundingBox;
       protected BoundingBox mLargeBoundingBox;
       protected BoundingSphere mBoundingSphere;

       public BasicModel(string theName, string ContentName)
       {
           mName = theName;
           mContentName = ContentName;
       }


       #region IModel Members

       public BoundingBox CompoundBoundingBox
       {
           get { return mBoundingBox ; }
       }

       public BoundingSphere  CompoundBoundingSphere
       {
           get { return mBoundingSphere; }
       }

       public BoundingBox CompoundSafeBoundingBox
       {
           get { return mLargeBoundingBox; }
       }

       public void CalculateBoundingBoxes()
       {
           CalculateCompoundBoundingBox();
           CalculateCompoundBoundingSphere();
           CalculateCompoundSafeBoundingBox();
       }

       public void CalculateCompoundBoundingBox()
       {
           mBoundingBox = BoundingHelper.CalculateBox(mModel);
       }

       public void CalculateCompoundBoundingSphere()
       {
           foreach (ModelMesh m in mModel.Meshes)
           {
               mBoundingSphere = BoundingSphere.CreateMerged(mBoundingSphere, m.BoundingSphere);
           }   
       }

       public void CalculateCompoundSafeBoundingBox()
       {
           mLargeBoundingBox = BoundingBox.CreateFromSphere(mBoundingSphere);
       }

       #endregion


        #region ILoadable Members

        public virtual  void LoadContent(ContentManager theContentManager)
        {
            mModel = theContentManager.Load<Model>(mContentName);
            CalculateBoundingBoxes();
        }

        public string ContentName
        {
            get { return mContentName; }
            set { mContentName = value; }
        }

        #endregion



        #region IDrawable Members

        public bool IsDrawActive
        {
            get { return mIsDrawActive; }
        }

        public void Draw(Camera theCamera, Vector3 thePosition, Quaternion theRotation)
        {
            //ToDo: havent implemented rotation ... not really plyed with quaterions before
            // - they are vector 3's with a 4th float.
            Matrix[] transforms = new Matrix[mModel.Bones.Count];
            mModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in mModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = theCamera.View;
                    effect.Projection = theCamera.Projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(thePosition);
                }
                mesh.Draw();
            }
        }


        #endregion

        #region IHasName Members
        

        public string Name
        {
            get { return mName; }
        }

        public int ID
        {
            get { return mID; }
        }
        #endregion

    }
}
