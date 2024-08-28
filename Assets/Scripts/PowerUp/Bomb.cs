using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Bomb : GameplayObject
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        public static void  Execute()
        {
            //ѭ���õ�ȫ�ֵĵ��ˣ����ٵ���
            foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {

                var enemy = enemyObj.GetComponent<Enemy>();

                if (enemy && enemy.gameObject.activeSelf)
                {
                    DamageSystem.CalculateDemage(Global.BombDamage.Value,enemy);
                 
                }



            }
            //���ű�ը��Ч
            AudioKit.PlaySound("Bomb");
            UIGamePanel.FlashScreen.Trigger();
            //������ζ�
            CameraController.Shake();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                Execute();
                //�����Լ�
                this.DestroyGameObjGracefully();
            }
        }
    }
}
