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
                        //�����Ҵ���
                        if (player)
                        {
                            //��÷���
                            var direction = player.Position() - exp.Position();
                            //�����ƶ������
                            exp.transform.Translate(direction.normalized * Time.deltaTime * 5f);
                        }
                    }).UnRegisterWhenGameObjectDestroyed(exp);
                }
                foreach (var coin in FindObjectsByType<Coin>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    ActionKit.OnUpdate.Register(() =>
                    {
                        var player = Player.Default;
                        //�����Ҵ���
                        if (player)
                        {
                            //��÷���
                            var direction = player.Position() - coin.Position();
                            //�����ƶ������
                            coin.transform.Translate(direction.normalized * Time.deltaTime * 5f);
                        }
                    }).UnRegisterWhenGameObjectDestroyed(coin);
                }
                //�����ռ����о�����Ч
                AudioKit.PlaySound("GetAllExp");
                //�����Լ�
                this.DestroyGameObjGracefully();
            }
        }
    }
}
