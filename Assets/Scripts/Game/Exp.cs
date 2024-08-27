using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Exp : GameplayObject
	{
        protected override Collider2D Collider2D =>SelfCollider2D;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //���������ײ��Χ
            if (collision.GetComponent<CollectableArea>())
            {
                //���ž�����Ч
                AudioKit.PlaySound("Exp");
                //�����һ
                Global.Exp.Value++;
                //�����Լ�
                this.DestroyGameObjGracefully();
            }
           
        }
    }
}
