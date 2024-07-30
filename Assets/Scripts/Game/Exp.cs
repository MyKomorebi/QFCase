using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Exp : ViewController
	{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //如果进入碰撞范围
            if (collision.GetComponent<CollectableArea>())
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
}
