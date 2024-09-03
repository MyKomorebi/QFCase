/****************************************************************************
 * 2024.8 枕头
 ****************************************************************************/



using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;
using UnityEngine.U2D;

namespace ProjectSurvivor
{
	public partial class TreasureChestPanel : UIElement,IController
	{
        ResLoader mResLoader = ResLoader.Allocate();
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
           var matchedPadiredItems= expUpgradeSystem.Items.Where(item =>
            {
                //if (item.CurrentLevel.Value >= 7)
                if(item.CurrentLevel.Value>=1&&item.PairedName.IsNotNullAndEmpty())
                {
                    var containsInPair = expUpgradeSystem.Pairs.ContainsKey(item.Key);
                    var pairedItemKey = expUpgradeSystem.Pairs[item.Key];
                    var pairedItemStartUpgrade = expUpgradeSystem.Dictionary[pairedItemKey].CurrentLevel.Value > 0;
                    var pairedUnlocked = expUpgradeSystem.PairedProperties[item.Key].Value;
                    return containsInPair && pairedItemStartUpgrade && !pairedUnlocked;
                }
                return false;
            });
            if (matchedPadiredItems.Any())
            {
                var item=matchedPadiredItems.ToList().GetRandomItem();
                Content.text="<b>"+item.PairedName+"</b>\n"+item.PairedDescription;
                while (!item.UpgradeFinish)
                {
                    item.Upgrade();
                }
                Icon.sprite=mResLoader.LoadSync<SpriteAtlas>("Icon")
                    .GetSprite(item.PairedIconName);
               
                Icon.Show();
                expUpgradeSystem.PairedProperties[item.Key].Value = true;

            }
            else
            {
                var upgradeItems = expUpgradeSystem.Items.Where(item => item.CurrentLevel.Value > 0 && !item.UpgradeFinish).ToList();
                if (upgradeItems.Any())
                {
                    var item = upgradeItems.GetRandomItem();
                    Content.text = item.Description;
                    Icon.sprite = mResLoader.LoadSync<SpriteAtlas>("Icon")
                   .GetSprite(item.IconName);

                    Icon.Show();
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
                    Icon.Hide();

                }
            }
            
        }

        protected override void OnBeforeDestroy()
		{
            mResLoader.Recycle2Cache();
            mResLoader = null;

        }

        public IArchitecture GetArchitecture()
        {
			return Global.Interface;
        }
    }
}