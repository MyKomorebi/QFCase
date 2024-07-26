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
				EnemyCountText.text = "����:" + enemyCount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			Global.CurrentSeconds.RegisterWithInitValue(currentsSeconds =>
			{
				if (Time.frameCount % 30 == 0)
				{
                    var currentsSecondsInt = Mathf.FloorToInt(currentsSeconds);

                    var seconds = currentsSecondsInt % 60;

                    var minutes = currentsSecondsInt / 60;

                    TimeText.text = "ʱ��:" + $"{minutes:00}:{seconds:00}";
                }
				

            });
			//��ʼ������ֵ�ı�ʱ��ִ���¼�
			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpText.text = "����ֵ��(" + exp+"/"+Global.ExpToNext()+")";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
            //��ʼ������ֵ�ı�ʱ��ִ���¼�
            Global.Level.RegisterWithInitValue(lv =>
            {
                LevelText.text = "�ȼ���" +lv ;


            }).UnRegisterWhenGameObjectDestroyed(gameObject);
			//��ֵ�ı�ʱִ���¼�
			Global.Level.Register(lv =>
			{

                Time.timeScale = 0f;

				UpgradeRoot.Show();
				AudioKit.PlaySound("LevelUp");


            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            //��ʼ������ֵ�ı�ʱ��ִ���¼�
            Global.Exp.RegisterWithInitValue(exp =>
            {
				if (exp >= Global.ExpToNext())
				{
					Global.Exp.Value -= Global.ExpToNext();

					Global.Level.Value++;
				}
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
			//���ذ�ť
			UpgradeRoot.Hide();
            //��ӵ���¼�
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
				

				CoinText.text = "���:" + coin;
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
