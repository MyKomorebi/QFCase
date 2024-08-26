using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
	public partial class AbilityController : ViewController,IController
	{
        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        void Start()
        {
            // Code Here

            Global.SimpleSwordUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                {
                    SimpleSword.Show();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.SimpleKnifeUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                {
                    Simpleknife.Show();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.RotateSwordUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                {
                    Rotateword.Show();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.BasketBallUnlocked.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                {
                    BasketBallAbility.Show();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);


            // 随机升级一个
            this.GetSystem<ExpUpgradeSystem>().Items.Where(item=>item.IsWeapon)
               .ToList()
               .GetRandomItem().Upgrade();

            //Global.SuperBomb.RegisterWithInitValue(unlocked =>
            //{
            //    if (unlocked)
            //    {
            //        SuperBomb.Show();
            //    }
            //}).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
