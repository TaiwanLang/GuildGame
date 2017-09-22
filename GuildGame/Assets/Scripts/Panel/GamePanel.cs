using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePanel : MonoBehaviour
{
    [System.Serializable]
    public class ChildPanel
    {
        public UIPanel panel;
        public int depth;

        public ChildPanel(UIPanel _panel, int _depth)
        {
            panel = _panel;
            depth = _depth;
        }
    }

    public PanelType type;

    [HideInInspector]
    public Animation panelAnimation;

    [HideInInspector]
    public AnimationClip[] animationClips;

    [HideInInspector]
    public ChildPanel[] childPanels;

    [HideInInspector]
    public bool IsIn = false, hasChildPanel = false;

    private UnityAction m_Action;

    private UIPanel m_UIPanel;
    private int m_Depth = 0;

    protected virtual void OnIn() { }
    protected virtual void OnOut() { }
    public virtual void OnChanged() { }
    public virtual void OnUpdated() { }
    public virtual void OnFocused() { }

    public int Depth
    {
        get { return m_Depth; }
        set { m_Depth = value; }
    }

    public void Initialize(UnityAction action = null)
    {
        if (GetComponent<Animation>() != null)
        {
            panelAnimation = GetComponent<Animation>();
            NicePlay.Utilities.DebugLog("Animation>() != null");
        }
        else if(transform.Find("Animation") != null && transform.Find("Animation").gameObject.GetComponent<Animation>() != null)
        {
            panelAnimation = transform.Find("Animation").gameObject.GetComponent<Animation>();
        }

        if (GetComponent<UIPanel>() != null)
        {
            m_UIPanel = GetComponent<UIPanel>();
            NicePlay.Utilities.DebugLog("GetComponent<UIPanel>() != null");
        }
        else if (type == PanelType.Popup)
        {
            NicePlay.Utilities.DebugLog("Automatically Adding UIPanel to GameObject");
            m_UIPanel = gameObject.AddComponent<UIPanel>() as UIPanel;
        }

        if (m_UIPanel != null && type == PanelType.Popup)
        {
            m_UIPanel.depth = Depth;
        }

        if (panelAnimation != null && CheckAnimationClip(2))
        {
        	gameObject.SetActive(true);
          	PlayAnimation(animationClips[2].name);
            NicePlay.Utilities.DebugLog("panelAnimation != null && !string.IsNullOrEmpty(resetAnimation)");
        }

        gameObject.SetActive(true);

        if (!IsIn)
        {
            In(action);
        }
    }

    private bool CheckAnimationClip(int index)
    {
        return animationClips != null && animationClips.Length >= index + 1 && animationClips[index] != null;
    }

    public virtual void In(UnityAction action = null)
    {
    	if(panelAnimation != null && CheckAnimationClip(0))
    	{
    		PlayAnimation(animationClips[0].name, false, action);
    	}
        else
        {
            if(action != null)
            {
                action();
            }
        	
        	StartCoroutine(Delay(0.1f, OnIn));
        }
    }

    public virtual void Out(UnityAction action = null)
    {
    	if(panelAnimation != null && CheckAnimationClip(1))
    	{
    		PlayAnimation(animationClips[1].name, true, action);
    	}
        else
        {
            if(action != null)
            {
                action();
            }
        	
        	StartCoroutine(Delay(0.1f, ()=>OnOutAnimation(null)));
        }
    }

    private void PlayAnimation(string animationName, bool isOut, UnityAction action = null)
    {
        m_Action = action;
        if (panelAnimation != null)
        {
            ActiveAnimation activeAnimation = ActiveAnimation.Play(panelAnimation, animationName, AnimationOrTween.Direction.Forward);
            if (!isOut)
            {
                EventDelegate.Add(activeAnimation.onFinished, () => OnInAnimation(activeAnimation), true);
            }
            else
            {
                EventDelegate.Add(activeAnimation.onFinished, () => OnOutAnimation(activeAnimation), true);
            }       
        }
        else
        {
            NicePlay.Utilities.DebugLog("Animation Error, Please check if object has Animation Component or Animation Name");
        }
    }

    public void PlayAnimation(string animationName, UnityAction action = null)
    {
        ActiveAnimation activeAnimation = ActiveAnimation.Play(panelAnimation, animationName, AnimationOrTween.Direction.Forward);
        m_Action = action;
        EventDelegate.Add(activeAnimation.onFinished, () => OnAnimation(activeAnimation), true);
        
    }

    public void PlayAnimation(string animationName,AnimationOrTween.Direction type, UnityAction action = null)
    {
        ActiveAnimation activeAnimation = ActiveAnimation.Play(panelAnimation, animationName, type);
        m_Action = action;
        EventDelegate.Add(activeAnimation.onFinished, () => OnAnimation(activeAnimation), true);
       
    }
	public void PlayLoopAnimation(string animationName)
	{
		panelAnimation.enabled=true;
		PlayAnimation(animationName);
	}


    private void OnInAnimation(ActiveAnimation anim)
    {
        if (m_Action != null)
        {
            m_Action();
        }
        IsIn = true;
        StartCoroutine(Delay(0.1f, OnIn));
    }

    IEnumerator Delay(float second, UnityAction action)
    {
    	yield return new WaitForSeconds(second);
    	if(action != null)
    	{
    		action();
    	}
    }

    private void OnOutAnimation(ActiveAnimation anim)
    {
        if (m_Action != null)
        {
            m_Action();
        }
        IsIn = false;
        OnOut();
        gameObject.SetActive(false);
    }

    private void OnAnimation(ActiveAnimation anim)
    {
        if (m_Action != null)
        {
            m_Action();
        }
    }

    protected void ClosePopup()
    {
        if(type == PanelType.Popup)
        {
            GamePanelManager.Instance.ClosePopup(this);
        }
    }
}
