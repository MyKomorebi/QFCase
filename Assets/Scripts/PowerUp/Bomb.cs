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
                foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
                {

                    var enemy=enemyObj.GetComponent<Enemy>();

                    if (enemy&&enemy.gameObject.activeSelf)
                    {
                        enemy.Hurt(enemy.hp);
                    }
                    


                }
                AudioKit.PlaySound("Bomb");
                CameraController.Shake();
                this.DestroyGameObjGracefully();
            }
        }
    }
}
