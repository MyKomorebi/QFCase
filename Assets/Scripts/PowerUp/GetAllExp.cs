using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GetAllExp : ViewController
	{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                foreach(var exp in FindObjectsByType<Exp>(FindObjectsInactive.Include,FindObjectsSortMode.None))
                {
                    ActionKit.OnUpdate.Register(() =>
                    {
                        var player = Player.Default;

                        if (player)
                        {
                            var direction = player.Position() - exp.Position();

                            exp.transform.Translate(direction.normalized * Time.deltaTime * 5f);
                        }
                    }).UnRegisterWhenGameObjectDestroyed(exp);
                }
                this.DestroyGameObjGracefully();
            }
        }
    }
}
