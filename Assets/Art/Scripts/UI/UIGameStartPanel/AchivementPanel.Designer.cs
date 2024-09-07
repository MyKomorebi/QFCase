/****************************************************************************
 * 2024.9 枕头
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class AchivementPanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public UnityEngine.UI.Button AchivementItemTemplate;
		[SerializeField] public RectTransform AchivementIemRoot;

		public void Clear()
		{
			BtnClose = null;
			AchivementItemTemplate = null;
			AchivementIemRoot = null;
		}

		public override string ComponentName
		{
			get { return "AchivementPanel";}
		}
	}
}
