using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleCircle : ViewController
    {
        void Start()
        {
            Circle.OnTriggerEnter2DEvent(collider =>
            {
                var hurtBox=collider.GetComponent<HurtBox>();

                if (hurtBox)
                {
                    if (hurtBox.Owner.CompareTag("Enemy"))
                    {
                      
                        hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            var radius = 3;

            var degree = Time.frameCount ;

            var circleLocalPos = new Vector2(-Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad)) * radius;

            Circle.LocalPosition(circleLocalPos.x, circleLocalPos.y);
        }
    }
}
