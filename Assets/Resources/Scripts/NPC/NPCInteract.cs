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

    private void Start()
    {
        //talkUI = talkPanel.GetComponent<TalkUI>();
    }

    private void OnTriggerEnter2D(Collider2D other) // 마우스 커서 바꿀거
    {
        if (null == cursorSprite)
            return;


    }

    private void OnMouseDown()  // 대화
    {
        talkPanel.SetActive(true);
        GameManager.instance.isOpenTalkPanel = true;
    }

    private void OnTriggerExit2D(Collider2D collision) // 마우스 커서 바꿀거
    {
        if (null == cursorSprite)
            return;


    }

}
