using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopPanel_MsgBox : GamePanel
{
    private UnityAction[] m_Actions;

    public GameObject confirmBtn_obj;
    public GameObject cancelBtn_obj;
    public GameObject neutralBtn_Obj;

	public UILabel confirm_Label;
	public UILabel cancel_Label;
    public UILabel neutral_Label;
	public UILabel msg_Label;
	public UILabel title_Label;

    public UIPanel[] mPanel = new UIPanel[3];

    public override void In(UnityAction action = null)
    {
        mPanel[0].depth = Depth+1;
        mPanel[1].depth = Depth+1;
        mPanel[2].depth = Depth+2;

        base.In(action);
    }

    private void Start()
    {
        UIEventListener.Get(confirmBtn_obj).onClick += OnClickFunction;
        UIEventListener.Get(cancelBtn_obj).onClick += OnClickFunction;
        UIEventListener.Get(neutralBtn_Obj).onClick += OnClickFunction;
    }


    public void SetMessage(string title, string msg, params UnityAction[] actions)
    {
		title_Label.text = title;
		msg_Label.text = msg;
		
        m_Actions = actions;

        if(actions != null && actions.Length > 0)
        {
            switch(actions.Length)
            {
                case 1:
                {
                    confirmBtn_obj.transform.localPosition = new Vector2(0,-216);
                    //@TODO Confirm Button Position
                    cancelBtn_obj.SetActive(false);
                    neutralBtn_Obj.SetActive(false);
                    break;
                }
                case 2:
                {
                    confirmBtn_obj.transform.localPosition = new Vector2(220,-216);
                    cancelBtn_obj.transform.localPosition = new Vector2(-230,-216);
                    //@TODO Confirm Button Position
                    //@TODO Cancel Button Position
                    cancelBtn_obj.SetActive(true);
                    neutralBtn_Obj.SetActive(false);
                    break;
                }
                case 3:
                {
                    confirmBtn_obj.transform.localPosition = new Vector2(267,-238);
                    cancelBtn_obj.transform.localPosition = new Vector2(-280,-238);
                    neutralBtn_Obj.transform.localPosition = new Vector2(-15,-112);
                    //@TODO Confirm Button Position
                    //@TODO Cancel Button Position
                    //@TODO Neutral Button Position
                    cancelBtn_obj.SetActive(true);
                    neutralBtn_Obj.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            confirmBtn_obj.transform.localPosition = new Vector2(0,-216);
            cancelBtn_obj.SetActive(false);
            neutralBtn_Obj.SetActive(false);
        }
    }

    private void ButtonAction(int index)
    {
        if(m_Actions != null && m_Actions.Length > 0)
        {
            if (m_Actions.Length > index && m_Actions[index] != null)
            {
                m_Actions[index]();
            }
        }   
    }

    public void OnClickFunction(GameObject btn)
    {
       GamePanelManager.Instance.ClosePopup(this);
        if (btn == confirmBtn_obj)
        {
            ButtonAction(0);
        }
        else if (btn == cancelBtn_obj)
        {
			ButtonAction(1);
        }
        else if(btn == neutralBtn_Obj)
        {
            ButtonAction(2);
        }
    }
}
