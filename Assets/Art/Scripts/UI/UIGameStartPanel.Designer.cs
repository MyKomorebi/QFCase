using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:0ede472c-bd0f-43fc-a416-9ac73d5c69bb
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnStartGame;
		[SerializeField]
		public CoinUpgradePanel CoinUpgradePanel;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			BtnStartGame = null;
			CoinUpgradePanel = null;
			
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
