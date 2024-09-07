using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Exp : PowerUp
	{
        protected override Collider2D Collider2D =>SelfCollider2D;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //如果进入碰撞范围
            if (collision.GetComponent<CollectableArea>())
            {
                FlyingToPlayer=true;
            }
           
        }

        protected override void Execute()
        {
            //播放经验音效
            AudioKit.PlaySound("Exp");
            //经验加一
            Global.Exp.Value++;
            //销毁自己
            this.DestroyGameObjGracefully();
        }
    }
}
