using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
	public class UIGameStartPanelData : UIPanelData
	{
	}
	public partial class UIGameStartPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
			// please add init code here
			Time.timeScale = 1.0f;
			
			BtnStartGame.onClick.AddListener(() =>
			{
				Global.RestData();
				this.CloseSelf();

				SceneManager.LoadScene(1);
			});

            BtnCoinUpgrade.onClick.AddListener(() =>
            {
                CoinUpgradePanel.Show();
            });

			Global.Coin.RegisterWithInitValue(coin =>
			{
				CoinText.text = "½ð±Ò£º" + coin;
				if (coin >= 5)
				{
					BtnCoinPercenUpgrade.Show();

					BtnExpPercentUpgrade.Show();
				}
				else
				{
                    BtnCoinPercenUpgrade.Hide();

                    BtnExpPercentUpgrade.Hide();
                }

			});

            BtnCoinPercenUpgrade.onClick.AddListener(() =>
			{
				Global.CoinPercent.Value += 0.1f;
				Global.Coin.Value -= 5;
			});

			BtnExpPercentUpgrade.onClick.AddListener(() =>
			{
				Global.Expercent.Value += 0.1f;
				Global.Coin.Value -= 5;
			});
			BtnClose.onClick.AddListener(() =>
			{
				CoinUpgradePanel.Hide();
			});
			
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
