using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ProjectSurvivor
{
	public partial class GetAllExp : GameplayObject
	{
        protected override Collider2D Collider2D => SelfCollider2D;
        static IEnumerator FlyToPlayerStart()
        {
            IEnumerable<PowerUp> exps = FindObjectsByType<Exp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            IEnumerable<PowerUp> coins = FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            int count = 0;
            foreach (var powerUp in exps.Concat(coins)
                         .OrderByDescending(e => e.InScreen)
                         .ThenBy(e => e.Distance2D(Player.Default)))
            {
                if (powerUp.InScreen)
                {
                    if (count > 5)
                    {
                        count = 0;
                        yield return new WaitForEndOfFrame();
                    }
                }
                else
                {
                    if (count > 2)
                    {
                        count = 0;
                        yield return new WaitForEndOfFrame();
                    }
                }

                count++;
                ActionKit.OnUpdate.Register(() =>
                {
                    var player = Player.Default;
                    if (player)
                    {
                        powerUp.transform.Translate(player.NormalizedDirection2DFrom(powerUp) * Time.deltaTime * 5f);
                    }
                }).UnRegisterWhenGameObjectDestroyed(powerUp);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                PowerUpManager.Default.StartCoroutine(FlyToPlayerStart());

                AudioKit.PlaySound("GetAllExp");

                this.DestroyGameObjGracefully();
            }
        }
    }
}
