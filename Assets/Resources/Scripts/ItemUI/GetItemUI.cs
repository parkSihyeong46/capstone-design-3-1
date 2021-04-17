using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemUI : MonoBehaviour  // 싱글톤으로 만들기
{
    public Transform itemImageTransform;
    private Transform itemNameTextTailTransform;

    public Image itemboxImage;
    public Image itemNameTextBodyImage;
    public Image itemNameTextTailImage;
    public Image itemImage;
    
    public Text explainTextText;
    public Text countTextText;

    private RectTransform itemNameTextBodyRectTransform;
    private RectTransform imageRectTransform;

    public bool isPrintcount = false;
    private bool isReduceSize = false;

    private const int INIT_TEXTBOX_SIZE = 2;  // 초기 텍스트 상자 사이즈
    private const int INIT_TEXTBOX_TAIL_LOCATION = 43;  // 늘어난 텍스트상자 이미지 끝에 꼭다리 (맨 우측 둥그런 테두리)
    private const int WORLD_DISTANCE = 20;   // 글자 하나당 변경될 텍스트상자 크기

    private const int MAX_IMAGE_SIZE = 64;
    private const int MIN_IMAGE_SIZE = 48;
    private const int START_IMAGE_SIZE = 60;

    private const float RESIZE_SPEED = 30.0f;
    private float accumResizeTime = 0.0f;
    private const float WAIT_TIME = 1.0f;
    private float accumWaitTime = 0.0f;
    private float accumDisapearTime = 0.0f;


    private void OnEnable()
    {
        itemImageTransform = transform.GetChild(3).GetComponent<Transform>();

        itemboxImage = transform.GetChild(0).GetComponent<Image>();
        itemNameTextBodyImage = transform.GetChild(1).GetComponent<Image>();
        itemNameTextTailImage = transform.GetChild(2).GetComponent<Image>();
        itemImage = itemImageTransform.GetComponent<Image>();

        explainTextText = transform.GetChild(4).GetComponent<Text>();
        countTextText = transform.GetChild(5).GetComponent<Text>();

        itemNameTextBodyRectTransform = transform.GetChild(1).GetComponent<RectTransform>();
        imageRectTransform = itemImageTransform.GetComponent<RectTransform>();
    }

    public void ReSizeTextBox(int textLength)
    {
        int expandSize = textLength * WORLD_DISTANCE + INIT_TEXTBOX_SIZE;
        itemNameTextBodyRectTransform.sizeDelta = new Vector2(expandSize, itemNameTextBodyRectTransform.sizeDelta.y);
        itemNameTextTailImage.transform.localPosition = new Vector3
            (INIT_TEXTBOX_TAIL_LOCATION + expandSize, itemNameTextTailImage.transform.localPosition.y, itemNameTextTailImage.transform.localPosition.z);
    }

    public void ResetImageSize()
    {
        isReduceSize = false;
        imageRectTransform.sizeDelta = new Vector2(START_IMAGE_SIZE, START_IMAGE_SIZE); 
        accumResizeTime = 0.0f;
        accumWaitTime = 0.0f;
        accumDisapearTime = 0.0f;
        SetColor(1, isPrintcount);
    }

    private void Update()
    {
        if(!isReduceSize)
        {
            accumResizeTime += Time.deltaTime;
            imageRectTransform.sizeDelta = new Vector2(START_IMAGE_SIZE + (accumResizeTime * RESIZE_SPEED), START_IMAGE_SIZE + (accumResizeTime * RESIZE_SPEED));

            if(imageRectTransform.sizeDelta.x >= MAX_IMAGE_SIZE)
            {
                imageRectTransform.sizeDelta = new Vector2(MAX_IMAGE_SIZE, MAX_IMAGE_SIZE);
                accumResizeTime = 0.0f;
                isReduceSize = true;
            }
        }
        else if (imageRectTransform.sizeDelta.x > MIN_IMAGE_SIZE)
        {
            accumResizeTime += Time.deltaTime;

            imageRectTransform.sizeDelta = new Vector2(MAX_IMAGE_SIZE - (accumResizeTime * RESIZE_SPEED), MAX_IMAGE_SIZE - (accumResizeTime * RESIZE_SPEED));
        }
        else if(accumWaitTime < WAIT_TIME)
        {
            accumWaitTime+= Time.deltaTime;
        }
        else
        {
            accumDisapearTime += Time.deltaTime;

            SetColor(1.0f - accumDisapearTime, isPrintcount);
            if (!((0.0f + 0.02f) > GetColor()))
                return;

            List<GetItemUI> uiList = GetItemUIManager.Instance.GetItemUIList();
            for (int i = 0; i < uiList.Count; i++)
            {
                if (uiList[i] != this)
                    continue;

                uiList.Remove(this);
                Destroy(gameObject);

                GetItemUIManager.Instance.ResetLocation();
                return;
            }     
        }
    }

    public void SetImageNText(Item item)
    {
        itemImage.sprite = item.ItemImage;
        explainTextText.text = item.ItemName;

        if(!isPrintcount)
        {
            Color color = countTextText.color;
            color.a = 0.0f;
            countTextText.color = color;

            return;
        }

        countTextText.text = item.Count.ToString();
    }
    public void SetColor(float alpha, bool isPrintCount = false)
    {
        Color color = itemboxImage.color;
        color.a = alpha;
        itemboxImage.color = color;

        color = itemNameTextBodyImage.color;
        color.a = alpha;
        itemNameTextBodyImage.color = color;

        color = itemNameTextTailImage.color;
        color.a = alpha;
        itemNameTextTailImage.color = color;

        color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;

        color = explainTextText.color;
        color.a = alpha;
        explainTextText.color = color;


        if(isPrintCount)
        {
            color = countTextText.color;
            color.a = alpha;
            countTextText.color = color;
        }
    }

    public float GetColor()
    {
        Color color = itemboxImage.color;
        return color.a;
    }
}