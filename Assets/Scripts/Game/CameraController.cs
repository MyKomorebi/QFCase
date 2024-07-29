using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class CameraController : ViewController
    {
        private Vector2 mTargetPostion = Vector2.zero;

       private  static CameraController mDefault=null;

        private void Awake()
        {
            mDefault = this;
        }

        private void OnDestroy()
        {
            if (mDefault != null)
            {
                mDefault=null;
            }
        }
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
        

        private Vector3 mCurrentCameraPos;

        private bool mShake = false;

        private int mShakeFrame = 0;
        private float mShakeA = 2f;

        public static void Shake()
        {
            mDefault.mShake = true;
            mDefault.mShakeFrame = 30;
            mDefault.mShakeA = 0.25f;
        }
        private void Update()
        {
            if (Player.Default)
            {
                mTargetPostion = Player.Default.transform.position;
                mCurrentCameraPos.x = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.x, mTargetPostion.x);
                mCurrentCameraPos.y = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.y, mTargetPostion.y);
                mCurrentCameraPos.z = transform.position.z;
                if (mShake)
                {
                   

                   
                        var shakeA = Mathf.Lerp(mShakeA, 0.0f, (mShakeFrame / 30.0f));


                        transform.position = new Vector3(mCurrentCameraPos.x + Random.Range(-shakeA, shakeA),
                            mCurrentCameraPos.y + Random.Range(-shakeA, shakeA), mCurrentCameraPos.z);
                    mShakeFrame--;


                    if (mShakeFrame <= 0)
                    {
                        mShake=false;
                    }
                }
                else
                {
                    transform.PositionX(
                        (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.x, mTargetPostion.x));
                    transform.PositionY((1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.y, mTargetPostion.y));
                }

            }
        }
    }
}
