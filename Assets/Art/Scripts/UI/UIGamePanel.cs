using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public class UIGamePanelData : UIPanelData
	{
	}
	public partial class UIGamePanel : UIPanel
	{

		public static EasyEvent FlashScreen = new EasyEvent();
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();
			// please add init code here

			

			EnemyGenerator.EnemyCount.RegisterWithInitValue(enemyCount =>
			{
				EnemyCountText.text = "敌人:" + enemyCount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			Global.CurrentSeconds.RegisterWithInitValue(currentsSeconds =>
			{
				if (Time.frameCount % 30 == 0)
				{
                    var currentsSecondsInt = Mathf.FloorToInt(currentsSeconds);

                    var seconds = currentsSecondsInt % 60;

                    var minutes = currentsSecondsInt / 60;

                    TimeText.text = "时间:" + $"{minutes:00}:{seconds:00}";
                }
				

            });
			//初始化，数值改变时，执行事件
			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpValue.fillAmount = exp /(float) Global.ExpToNext();
				
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
            //初始化，数值改变时，执行事件
            Global.Level.RegisterWithInitValue(lv =>
            {
                LevelText.text = "等级：" +lv ;


            }).UnRegisterWhenGameObjectDestroyed(gameObject);
			ExpUpgradePanle.Hide();
            //数值改变时执行事件
            Global.Level.Register(lv =>
			{

				Time.timeScale = 0f;

                ExpUpgradePanle.Show();
                AudioKit.PlaySound("LevelUp");

				
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			//初始化，数值改变时，执行事件
			Global.Exp.RegisterWithInitValue(exp =>
            {
				if (exp >= Global.ExpToNext())
				{
					Global.Exp.Value -= Global.ExpToNext();

					Global.Level.Value++;
				}
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
			


			var enemyGenerator=FindObjectOfType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				if( enemyGenerator.LastWave &&enemyGenerator.CurrentWave==null &&EnemyGenerator.EnemyCount.Value==0)
				{
					this.CloseSelf();
					UIKit.OpenPanel<UIGamePassPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);


		

			Global.Coin.RegisterWithInitValue(coin =>
			{
				

				CoinText.text = "金币:" + coin;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			FlashScreen.Register(() =>
			{
                ActionKit
                   .Sequence()
                   .Lerp(0, 0.5f, 0.1f,
                       alpha => ScreenColor.ColorAlpha(alpha))
                   .Lerp(0.5f, 0, 0.2f,
                       alpha => ScreenColor.ColorAlpha(alpha),
                       () => ScreenColor.ColorAlpha(0))
                   .Start(this);

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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
