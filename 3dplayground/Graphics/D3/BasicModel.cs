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
       protected int mID = GlobalIDGenerator.GetNextID();

       protected BoundingBox mBoundingBox;
       protected BoundingBox mLargeBoundingBox;
       protected BoundingSphere mBoundingSphere;

       public BasicModel(string theName, string ContentName)
       {
           mName = theName;
           mContentName = ContentName;
       }


       #region IModel Members

       /// <summary>
       /// This is the axis aligned bounding box for the model as it is loaded.
       /// This box will not correctly detect collisions with a rotated model.
       /// </summary>
       public BoundingBox CompoundBoundingBox
       {
           get { return mBoundingBox ; }
       }

       /// <summary>
       /// This is the bounding sphere of the model as it is loaded.
       /// </summary>
       public BoundingSphere  CompoundBoundingSphere
       {
           get { return mBoundingSphere; }
       }

       /// <summary>
       /// This is the axis aligned bounding box for the bounding sphere - represents an axis aligned bounding box that is rotation independant.
       /// </summary>
       /// <remarks>
       /// If we know what direction we are facing i aimagine this can be used as part of an object cull
       /// or possibly for fast collision detection that is independant of objects rotation
       /// </remarks>
       public BoundingBox CompoundSafeBoundingBox
       {
           get { return mLargeBoundingBox; }
       }

       /// <summary>
       /// Calculates all of the bounding boxes for this model.
       /// This is a potentially expensive operation and shuld not be used once the object loading phase is complete
       /// </summary>
       public void CalculateBoundingBoxes()
       {
           CalculateCompoundBoundingBox();
           CalculateCompoundBoundingSphere();
           CalculateCompoundSafeBoundingBox();
       }

       /// <summary>
       /// calculates the Bounding box for the model as it is loaded.
       /// This is a potentially expensive operation and shuld not be used once the object loading phase is complete
       /// </summary>
       public void CalculateCompoundBoundingBox()
       {
           mBoundingBox = BoundingHelper.CalculateBox(mModel);
       }

       /// <summary>
       /// Calculates the Bounding Sphere for the model as it is Loaded.
       /// This is a potentially expensive operation and shuld not be used once the object loading phase is complete
       /// </summary>
       public void CalculateCompoundBoundingSphere()
       {
           foreach (ModelMesh m in mModel.Meshes)
           {
               mBoundingSphere = BoundingSphere.CreateMerged(mBoundingSphere, m.BoundingSphere);
           }   
       }

       /// <summary>
       /// Calculates the Bounding Box for the Bounding Sphere as it is loaded.
       /// This is a potentially expensive operation and shuld not be used once the object loading phase is complete
       /// </summary>
       public void CalculateCompoundSafeBoundingBox()
       {
           mLargeBoundingBox = BoundingBox.CreateFromSphere(mBoundingSphere);
       }

       #endregion


        #region ILoadable Members

       /// <summary>
       /// Virtual, Loads th emodel and calculates its bounding boxes.
       /// </summary>
       /// <param name="theContentManager"></param>
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
