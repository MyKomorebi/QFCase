/****************************************************************************
 * 2024.9 枕头
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class AchivementController
	{
		[SerializeField] public UnityEngine.UI.Image AchivemntItem;
		[SerializeField] public UnityEngine.UI.Text Desription;
		[SerializeField] public UnityEngine.UI.Text Title;
		[SerializeField] public UnityEngine.UI.Image Icon;

		public void Clear()
		{
			AchivemntItem = null;
			Desription = null;
			Title = null;
			Icon = null;
		}

		public override string ComponentName
		{
			get { return "AchivementController";}
		}
	}
}
