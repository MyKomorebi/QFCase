/****************************************************************************
 * 2024.8 枕头
 ****************************************************************************/



using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
	public partial class TreasureChestPanel : UIElement,IController
	{
		private void Awake()
		{
			BtnSure.onClick.AddListener(() =>
			{
				
				Time.timeScale = 1.0f;
				this.Hide();
			});
		}
        private void OnEnable()
        {
            var expUpgradeSystem=this.GetSystem<ExpUpgradeSystem>();
			var upgradeItems = expUpgradeSystem.Items.Where(item => item.CurrentLevel.Value > 0 && !item.UpgradeFinish).ToList();
			if(upgradeItems.Any())
			{
				var item = upgradeItems.GetRandomItem();
				Content.text=item.Description;
				item.Upgrade();

            }
			else
			{
                if (Global.HP.Value < Global.MaxHP.Value)
                {
                    if (Random.Range(0, 1.0f) < 0.2f)
                    {
                        Content.text = "恢复 1 血量";
                        AudioKit.PlaySound("HP");
                        Global.HP.Value++;
                        
                        return;
                    }
                }

                Content.text = "增加 50 金币";
                Global.Coin.Value += 50;
               
            }
        }

        protected override void OnBeforeDestroy()
		{
		}

        public IArchitecture GetArchitecture()
        {
			return Global.Interface;
        }
    }
}