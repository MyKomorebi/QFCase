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
                        //销毁自己
                        this.DestroyGameObjGracefully();
                        //真机模式下，必须初始化
                        ResKit.Init();
                        //打开结束面板
                        UIKit.OpenPanel<UIGameOverPanel>();
                    }
				}
				

			}).UnRegisterWhenGameObjectDestroyed(gameObject);//销毁对象时注销事件

			
		}

		void Update()
		{
			//获取水平输入
			var horizontal = Input.GetAxis("Horizontal");
			//获取垂直输入
			var vertical = Input.GetAxis("Vertical");
			//归一化向量
			var direction = new Vector2(horizontal, vertical).normalized;
			//为刚体添加事件
			SelfRigidbody2D.velocity = direction * movementSpeed;
		}

        private void OnDestroy()
        {
			Default = null;
        }
    }
}
