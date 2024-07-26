using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HP : ViewController
	{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                if(Global.HP.Value==Global.MaxHP.Value)
                {

                }
                else
                {
                    //AudioKit.PlaySound("HP");

                    Global.HP.Value++;

                    this.DestroyGameObjGracefully();
                }
               
            }
        }
    }
}
