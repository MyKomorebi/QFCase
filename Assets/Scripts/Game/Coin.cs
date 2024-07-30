using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Coin : ViewController
	{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�����������ҵ���ײ����
            if (collision.GetComponent<CollectableArea>())
            {
                //���Ž����Ч
                AudioKit.PlaySound("Coin");
                //�������
                Global.Coin.Value++;
                //�����Լ�
                this.DestroyGameObjGracefully();
            } 
        }
    }
}
