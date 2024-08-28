using UnityEngine;
using QFramework;
using QAssetBundle;
namespace ProjectSurvivor
{
	public partial class Ball : ViewController
	{

	
		void Start()
		{
			SelfRigidody2D.velocity =
				new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f) *
				Random.Range(Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2));
			Global. SuperBasketBall.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					this.LocalScale(3);
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			HurtBox.OnTriggerEnter2DEvent(collider =>
			{
				var hurtBox=collider.GetComponent<HurtBox>();
				if (hurtBox)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						var enemy=hurtBox.Owner.GetComponent<IEnemy>();
						var damageTimes=Global.SuperBasketBall.Value?Random.Range(2,3+1):1;
						DamageSystem.CalculateDemage(Global.BasketBallDamage.Value*damageTimes, enemy);
						
						
                        if (Random.Range(0, 1.0f) < 0.5f&&collider&&collider.attachedRigidbody&&Player.Default)
                        {
                            collider.attachedRigidbody.velocity =
                            collider.NormalizedDirection2DFrom(this) * 5 +
                            collider.NormalizedDirection2DFrom(Player.Default) * 10;
                        }
                    }
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var normal=collision.GetContact(0).normal;

			if(normal.x>normal.y)
			{
				SelfRigidody2D.velocity = new Vector2(SelfRigidody2D.velocity.x,
					Mathf.Sign(SelfRigidody2D.velocity.y) * Random.Range(0.5f, 1.5f) *
					Random.Range(Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2));
				SelfRigidody2D.angularVelocity = Random.Range(-360, 360);


			}
			else
			{
				var rb=SelfRigidody2D;

				rb.velocity =
					new Vector2(Mathf.Sign(rb.velocity.x) * Random.Range(0.5f, 1.5f) * Random.Range(
						Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2), rb.velocity.y);
				rb.angularVelocity = Random.Range(-360f, 360f);
					
			}
			AudioKit.PlaySound(Sfx.BALL);
        }
    }
}
