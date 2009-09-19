using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _3dplayground
{

    public enum CameraMode
    {
        Attached,
        detached

    }

    /// <summary>
    /// This class respresents the camera will will define the view into the object space and the methods available to the camera.
    /// Provides access to a frustrum for visibility detection.    
    /// </summary>
   public  class Camera
    {   
       //ToDo: make camera so it can be attached to an object.
        float mAspectRatio = 1;
        float mFOV = MathHelper.ToRadians(180-1) ;
        float mNearClip = 1;
        float mFarClip = 100f;

        Matrix mProjection;
        Matrix mView;
        BoundingFrustum mFrustrum;

        // these values are used when the camera is attached 
        // - when the camera is detached it should pull this data from the object it was attacvhed to
        Vector3 mCameraPosition = Vector3.Zero;
        Vector3 mCameraTarget=Vector3.Zero;
        Vector3 mCameraUpVector=Vector3.UnitZ ;        

        IAmInSpace mAttachedTo;
        CameraMode mCameraMode = CameraMode.detached;

        public Camera()
        {
            GenerateProjection();
            GenerateView();
            GenerateFrustrum();
        }

       /// <summary>
       /// Gets / Sets wether the camera is attached to an object.
       /// </summary>
        public CameraMode CameraMode
        {
            get { return mCameraMode; }
            set { mCameraMode = value; }
        }

        public void AttachToObject(IAmInSpace theTarget)
        {
            mAttachedTo = theTarget;
        }   


       /// <summary>
       /// Will compile the matricies for drawing. To be used if the object it is attached 
       /// to moves or the cameras position itself changes.
       /// </summary>
        public void Compile()
        {
            GenerateProjection();
            GenerateView();
            GenerateFrustrum();
        }



        #region Projection
        /// <summary>
       /// Gets or setrs the near clipping plane of the frustrum and projection matricies
       /// </summary>
        public float NearClippingPlane
        {
            get { return mNearClip; }
            set
            {
                if (mNearClip != value)
                {
                    mNearClip = value;                   
                }
            }
        } 
       /// <summary>
        /// Gets / Sets the Far clipping plane of the Frustrum  and projection matricies
       /// </summary>
        public float FarClippingPlane
        {
            get { return mFarClip; }
            set
            {
                if (mFarClip != value)
                {
                    mFarClip = value;
                }
            }
        }
       /// <summary>
       /// Gets or sets the Field of View to be used in the Projection and Frustrum
       /// </summary>
        public float FieldOfView
        {
            get { return mFOV; }
            set
            {
                if (mFOV != value)
                {
                    mFOV = value;
                }
            }
        }  
       /// <summary>
       /// gets or sets the aspect ration the projection will use.
       /// </summary>
        public float AspectRatio
        {
            get { return mAspectRatio; }
            set
            {
                if (mAspectRatio != value)
                {
                    mAspectRatio = value;
                }
            }
        }
        /// <summary>
        /// Gets the Projection Matrix that was last compiled.
        /// </summary>
        public Matrix Projection
        { get { return mProjection; } }

       /// <summary>
       /// Creates a PerspectiveFOV Matrix from the FOV, ASpect Ratio, Near and Fasr clip
       /// </summary>
        public  void GenerateProjection()
        {
            mProjection = Matrix.CreatePerspectiveFieldOfView(mFOV,
                    mAspectRatio, mNearClip , mFarClip );
        }
        #endregion

        #region View
       /// <summary>
       /// Gets or sets the Position of the view matrix
       /// </summary>
        public Vector3 Position
        {
            get
            {
                Vector3 retVal;
                if (mCameraMode == CameraMode.detached)
                {
                    retVal = mCameraPosition;
                }
                else
                {
                    retVal = (mAttachedTo.Position.ToVector3() + mAttachedTo.CameraOffset);
                }
                return retVal;
            }
            set
            {
                if (mCameraPosition != value)
                {
                    mCameraPosition = value;
                }
            }
        }
       /// <summary>
       /// Gets or sets the position that the camera shuld face
       /// </summary>
        public Vector3 Target
        {
            get
            {
                Vector3 retVal;
                if (mCameraMode == CameraMode.detached)
                {

                    retVal = mCameraTarget;
                }
                else
                {
                    retVal = mAttachedTo.Position.ToVector3();

                }
                return retVal;
            }
            set
            {
                if (mCameraTarget != value)
                {
                    mCameraTarget = value;
                }
            }
        }
       /// <summary>
       /// Gets / Sets a vector 3 representing the Up Direction of the Camera.
       /// </summary>
        public Vector3 UpVector
        {
            get
            {
                Vector3 retVal;
                if (mCameraMode == CameraMode.Attached)
                {
                    retVal = mAttachedTo.UpVector;
                }
                else
                {
                    retVal = mCameraUpVector;
                }
                return retVal;
            }
            set
            {
                if (mCameraUpVector != value)
                {
                    mCameraUpVector = value;
                }
            }
        }

       /// <summary>
       /// Gets the last compiles View Matrix
       /// </summary>
        public Matrix View
        { get { return mView; } }

       /// <summary>
       /// Generates the View matrix fromt he camera Position, target and UpVector.
       /// </summary>
        public void GenerateView()
        {
            if (mCameraMode == CameraMode.Attached)
            {
                Vector3 camPos = mAttachedTo.CameraOffset;
                camPos = Vector3.Transform(camPos,Matrix.CreateFromQuaternion(mAttachedTo.Rotation));
                camPos += mAttachedTo.Position.ToVector3();

                Vector3 camUp = mAttachedTo.UpVector;

                mView = Matrix.CreateLookAt(camPos ,mAttachedTo.Position.ToVector3(),camUp);
                    //new Vector3(mAttachedTo.Rotation.X,mAttachedTo.Rotation.Y,mAttachedTo.Rotation.Z), 
                    //mAttachedTo.UpVector.ToVector3() );
            }
            else
            {
               
                mView = Matrix.CreateLookAt(mCameraPosition, mCameraTarget, mCameraUpVector);
            }
        }
        #endregion

        #region Frustrum

       /// <summary>
       /// Gets the Bounding frustrum for the camera view
       /// </summary>
        public BoundingFrustum BoundingFrustrum
        { get { return mFrustrum; } }

       /// <summary>
       /// Generates the rustrum fromt he View and perspective MAtricies.
       /// </summary>
        public void GenerateFrustrum()
        {
            mFrustrum = new BoundingFrustum(Matrix.Multiply(mView, mProjection));     
        }

        #endregion


    }
}
