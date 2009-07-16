using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _3dplayground
{
   public  class Camera
    {


        float mAspectRatio = 1;
        float mFOV = MathHelper.ToRadians(180-1) ;
        float mNearClip = 1;
        float mFarClip = 100f;

        Matrix mProjection;

        Matrix mView;

        Vector3 mCameraPosition = Vector3.Zero;
        Vector3 mCameraTarget=Vector3.Zero;
        Vector3 mCameraUpVector=Vector3.UnitZ ;

        public Camera()
        {
            GenerateProjection();
            GenerateView();
        }

        #region Projection
        public float NearClippingPlane
        {
            get { return mNearClip; }
            set
            {
                if (mNearClip != value)
                {
                    mNearClip = value;
                    GenerateProjection();
                }
            }
        } 
        public float FarClippingPlane
        {
            get { return mFarClip; }
            set
            {
                if (mFarClip != value)
                {
                    mFarClip = value;
                    GenerateProjection();
                }
            }
        }  
        public float FieldOfView
        {
            get { return mFOV; }
            set
            {
                if (mFOV != value)
                {
                    mFOV = value;
                    GenerateProjection();
                }
            }
        }   
        public float AspectRatio
        {
            get { return mAspectRatio; }
            set
            {
                if (mAspectRatio != value)
                {
                    mAspectRatio = value;
                    GenerateProjection();
                }
            }
        }

        public Matrix Projection
        { get { return mProjection; } }

        protected void GenerateProjection()
        {
            mProjection = Matrix.CreatePerspectiveFieldOfView(mFOV,
                    mAspectRatio, mNearClip , mFarClip );
        }
        #endregion
        #region View
        public Vector3 Position
        {
            get { return mCameraPosition; }
            set
            {
                if (mCameraPosition != value)
                {
                    mCameraPosition = value;
                    GenerateView();
                }
            }
        }

        public Vector3 Target
        {
            get { return mCameraTarget; }
            set
            {
                if (mCameraTarget != value)
                {
                    mCameraTarget = value;
                    GenerateView();
                }
            }
        }

        public Vector3 UpVector
        {
            get { return mCameraUpVector; }
            set
            {
                if (mCameraUpVector != value)
                {
                    mCameraUpVector = value;
                    GenerateView();
                }
            }
        }

        public Matrix View
        { get { return mView; } }

        protected void GenerateView()
        {
            mView = Matrix.CreateLookAt(mCameraPosition, mCameraTarget, mCameraUpVector);
        }
        #endregion


    }
}
