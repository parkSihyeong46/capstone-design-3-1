using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour
{
    public Sprite cursorSprite;
    public Sprite npcSprite;
    public string npcName;

    public Text talkText;
    public GameObject talkPanel;
    public TalkUI talkUI;

    public GameObject shopObject;
    public GameObject optionTab;
    public State state;

    private void OnTriggerEnter2D(Collider2D other) // 마우스 커서 바꿀거
    {
        if (null == cursorSprite)
            return;


    }

    private void OnMouseDown()  // 상호작용
    {
        switch (state)
        {
            case State.TALK:
                talkPanel.SetActive(true);
                GameManager.instance.isOpenTalkPanel = true;
                break;
            case State.TRADE:
                if(!GameManager.instance.isOpenTalkPanel)
                {
                    if(optionTab.activeSelf)
                    {
                        optionTab.SetActive(false);
                    }

                    shopObject.SetActive(true);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // 마우스 커서 바꿀거
    {
        if (null == cursorSprite)
            return;


    }

    public enum State
    {
        TALK = 0,
        TRADE = 1,
    }
}
