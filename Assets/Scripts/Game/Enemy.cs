using UnityEngine;
using QFramework;
using System;
using static UnityEngine.EventSystems.EventTrigger;


namespace ProjectSurvivor
{
	public partial class Enemy : ViewController
	{
		//Ѫ��
		public float hp = 3;
		//�ƶ��ٶ�
		public float movementSpeed = 2f;
		void Start()
		{
			EnemyGenerator.EnemyCount.Value++;
		}

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
        void Update()
		{
			if (Player.Default)
			{
                //�õ�ָ����ҵķ���
                var direction = (Player.Default.transform.position - transform.position).normalized;
                //������ƶ�
                transform.Translate(direction * movementSpeed * Time.deltaTime);
            }

			if (hp <= 0)
			{
				this.DestroyGameObjGracefully();

				//��������ֵ�ĵ���

                Global.GeneratePowerUp(this.gameObject);
			}
		
		}

        private bool IgnoreHurt=false;

        internal void Hurt(float value)
        {
            if (IgnoreHurt) return;

            Sprite.color = Color.red;

            AudioKit.PlaySound("Hit");

            ActionKit.Delay(0.2f, () =>
            {
                this.hp -= Global.SimpleAbillityDamage.Value;

                this.Sprite.color = Color.white;

                IgnoreHurt = false;

            }).Start(this);
        }
    }
}
