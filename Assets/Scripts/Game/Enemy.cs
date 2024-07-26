using UnityEngine;
using QFramework;
using System;
using static UnityEngine.EventSystems.EventTrigger;


namespace ProjectSurvivor
{
	public partial class Enemy : ViewController
	{
		//血量
		public float hp = 3;
		//移动速度
		public float movementSpeed = 2f;
		void Start()
		{
			EnemyGenerator.EnemyCount.Value++;
		}

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }

        private void FixedUpdate()
        {
            if (Player.Default)
            {
                //得到指向玩家的方向
                var direction = (Player.Default.transform.position - transform.position).normalized;
                //向玩家移动
                SelfRigidbody2D.velocity = direction * movementSpeed;
            }
            else
            {
                SelfRigidbody2D.velocity=Vector2.zero;
            }
        }
        void Update()
		{
			

			if (hp <= 0)
			{
				this.DestroyGameObjGracefully();

				//触发经验值的掉落

                Global.GeneratePowerUp(this.gameObject);
			}
		
		}

        private bool IgnoreHurt=false;

        internal void Hurt(float value,bool force=false)
        {
            if (IgnoreHurt&&!force) return;

            Sprite.color = Color.red;

            AudioKit.PlaySound("Hit");

            ActionKit.Delay(0.2f, () =>
            {
                this.hp -= value;

                this.Sprite.color = Color.white;

                IgnoreHurt = false;

            }).Start(this);
        }
    }
}
