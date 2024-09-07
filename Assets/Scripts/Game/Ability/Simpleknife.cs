using UnityEngine;
using QFramework;
using System.Linq;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Simpleknife : ViewController
    {
        
        private float mCurrentSeconds = 0;

        
        void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >=Global.SimpleAbillityDuration.Value)
            {
                mCurrentSeconds = 0.0f;
                if (Player.Default)
                {
                    var i = 0;
                    //获取所有的敌人
                    var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                    .OrderBy(enemy => Player.Default.Distance2D(enemy))
                      .Take(Global.SimpleKinfeCount.Value+Global.AdditionalFlyThingCount.Value);
                    foreach (Enemy enemy in enemies)
                    {
                        if (i < 4)
                        {
                            ActionKit.DelayFrame(11 * i, () =>
                            {
                                AudioKit.PlaySound(Sfx.KNIFE);
                            })
                               .StartGlobal();
                            i++;
                        }
                     
                        if (enemy)
                        {
                            Knife.Instantiate()
                                .Position(this.Position())
                                .Show()
                                .Self(self =>
                                {
                                   
                                    var selfCache = self;
                                    var direction = enemy.NormalizedDirection2DFrom(Player.Default);
                                    self.transform.up=direction;
                                    var rigidbody2D = self.GetComponent<Rigidbody2D>();
                                    rigidbody2D.velocity = direction * 10;

                                    var attackCount = 0;
                                    self.OnTriggerEnter2DEvent(collider =>
                                    {
                                        var hurtBox = collider.GetComponent<HitHurtBox>();

                                        if (hurtBox)
                                        {
                                            if (hurtBox.Owner.CompareTag("Enemy"))
                                            {
                                                var damageTimes=Global.SuperKnife.Value?Random.Range(2,3+1):1;
                                                DamageSystem.CalculateDemage(Global.SimpleKinfeDamage.Value*damageTimes, 
                                                    hurtBox.Owner.GetComponent<Enemy>());
                                               
                                                attackCount++;
                                                if(attackCount >= Global.SimpleKinfeAttackCount.Value)
                                                {
                                                    selfCache.DestroyGameObjGracefully();
                                                }
                                              
                                            }
                                        }
                                    }).UnRegisterWhenGameObjectDestroyed(self);


                                    ActionKit.OnUpdate.Register(() =>
                                    {
                                        if (Player.Default)
                                        {
                                            if (Player.Default.Distance2D(selfCache) > 20)
                                            {
                                                selfCache.DestroyGameObjGracefully();
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
}
