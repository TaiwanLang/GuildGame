using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

	public GamePanelType gamePanelType;
	public GamePanel[] gamePanels;

	private GamePanelInfo gamePanelInfo;

	public void Play()
	{
		NicePlay.Utilities.DebugLog("Play = " + gamePanelType);
		gamePanelInfo = new GamePanelInfo(gamePanelType, gamePanels);
		GamePanelManager.Instance.Play(gamePanelInfo);
	}
}
