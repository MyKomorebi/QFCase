using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GameStartController : ViewController
	{
       

        void Start()
		{
			//打开游戏开始界面
			UIKit.OpenPanel<UIGameStartPanel>();
		}
	}
}
