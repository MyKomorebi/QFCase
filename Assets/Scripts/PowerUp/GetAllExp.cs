using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GetAllExp : GameplayObject
	{
        protected override Collider2D Collider2D => SelfCollider2D;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                foreach(var exp in FindObjectsByType<Exp>(FindObjectsInactive.Include,FindObjectsSortMode.None))
                {
                    ActionKit.OnUpdate.Register(() =>
                    {
                        var player = Player.Default;
                        //如果玩家存在
                        if (player)
                        {
                            //获得方向
                            var direction = player.Position() - exp.Position();
                            //经验移动向玩家
                            exp.transform.Translate(direction.normalized * Time.deltaTime * 5f);
                        }
                    }).UnRegisterWhenGameObjectDestroyed(exp);
                }
                foreach (var coin in FindObjectsByType<Coin>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    ActionKit.OnUpdate.Register(() =>
                    {
                        var player = Player.Default;
                        //如果玩家存在
                        if (player)
                        {
                            //获得方向
                            var direction = player.Position() - coin.Position();
                            //经验移动向玩家
                            coin.transform.Translate(direction.normalized * Time.deltaTime * 5f);
                        }
                    }).UnRegisterWhenGameObjectDestroyed(coin);
                }
                //播放收集所有经验音效
                AudioKit.PlaySound("GetAllExp");
                //销毁自己
                this.DestroyGameObjGracefully();
            }
        }
    }
}
