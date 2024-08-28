using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Bomb : GameplayObject
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        public static void  Execute()
        {
            //循环得到全局的敌人，销毁敌人
            foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {

                var enemy = enemyObj.GetComponent<Enemy>();

                if (enemy && enemy.gameObject.activeSelf)
                {
                    DamageSystem.CalculateDemage(Global.BombDamage.Value,enemy);
                 
                }



            }
            //播放爆炸音效
            AudioKit.PlaySound("Bomb");
            UIGamePanel.FlashScreen.Trigger();
            //摄像机晃动
            CameraController.Shake();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                Execute();
                //销毁自己
                this.DestroyGameObjGracefully();
            }
        }
    }
}
