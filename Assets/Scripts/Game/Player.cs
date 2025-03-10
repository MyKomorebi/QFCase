using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Player : ViewController
    {

        //�ƶ��ٶ�
        public float movementSpeed = 5;
        //����
        public static Player Default;
        private AudioPlayer mWalkSfx;


        private void Awake()
        {
            Default = this;
        }
        void Start()
        {


            //2d�����¼�
            HurtBox.OnTriggerEnter2DEvent(Collider2D =>
            {
                //��ȡ��ײ��
                var hitBox = Collider2D.GetComponent<HitHurtBox>();
                //�����Ϊ��
                if (hitBox != null)
                {
                    //��ײ�Ķ���Ϊ����
                    if (hitBox.Owner.CompareTag("Enemy"))
                    {
                        //Ѫ����һ
                        Global.HP.Value--;
                        //���Ѫ���Ѿ�С��0
                        if (Global.HP.Value <= 0)
                        {
                            //����������Ч
                            AudioKit.PlaySound("Die");
                            //�����Լ�
                            this.DestroyGameObjGracefully();

                            //�򿪽������
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
                        else
                        {
                            //����������Ч
                            AudioKit.PlaySound("Hurt");
                        }

                    }
                }


            }).UnRegisterWhenGameObjectDestroyed(gameObject);//���ٶ���ʱע���¼�


            void UpdateHP()
            {
                HPValue.fillAmount = Global.HP.Value / (float)Global.MaxHP.Value;
            }
            Global.HP.RegisterWithInitValue(hp =>
            {
                UpdateHP();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.MaxHP.RegisterWithInitValue(maxHP =>
            {
                UpdateHP();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
        private bool mFaceRight = true;
        void Update()
        {
            //��ȡˮƽ����
            var horizontal = Input.GetAxisRaw("Horizontal");
            //��ȡ��ֱ����
            var vertical = Input.GetAxisRaw("Vertical");
            //��һ������
            var targetVelocity = new Vector2(horizontal, vertical).normalized *
                (movementSpeed * Global.MovementSpeedRate.Value);
            if (horizontal == 0 && vertical == 0)
            {
                if (mFaceRight)
                {
                    Sprite.Play("PlayerIdleRight");
                }
                else
                {
                    Sprite.Play("PlayerIdleLeft");
                }

                if (mWalkSfx != null)
                {
                    mWalkSfx.Stop();
                    mWalkSfx = null;
                }
            }
            else
            {
                if (mWalkSfx == null)
                {
                    mWalkSfx = AudioKit.PlaySound(Sfx.WALK, true);
                }

                if (horizontal > 0)
                {
                    mFaceRight = true;
                }
                else if (horizontal < 0)
                {
                    mFaceRight = false;
                }

                if (mFaceRight)
                {
                    Sprite.Play("PlayerWalkRight");
                }
                else
                {
                    Sprite.Play("PlayerWalkLeft");
                }
            }
            //Ϊ���������ٶ�
            SelfRigidbody2D.velocity = Vector2.Lerp(SelfRigidbody2D.velocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime * 5));
        }

        private void OnDestroy()
        {
            Default = null;
        }
    }
}
