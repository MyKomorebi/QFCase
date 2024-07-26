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
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();
			// please add init code here

			Global.HP.RegisterWithInitValue(hp=>
			{
				HPText.text = "HP:" + hp + "/" + Global.MaxHP.Value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.MaxHP.RegisterWithInitValue(maxHp =>
			{
                HPText.text = "HP:" + Global.HP.Value + "/" + maxHp;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

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
				ExpText.text = "经验值：(" + exp+"/"+Global.ExpToNext()+")";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
            //初始化，数值改变时，执行事件
            Global.Level.RegisterWithInitValue(lv =>
            {
                LevelText.text = "等级：" +lv ;


            }).UnRegisterWhenGameObjectDestroyed(gameObject);
			//数值改变时执行事件
			Global.Level.Register(lv =>
			{

                Time.timeScale = 0f;

				UpgradeRoot.Show();
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
			//隐藏按钮
			UpgradeRoot.Hide();
            //添加点击事件
            BtnUpgrade.onClick.AddListener(() =>
			{
				Time.timeScale = 1f;

				Global.SimpleAbillityDamage.Value *= 1.5f;

                UpgradeRoot.Hide();
            });

			BtnSimpleDurationUpgrade.onClick.AddListener(() =>
			{
                Time.timeScale = 1f;

                Global.SimpleAbillityDamage.Value *= 0.8f;

				UpgradeRoot.Hide();
            });


			var enemyGenerator=FindObjectOfType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				if( enemyGenerator.LastWave &&enemyGenerator.CurrentWave==null &&EnemyGenerator.EnemyCount.Value==0)
				{
					UIKit.OpenPanel<UIGamePassPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);


		

			Global.Coin.RegisterWithInitValue(coin =>
			{
				

				CoinText.text = "金币:" + coin;
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
