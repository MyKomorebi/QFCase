using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GameUIControl : ViewController
	{
		void Start()
		{
			// Code Here
			//打开游戏面板
			UIKit.OpenPanel<UIGamePanel>();
			
		}

        private void OnDestroy()
        {
			//结束游戏面板
            UIKit.ClosePanel<UIGamePanel>();
        }
    }
}
