using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GamePanelType
{
	Menu,
	Prepare,
	CharacterInfo,
	EquipmentInfo,
	Rank,
	Friend,
	Tournament,
	Ready,
	FirstSelect,
	Leaderboard,
	Chellange,
	ChallengeReady,
	Shop
}

public enum PanelType
{
	Top,
	Left,
	Right,
	Chest,
	Bottom,
	Background,
	Popup,
	Count
}

public class GamePanelInfo
{
	public GamePanelType type;

	public Dictionary<PanelType, GamePanel> panels;

	public GamePanelInfo(GamePanelType _type, params GamePanel[] _panels)
	{
		type = _type;
		panels = new Dictionary<PanelType, GamePanel>();
		for(int i = 0; i < _panels.Length; i++)
		{
			panels.Add(_panels[i].type, _panels[i]);
		}
	}
}

public class GamePanelManager : MonoBehaviour {

	private const string GAME_OBJECT_NAME = "_GamePanelManager";
	private static GamePanelManager m_Instance;

	private const int POPUP_DEPTH = 50;

	private List<GamePanel> m_PopupPanelList = new List<GamePanel>();

	private PopPanel_MsgBox message_Panel;

	private GamePanelInfo m_CurrentPanel;
	private GamePanelInfo m_PreviousPanel;
	public GamePanelInfo CurrentPanel
	{
		get { return m_CurrentPanel; }
	}
	private bool m_IsTransition = false;
	public bool inTransition{get{return m_IsTransition;}}

	public static GamePanelManager Instance
	{
		get
		{
			if(m_Instance == null)
			{
				GameObject obj = new GameObject(GAME_OBJECT_NAME);
				m_Instance = obj.AddComponent<GamePanelManager>() as GamePanelManager;
				DontDestroyOnLoad( obj );
			}
			return m_Instance;
		}
	}

	public void Reset()
	{
		m_IsTransition = false;
		m_PopupPanelList.Clear();
		m_CurrentPanel = null;
		m_PreviousPanel = null;
	}

	public void Play(GamePanelInfo panel)
	{
		Debug.Log("nowpanel status" + m_IsTransition);
		if(!m_IsTransition)
		{
			SetTransition(true);
			if(m_CurrentPanel != null)
			{
				if(m_CurrentPanel.type == panel.type)
				{
					SetTransition(false);
				}
				m_PreviousPanel = m_CurrentPanel;
				
				for(int i = 0; i < (int)PanelType.Count; i++)
				{
					UnityAction tempAction = null;
					if(panel.panels.ContainsKey((PanelType)i))
					{
						switch((PanelType)i)
						{
							case PanelType.Right:
							{
								tempAction = ()=> StartPanel(panel);
								break;
							}
							case PanelType.Left:
							{
								if(!panel.panels.ContainsKey(PanelType.Right) || !m_PreviousPanel.panels.ContainsKey(PanelType.Right))
								{
									tempAction = ()=> StartPanel(panel);
								}
								break;
							}
						}

						if(m_PreviousPanel.panels.ContainsKey((PanelType)i) && m_PreviousPanel.panels[(PanelType)i] != panel.panels[(PanelType)i])
						{
							m_PreviousPanel.panels[(PanelType)i].Out(tempAction);
						}
					}
					else
					{
						switch((PanelType)i)
						{
							case PanelType.Top:
							case PanelType.Bottom:
							case PanelType.Background:
							//case PanelType.Chest:
							{
								break;
							}
							default:
							{
								if(m_PreviousPanel.panels.ContainsKey((PanelType)i))
								{
									m_PreviousPanel.panels[(PanelType)i].Out(tempAction);
								}
								break;
							}
						}
					}
				}
			}
			else
			{
				StartPanel(panel);
			}
		}
	}

	public void PlayMessage(string title, string msg, params UnityAction[] actions)
	{
		if(message_Panel == null)
		{
			GameObject obj = Instantiate(Resources.Load("Message_Panel") as GameObject);
            obj.name = "_Message_Panel";
            obj.transform.parent = GameObject.Find("UI Root (2D)").transform.Find("Camera").transform;
            obj.transform.localScale = new Vector3(1, 1, 1);
            message_Panel = obj.GetComponent<PopPanel_MsgBox>();
		}

		if(message_Panel.type != PanelType.Popup)
		{
			NicePlay.Utilities.DebugLog("Panel is not a Popup type");
		}
		else
		{
			if(!m_PopupPanelList.Contains(message_Panel))
			{
				if(m_PopupPanelList.Count <= 0)
				{
					message_Panel.Depth = POPUP_DEPTH;
				}
				else
				{
					message_Panel.Depth = m_PopupPanelList[m_PopupPanelList.Count-1].Depth + 4;
				}
				m_PopupPanelList.Add(message_Panel);
			}
			message_Panel.SetMessage(title, msg, actions);
			message_Panel.Initialize();	
		}
	}

	public void PlayPopup(GamePanel panel)
	{
		if(panel.type != PanelType.Popup)
		{
			NicePlay.Utilities.DebugLog("Panel is not a Popup type");
		}
		else
		{
			Debug.Log("open 1 =" +m_IsTransition);
			if(!m_IsTransition)
			{
				SetTransition(true);

				if(!m_PopupPanelList.Contains(panel))
				{
					if(m_PopupPanelList.Count <= 0)
					{
						panel.Depth = POPUP_DEPTH;
					}
					else
					{
						panel.Depth = m_PopupPanelList[m_PopupPanelList.Count-1].Depth + 4;
					}
					m_PopupPanelList.Add(panel);
				}
				panel.Initialize(()=> SetTransition(false));
				Debug.Log("open 2 =" +m_IsTransition);
			}	
		}
	}

	public void ClosePopup(GamePanel panel)
	{
		if(panel.type != PanelType.Popup)
		{
			NicePlay.Utilities.DebugLog("Panel is not a Popup type");
		}
		else
		{
		
			if(!m_IsTransition)
			{
				SetTransition(true);
				Debug.Log("Closeeeeee =" +m_IsTransition);
				panel.Out();
				SetTransition(false);
				if(m_PopupPanelList.Contains(panel))
				{
					m_PopupPanelList.Remove(panel);
				}
			}
		}
	}

	private void StartPanel(GamePanelInfo panel)
	{
		m_CurrentPanel = panel;
		foreach(PanelType key in m_CurrentPanel.panels.Keys)
		{
			if(!m_CurrentPanel.panels[key].IsIn)
			{
				if(!m_CurrentPanel.panels.ContainsKey(PanelType.Right) && key == PanelType.Left)
				{
					m_CurrentPanel.panels[key].Initialize(()=> SetTransition(false));
				}
				else if(key == PanelType.Right)
				{
					m_CurrentPanel.panels[key].Initialize(()=> SetTransition(false));
				}
				else
				{
					m_CurrentPanel.panels[key].Initialize();
				}
			}
			else
			{
				m_CurrentPanel.panels[key].OnChanged();
			}
		}
	}

	private void SetTransition(bool isTransition)
	{
		m_IsTransition = isTransition;
	}
}
