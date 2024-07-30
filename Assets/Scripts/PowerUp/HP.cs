using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HP : ViewController
	{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                if(Global.HP.Value==Global.MaxHP.Value)
                {

                }
                else
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
    }
}
