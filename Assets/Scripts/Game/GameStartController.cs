using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class GameStartController : ViewController
	{
       

        void Start()
		{
			//����Ϸ��ʼ����
			UIKit.OpenPanel<UIGameStartPanel>();
		}
	}
}
