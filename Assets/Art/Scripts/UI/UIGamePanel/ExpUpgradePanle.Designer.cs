/****************************************************************************
 * 2024.8 USER-20240116VV
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class ExpUpgradePanle
	{
		[SerializeField] public UnityEngine.UI.Button BtnExpUpgradeItemTemplate;
		[SerializeField] public RectTransform UpgradeRoot;

		public void Clear()
		{
			BtnExpUpgradeItemTemplate = null;
			UpgradeRoot = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanle";}
		}
	}
}
