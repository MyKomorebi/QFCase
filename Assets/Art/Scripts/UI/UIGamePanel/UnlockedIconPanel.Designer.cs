/****************************************************************************
 * 2024.9 枕头
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class UnlockedIconPanel
	{
		[SerializeField] public UnityEngine.UI.Image UnlocaedIconTemplate;
		[SerializeField] public RectTransform UnlockedIconRoot;

		public void Clear()
		{
			UnlocaedIconTemplate = null;
			UnlockedIconRoot = null;
		}

		public override string ComponentName
		{
			get { return "UnlockedIconPanel";}
		}
	}
}
