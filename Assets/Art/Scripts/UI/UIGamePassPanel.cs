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
