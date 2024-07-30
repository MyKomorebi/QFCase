using UnityEngine;
using QFramework;


namespace ProjectSurvivor
{
	public partial class Player : ViewController
	{

		//移动速度
		public float movementSpeed = 5;
		//单例
		public static Player Default;



        private void Awake()
        {
            Default = this;
        }
        void Start()
		{
			

			//2d触发事件
			HurtBox.OnTriggerEnter2DEvent(Collider2D =>
			{
				//获取碰撞盒
				var hitBox = Collider2D.GetComponent<HitBox>();
				//如果不为空
				if(hitBox != null)
				{
					//碰撞的对象为敌人
					if (hitBox.Owner.CompareTag("Enemy"))
					{
						//血量减一
						Global.HP.Value--;
						//如果血量已经小于0
						if(Global.HP.Value<=0)
						{
							//播放死亡音效
                            AudioKit.PlaySound("Die");
                            //销毁自己
                            this.DestroyGameObjGracefully();
                            
                            //打开结束面板
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
						else
						{
							//播放受伤音效
							AudioKit.PlaySound("Hurt");
						}
						
                    }
				}
				

			}).UnRegisterWhenGameObjectDestroyed(gameObject);//销毁对象时注销事件

			
		}

		void Update()
		{
			//获取水平输入
			var horizontal = Input.GetAxisRaw("Horizontal");
			//获取垂直输入
			var vertical = Input.GetAxisRaw("Vertical");
			//归一化向量
			var targetVelocity = new Vector2(horizontal, vertical).normalized*movementSpeed;
			//为刚体添加速度
			SelfRigidbody2D.velocity =Vector2.Lerp(SelfRigidbody2D.velocity,targetVelocity,1-Mathf.Exp(-Time.deltaTime*5));
		}

        private void OnDestroy()
        {
			Default = null;
        }
    }
}
