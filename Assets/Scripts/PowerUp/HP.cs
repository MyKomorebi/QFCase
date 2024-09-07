using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HP : PowerUp
	{
        protected override Collider2D Collider2D => SelfCollider2D;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                if(Global.HP.Value==Global.MaxHP.Value)
                {

                }
                else
                {
                    FlyingToPlayer = true;
                }
               
            }
        }
        protected override void Execute()
        {
            //���Żظ�Ѫ��
            AudioKit.PlaySound("HP");
            //Ѫ����һ
            Global.HP.Value++;
            //�����Լ�
            this.DestroyGameObjGracefully();
        }
    }
}
