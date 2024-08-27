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
                //�����ռ����о�����Ч
                AudioKit.PlaySound("TreasuerChest");
                //�����Լ�
                this.DestroyGameObjGracefully();
            }
        }
    }
}
