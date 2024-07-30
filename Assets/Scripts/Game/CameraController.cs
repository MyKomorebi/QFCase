using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class CameraController : ViewController
    {
        //目标的位置
        private Vector2 mTargetPostion = Vector2.zero;
        //单例
        private static CameraController mDefault = null;

        private void Awake()
        {
            mDefault = this;
        }

        private void OnDestroy()
        {
            if (mDefault != null)
            {
                mDefault = null;
            }
        }
        private void Start()
        {
            //设置运行的帧数
            Application.targetFrameRate = 60;
        }

        //当前摄像机的位置
        private Vector3 mCurrentCameraPos;
        //是否摇晃
        private bool mShake = false;
        //摇晃的帧
        private int mShakeFrame = 0;

        private float mShakeA = 2f;
        /// <summary>
        /// 摇晃
        /// </summary>
        public static void Shake()
        {
            mDefault.mShake = true;
            mDefault.mShakeFrame = 30;
            mDefault.mShakeA = 0.25f;
        }
        private void Update()
        {
            //如果玩家存在
            if (Player.Default)
            {
                //获取当前玩家位置
                mTargetPostion = Player.Default.transform.position;
                //平滑过渡
                mCurrentCameraPos.x = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.x, mTargetPostion.x);
                mCurrentCameraPos.y = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.y, mTargetPostion.y);
                mCurrentCameraPos.z = transform.position.z;
                //如果摇晃
                if (mShake)
                {

                    var shakeA = Mathf.Lerp(mShakeA, 0.0f, (mShakeFrame / 30.0f));

                    transform.position = new Vector3(mCurrentCameraPos.x + Random.Range(-shakeA, shakeA),
                        mCurrentCameraPos.y + Random.Range(-shakeA, shakeA), mCurrentCameraPos.z);
                    mShakeFrame--;


                    if (mShakeFrame <= 0)
                    {
                        mShake = false;
                    }
                }
                else
                {
                    //摄像机的平滑过渡
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
