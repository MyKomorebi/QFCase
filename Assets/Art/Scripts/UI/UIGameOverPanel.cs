using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using QAssetBundle;

namespace ProjectSurvivor
{
	public class UIGameOverPanelData : UIPanelData
	{
	}
	public partial class UIGameOverPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameOverPanelData ?? new UIGameOverPanelData();
			// please add init code here

			

			BtnBackToStart.onClick.AddListener(() =>
			{
				AudioKit.PlaySound(Sfx.BUTTONCLICK);
				this.CloseSelf();

				SceneManager.LoadScene(0);
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
