using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Exp : ViewController
	{
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
