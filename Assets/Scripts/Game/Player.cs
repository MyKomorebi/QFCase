using UnityEngine;
using QFramework;


namespace ProjectSurvivor
{
	public partial class Player : ViewController
	{

		//�ƶ��ٶ�
		public float movementSpeed = 5;
		//����
		public static Player Default;



        private void Awake()
        {
            Default = this;
        }
        void Start()
		{
			

			//2d�����¼�
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
                            //�����Լ�
                            this.DestroyGameObjGracefully();
                            
                            //�򿪽������
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
						else
						{
							AudioKit.PlaySound("Hurt");
						}
						
                    }
				}
				

			}).UnRegisterWhenGameObjectDestroyed(gameObject);//���ٶ���ʱע���¼�

			
		}

		void Update()
		{
			//��ȡˮƽ����
			var horizontal = Input.GetAxis("Horizontal");
			//��ȡ��ֱ����
			var vertical = Input.GetAxis("Vertical");
			//��һ������
			var direction = new Vector2(horizontal, vertical).normalized;
			//Ϊ��������¼�
			SelfRigidbody2D.velocity = direction * movementSpeed;
		}

        private void OnDestroy()
        {
			Default = null;
        }
    }
}
