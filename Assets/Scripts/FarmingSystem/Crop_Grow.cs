using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop_Grow : MonoBehaviour
{
    public GameObject dayTime_Controller;   //먼저 public 오브젝트 선언
    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite[] sprites;
    int growLevel;
    int dayPassCheck;

    public bool isFullyGrown;

    private void Start()
    {
        dayTime_Controller = GameObject.Find("GameManager");

        DayTime_Controller daytime = dayTime_Controller.GetComponent<DayTime_Controller>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("CropGrowing");
        growLevel = 0;
        dayPassCheck = daytime.day - 1;
    }

    IEnumerator CropGrowing()
    {
        DayTime_Controller daytime = dayTime_Controller.GetComponent<DayTime_Controller>(); //데이타임컨트롤러 선언 후 앞서 선언한 public 게임오브젝트에서 데이타임컨트롤러 가져옴

        //다 자랄때까지 실행
        while (isFullyGrown == false)
        {
            yield return new WaitForEndOfFrame();

            //하루가 지나고 6시가 되면 성장하도록 함. 00시에 심은 작물은 6시가 되어도 성장 안 함(하루가 넘어간게 아니기때문). 대소비교 하지않은 이유는 월말에서 월초로 넘어가는것 때문
            if (dayPassCheck != daytime.day && daytime.Hours == 6)
            {
                spriteRenderer.sprite = sprites[growLevel];
                dayPassCheck = daytime.day;
                growLevel++;
            }
            //성장레벨을 달성했을 때 완전성장여부 결정. 며칠 지나면 썩는 로직은 추후 작성예정
            if (growLevel == sprites.Length - 1)
            {
                isFullyGrown = true;
            }
        }
    }
}
