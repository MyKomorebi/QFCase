using UnityEngine;
using QFramework;
using System;
using static UnityEngine.EventSystems.EventTrigger;


namespace ProjectSurvivor
{
	public partial class Enemy : ViewController,IEnemy
	{
		//血量
		public float HP = 3;
		//移动速度
		public float MovementSpeed = 2f;
		void Start()
		{
            //敌人总数加一
			EnemyGenerator.EnemyCount.Value++;
		}

        private void OnDestroy()
        {
            //敌人数量减一
            EnemyGenerator.EnemyCount.Value--;
        }

        private void FixedUpdate()
        {
            //如果玩家存在
            if (Player.Default)
            {
                //得到指向玩家的方向
                var direction = (Player.Default.transform.position - transform.position).normalized;
                //向玩家移动
                SelfRigidbody2D.velocity = direction * MovementSpeed;
            }
            else
            {
                SelfRigidbody2D.velocity=Vector2.zero;
            }
        }
        void Update()
		{
			
            //如果敌人数量小于0
			if (HP <= 0)
			{
                //销毁自己
				this.DestroyGameObjGracefully();

				//触发经验值的掉落

                Global.GeneratePowerUp(this.gameObject);
			}
		
		}

        private bool IgnoreHurt=false;

        public void Hurt(float value,bool force=false)
        {
            if (IgnoreHurt&&!force) return;
            //播放伤害文字
            FloatingTextController.Play(transform.position,value.ToString());
            //显示红色
            Sprite.color = Color.red;
            //播放受伤动画
            AudioKit.PlaySound("Hit");
            //延迟0.2秒
            ActionKit.Delay(0.2f, () =>
            {
                //血量减少
                this.HP -= value;
                //显示白色
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
