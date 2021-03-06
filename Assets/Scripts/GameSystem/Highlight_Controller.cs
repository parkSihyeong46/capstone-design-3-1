using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight_Controller : MonoBehaviour
{
    [SerializeField] GameObject highlight;
    GameObject currentTarget;
    Bounds bound;

    public void Highlight(GameObject target)
    {
        Debug.Log("하이라이트 컨트롤러의 하이라이트 메소드 실행, " + currentTarget);
        
        //이미 마커가 표시되어있으면 리턴
        if(currentTarget == target)
        {
            return;
        }
        //마커가 표시되어 있지 않으니 전달 받은 게임오브젝트를 currentTarget에 대입
        currentTarget = target;

        bound = currentTarget.GetComponent<Collider2D>().bounds;

        Vector3 position = target.transform.position;   // 마커 위치 설정
        HighlightPos(position);
    }

    public void HighlightPos(Vector3 position)
    {
        highlight.SetActive(true);
        highlight.transform.position = new Vector3(position.x, position.y + bound.extents.y + 0.5f);
    }

    public void Hide()
    {
        currentTarget = null;
        highlight.SetActive(false);
    }
}
