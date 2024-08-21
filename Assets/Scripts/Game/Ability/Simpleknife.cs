using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
    public partial class Simpleknife : ViewController
    {
        
        private float mCurrentSeconds = 0;
        void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= 1.0f)
            {
                mCurrentSeconds = 0.0f;
                if (Player.Default)
                {
                    //获取所有的敌人
                    var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                    //遍历获取的敌人
                    var enemy = enemies.OrderBy(enemy => (Player.Default.transform.position - enemy.transform.position).magnitude).FirstOrDefault();

                    if (enemy)
                    {
                        Knife.Instantiate()
                            .Position(this.Position())
                            .Show()
                            .Self(self =>
                            {
                                var direction = (enemy.Position() - Player.Default.Position()).normalized;
                                var rigidbody2D = self.GetComponent<Rigidbody2D>();
                                rigidbody2D.velocity = (direction * 10);

                                self.OnTriggerEnter2DEvent(collider =>
                                {
                                    var hurtBox = collider.GetComponent<HurtBox>();

                                    if (hurtBox)
                                    {
                                        if (hurtBox.Owner.CompareTag("Enemy"))
                                        {

                                            hurtBox.Owner.GetComponent<Enemy>().Hurt(5);
                                            self.DestroyGameObjGracefully();
                                        }
                                    }
                                }).UnRegisterWhenGameObjectDestroyed(self);


                                ActionKit.OnUpdate.Register(() =>
                                {
                                    if (Player.Default)
                                    {
                                        if ((Player.Default.Position() - self.Position()).magnitude > 20)
                                        {
                                            self.DestroyGameObjGracefully();
                                        }
                                    }
                                }).UnRegisterWhenGameObjectDestroyed(self);
                            });
                       
                    }
                }
               
               


            }
        }
    }
}
