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
                //循环得到全局的敌人，销毁敌人
                foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
                {

                    var enemy=enemyObj.GetComponent<Enemy>();

                    if (enemy&&enemy.gameObject.activeSelf)
                    {
                        enemy.Hurt(enemy.HP);
                    }
                    


                }
                //播放爆炸音效
                AudioKit.PlaySound("Bomb");
                //摄像机晃动
                CameraController.Shake();
                //销毁自己
                this.DestroyGameObjGracefully();
            }
        }
    }
}
