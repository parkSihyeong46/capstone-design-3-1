using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemUI : MonoBehaviour
{
    public Transform itemImageTransform;

    public Image itemboxImage;
    public Image itemNameTextBodyImage;
    public Image itemNameTextTailImage;
    public Image itemImage;
    
    public Text explainTextText;
    public Text countTextText;

    private RectTransform imageRectTransform;

    public bool isPrintcount = false;
    private bool isReduceSize = false;

    private const int MaxImageSize = 64;
    private const int MinImageSize = 48;
    private const int StartImageSize = 60;

    private const float resizeSpeed = 30.0f;
    private float accumResizeTime = 0.0f;
    private const float waitTime = 1.0f;
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

        imageRectTransform = itemImageTransform.GetComponent<RectTransform>();
    }

    public void ResetImageSize()
    {
        accumDisapearTime = 0.0f;
    }

    private void Update()
    {
        if(!isReduceSize)
        {
            accumResizeTime += Time.deltaTime;
            imageRectTransform.sizeDelta = new Vector2(StartImageSize + (accumResizeTime * resizeSpeed), StartImageSize + (accumResizeTime * resizeSpeed));

            if(imageRectTransform.sizeDelta.x >= MaxImageSize)
            {
                imageRectTransform.sizeDelta = new Vector2(MaxImageSize, MaxImageSize);
                accumResizeTime = 0.0f;
                isReduceSize = true;
            }
        }
        else if (imageRectTransform.sizeDelta.x > MinImageSize)
        {
            accumResizeTime += Time.deltaTime;

            imageRectTransform.sizeDelta = new Vector2(MaxImageSize - (accumResizeTime * resizeSpeed), MaxImageSize - (accumResizeTime * resizeSpeed));
        }
        else if(accumWaitTime < waitTime)
        {
            accumWaitTime+= Time.deltaTime;
        }
        else
        {
            accumDisapearTime += Time.deltaTime;

            SetColor(1.0f - accumDisapearTime, isPrintcount);
            if ((0.0f - 0.01f) > GetColor())
                Destroy(gameObject);
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