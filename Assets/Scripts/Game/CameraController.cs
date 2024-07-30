using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class CameraController : ViewController
    {
        //Ŀ���λ��
        private Vector2 mTargetPostion = Vector2.zero;
        //����
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
            //�������е�֡��
            Application.targetFrameRate = 60;
        }

        //��ǰ�������λ��
        private Vector3 mCurrentCameraPos;
        //�Ƿ�ҡ��
        private bool mShake = false;
        //ҡ�ε�֡
        private int mShakeFrame = 0;

        private float mShakeA = 2f;
        /// <summary>
        /// ҡ��
        /// </summary>
        public static void Shake()
        {
            mDefault.mShake = true;
            mDefault.mShakeFrame = 30;
            mDefault.mShakeA = 0.25f;
        }
        private void Update()
        {
            //�����Ҵ���
            if (Player.Default)
            {
                //��ȡ��ǰ���λ��
                mTargetPostion = Player.Default.transform.position;
                //ƽ������
                mCurrentCameraPos.x = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.x, mTargetPostion.x);
                mCurrentCameraPos.y = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.y, mTargetPostion.y);
                mCurrentCameraPos.z = transform.position.z;
                //���ҡ��
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
                    //�������ƽ������
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
