using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
    public partial class SimpleSword : ViewController
    {
        //��ǰ��
        private float currentSeconds = 0;
       
       
        private void Update()
        {
            currentSeconds += Time.deltaTime;
            //���������ʱ��

            if (currentSeconds >= Global.SimpleAbillityDuration.Value)
            {
                //��ǰʱ������
                currentSeconds = 0;
                var countTimes=Global.SuperSword.Value?2:1;
                var damageTimes= Global.SuperSword.Value?Random.Range(2,3+1):1;
                var distanceTimes= Global.SuperSword.Value?2:1;

                //��ȡ���еĵ���
                var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                //������ȡ�ĵ���
                foreach (var enemy in enemies
                    .OrderBy(e=>e.Direction2DFrom(Player.Default).magnitude)
                    .Where(e=>e.Direction2DFrom(Player.Default).magnitude<Global.SimpleSwordRange.Value*distanceTimes)
                    .Take((Global.SimpleSwordCount.Value + Global.AdditionalFlyThingCount.Value)*countTimes))
                {
                   
                        //��������
                        //enemy.Hurt(Global.SimpleAbillityDamage.Value);
                        Sword.Instantiate().Position(enemy.Position() + Vector3.left * 0.25f)
                            .Show()
                            .Self(self =>
                            {
                                var selfcache = self;
                                selfcache.OnTriggerEnter2DEvent(collider2D =>
                                {
                                    var hurtBox = collider2D.GetComponent<HitHurtBox>();
                                    if (hurtBox)
                                    {
                                        if (hurtBox.Owner.CompareTag("Enemy"))
                                        {
                                            DamageSystem.CalculateDemage(Global.SimpleAbillityDamage.Value*damageTimes,
                                                hurtBox.Owner.GetComponent<Enemy>());
                                            
                                        }
                                    }
                                }).UnRegisterWhenGameObjectDestroyed(gameObject);
                                ActionKit.Sequence()
                                .Callback(() => { selfcache.enabled = false; })
                                .Parallel(p =>
                                {
                                    p.Lerp(0, 10, 0.2f, (z) =>
                                    {
                                     selfcache .transform .LocalEulerAnglesZ(z);
                                    });
                                    p.Append(ActionKit.Sequence()
                                        .Lerp(0, 1.25f, 0.1f, scale => selfcache.LocalScale(scale))
                                         .Lerp(1.25f, 1f, 0.1f, scale => selfcache.LocalScale(scale))
                                        );
                                })
                                .Callback(() => { selfcache.enabled=true; })
                                .Parallel(p =>
                                {
                                    p.Lerp(10, -180, 0.2f, z =>
                                    {
                                        selfcache.transform.LocalEulerAnglesZ(z);
                                    });
                                    p.Append(ActionKit.Sequence()
                                        .Lerp(1, 1.25f, 0.1f, scale => selfcache.LocalScale(scale))
                                        .Lerp(1.25f, 1, 0.1f, scale => selfcache.LocalScale(scale)));
                                })
                                .Callback(() => { selfcache .enabled=false; })
                                .Lerp(-180, 0, 0.3f, z =>
                                {
                                    selfcache.LocalEulerAnglesZ(z)
                                   .LocalScale(z.Abs()/180);
                                })
                                .Start(this, () =>
                                {
                                    selfcache.DestroyGameObjGracefully();
                                });
                            });

                      
                    
                }
              
            }
        }
    }
}
