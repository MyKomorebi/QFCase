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

				var hitBox = Collider2D.GetComponent<HitBox>();

				if(hitBox != null)
				{
					if (hitBox.Owner.CompareTag("Enemy"))
					{
						Global.HP.Value--;
						if(Global.HP.Value<=0)
						{
                            AudioKit.PlaySound("Die");
                            //销毁自己
                            this.DestroyGameObjGracefully();
                            
                            //打开结束面板
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
						else
						{
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
			//为刚体添加事件
			SelfRigidbody2D.velocity =Vector2.Lerp(SelfRigidbody2D.velocity,targetVelocity,1-Mathf.Exp(-Time.deltaTime*5));
		}

        private void OnDestroy()
        {
			Default = null;
        }
    }
}
