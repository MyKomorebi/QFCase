using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Coin : PowerUp
	{
        protected override Collider2D Collider2D => SelfCollider2D;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //如果触碰到玩家的碰撞区域
            if (collision.GetComponent<CollectableArea>())
            {
                FlyingToPlayer = true;
            } 
        }
        protected override void Execute()
        {
            //播放金币音效
            AudioKit.PlaySound("Coin");

            //金币增加
            Global.Coin.Value++;
            //销毁自己
            this.DestroyGameObjGracefully();
        }
    }
}
