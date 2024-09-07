using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:56215467-40c1-4ebd-a297-38e6c01963f7
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnAchivement;
		[SerializeField]
		public UnityEngine.UI.Button BtnStartGame;
		[SerializeField]
		public CoinUpgradePanel CoinUpgradePanel;
		[SerializeField]
		public AchivementPanel AchivementPanel;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			BtnAchivement = null;
			BtnStartGame = null;
			CoinUpgradePanel = null;
			AchivementPanel = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
