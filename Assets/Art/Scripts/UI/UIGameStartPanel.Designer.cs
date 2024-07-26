using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:e00f5d5b-a4a1-49b2-bb10-39d9ad900137
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public RectTransform CoinUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinPercenUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnExpPercentUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public UnityEngine.UI.Button BtnPlayerMaxHpUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnStartGame;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			CoinUpgradePanel = null;
			BtnCoinPercenUpgrade = null;
			BtnExpPercentUpgrade = null;
			BtnClose = null;
			CoinText = null;
			BtnPlayerMaxHpUpgrade = null;
			BtnStartGame = null;
			
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
