using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
	public class UIGamePassPanelData : UIPanelData
	{
	}
	public partial class UIGamePassPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePassPanelData ?? new UIGamePassPanelData();
            // please add init code here

            //全局的Update，注册事件
            ActionKit.OnUpdate.Register(() =>
            {
                //按下空格
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //关闭自己
                    this.CloseSelf();

					Global.RestData();
                    //重新加载自己
                    SceneManager.LoadScene(1);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//销毁自己时注销事件

            BtnBackToStart.onClick.AddListener(() =>
            {
                this.CloseSelf();

                SceneManager.LoadScene(0);
            });

			AudioKit.PlaySound("GamePass");
        }
		
		protected override void OnOpen(IUIData uiData = null)
		{

			Time.timeScale = 0;
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
			Time.timeScale = 1;
		}
	}
}
