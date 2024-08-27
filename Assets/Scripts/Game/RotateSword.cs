using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace ProjectSurvivor
{
    public partial class RotateSword : ViewController
    {
        List <Collider2D>mSwords=new List <Collider2D> ();
        void Start()
        {
          
            Sword.Hide();
            void CreateSword()
            {
                mSwords.Add(Sword.InstantiateWithParent(this)
                       .Self(self =>
                       {
                           self.OnTriggerEnter2DEvent(collider =>
                           {
                               var hurtBox = collider.GetComponent<HurtBox>();

                               if (hurtBox)
                               {
                                   if (hurtBox.Owner.CompareTag("Enemy"))
                                   {
                                       DamageSystem.CalculateDemage(Global.RotateSwordDamage.Value,
                                           hurtBox.Owner.GetComponent<Enemy>());

                                       if (Random.Range(0, 1.0f) < 0.5f)
                                       {
                                           collider.attachedRigidbody.velocity =
                                           collider.NormalizedDirection2DFrom(self) * 5 +
                                           collider.NormalizedDirection2DFrom(Player.Default) * 10;
                                       }
                                   }
                               }
                           }).UnRegisterWhenGameObjectDestroyed(self);
                       })
                  .Show());
            }
            void CreatesSwords()
            {
                var toAddCount = Global.RotateSwordCount.Value + Global.AdditionalFlyThingCount.Value - mSwords.Count;

                for (var i = 0; i < toAddCount; i++)
                {
                    CreateSword();

                }
                UpdateCirclePos();
            }
            Global.RotateSwordCount.Or(Global.AdditionalFlyThingCount).Register((CreatesSwords)).UnRegisterWhenGameObjectDestroyed (gameObject);
            CreatesSwords();
            Global.RotateSwordRange.Register((range) =>
            {
                UpdateCirclePos ();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
          
        }
        void UpdateCirclePos()
        {
            var radius =Global.RotateSwordRange.Value;
        
            var durationDegress=360/mSwords.Count;
            for(var i=0; i<mSwords.Count; i++)
            {

                var circleLocalPos = new Vector2(Mathf.Cos(durationDegress*i * Mathf.Deg2Rad), Mathf.Sin(durationDegress * i * Mathf.Deg2Rad)) * radius;
                mSwords[i].LocalPosition(circleLocalPos.x, circleLocalPos.y)
               .LocalEulerAnglesZ(durationDegress * i - 90);
            }

           
        }

        private void Update()
        {
            

            var degree = Time.frameCount*Global.RotateSwordSpeed.Value ;
            this.LocalEulerAnglesZ(-degree);

           
        }
    }
}
