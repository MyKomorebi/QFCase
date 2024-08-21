using UnityEngine;
using QFramework;
using System;
using static UnityEngine.EventSystems.EventTrigger;


namespace ProjectSurvivor
{
	public partial class Enemy : ViewController,IEnemy
	{
		//Ѫ��
		public float HP = 3;
		//�ƶ��ٶ�
		public float MovementSpeed = 2f;
		void Start()
		{
            //����������һ
			EnemyGenerator.EnemyCount.Value++;
		}

        private void OnDestroy()
        {
            //����������һ
            EnemyGenerator.EnemyCount.Value--;
        }

        private void FixedUpdate()
        {
            //�����Ҵ���
            if (Player.Default)
            {
                //�õ�ָ����ҵķ���
                var direction = (Player.Default.transform.position - transform.position).normalized;
                //������ƶ�
                SelfRigidbody2D.velocity = direction * MovementSpeed;
            }
            else
            {
                SelfRigidbody2D.velocity=Vector2.zero;
            }
        }
        void Update()
		{
			
            //�����������С��0
			if (HP <= 0)
			{
                //�����Լ�
				this.DestroyGameObjGracefully();

				//��������ֵ�ĵ���

                Global.GeneratePowerUp(this.gameObject);
			}
		
		}

        private bool IgnoreHurt=false;

        public void Hurt(float value,bool force=false)
        {
            if (IgnoreHurt&&!force) return;
            //�����˺�����
            FloatingTextController.Play(transform.position,value.ToString());
            //��ʾ��ɫ
            Sprite.color = Color.red;
            //�������˶���
            AudioKit.PlaySound("Hit");
            //�ӳ�0.2��
            ActionKit.Delay(0.2f, () =>
            {
                //Ѫ������
                this.HP -= value;
                //��ʾ��ɫ
                this.Sprite.color = Color.white;

                IgnoreHurt = false;

            }).Start(this);
        }

        public void SetHPScale(float hPScale)
        {
            HP *= hPScale;
        }

        public void SetSpeedScale(float speedScale)
        {
            MovementSpeed *= speedScale;
        }
    }
}
