using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

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

			//ȫ�ֵ�Update��ע���¼�
			ActionKit.OnUpdate.Register(() =>
			{
				//���¿ո�
                if (Input.GetKeyDown(KeyCode.Space))
                {
					//�ر��Լ�
					this.CloseSelf();

                    Global.RestData();

                    //���¼����Լ�
                    SceneManager.LoadScene("SampleScene");
                  
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//�����Լ�ʱע���¼�

			BtnBackToStart.onClick.AddListener(() =>
			{
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
