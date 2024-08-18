using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Bomb : ViewController
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                //ѭ���õ�ȫ�ֵĵ��ˣ����ٵ���
                foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
                {

                    var enemy=enemyObj.GetComponent<Enemy>();

                    if (enemy&&enemy.gameObject.activeSelf)
                    {
                        enemy.Hurt(enemy.HP);
                    }
                    


                }
                //���ű�ը��Ч
                AudioKit.PlaySound("Bomb");
                //������ζ�
                CameraController.Shake();
                //�����Լ�
                this.DestroyGameObjGracefully();
            }
        }
    }
}
