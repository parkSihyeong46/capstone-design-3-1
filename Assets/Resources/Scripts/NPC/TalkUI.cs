using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkUI : MonoBehaviour
{
    public GameObject npcImageObj;
    public GameObject npcNameObj;
    public GameObject talkTextObj;

    public Sprite npcImage;
    public Text npcName;
    public Text talkText;

    private void Start()
    {
        npcImage = npcImageObj.GetComponent<Sprite>();
        npcName = npcNameObj.GetComponent<Text>();
        talkText = talkTextObj.GetComponent<Text>();
    }

    public void ClickTalkUI()
    {
        if (GameManager.instance.isOpenTalkPanel)
        {
            GameManager.instance.isOpenTalkPanel = false;
            gameObject.SetActive(false);
        }
    }

}
