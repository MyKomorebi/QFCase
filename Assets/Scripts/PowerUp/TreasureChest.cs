using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class TreasureChest : GameplayObject
	{
        protected override Collider2D Collider2D => SelfCollider2D;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                UIGamePanel.OpenTreasurePanel.Trigger();
                //播放收集所有经验音效
                AudioKit.PlaySound("TreasuerChest");
                //销毁自己
                this.DestroyGameObjGracefully();
            }
        }
    }
}
