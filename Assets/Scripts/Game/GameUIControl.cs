using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GameUIControl : ViewController
	{
		void Start()
		{
			// Code Here
			//����Ϸ���
			UIKit.OpenPanel<UIGamePanel>();
			
		}

        private void OnDestroy()
        {
			//������Ϸ���
            UIKit.ClosePanel<UIGamePanel>();
        }
    }
}
