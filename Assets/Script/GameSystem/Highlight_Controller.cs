using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight_Controller : MonoBehaviour
{
    [SerializeField] GameObject highlight;
    GameObject currentTarget;
    Bounds bound;

    private void Start()
    {
        
    }

    public void Highlight(GameObject target)
    {
        //이미 마커가 표시되어있으면 리턴
        if(currentTarget == target)
        {
            return;
        }
        currentTarget = target;

        bound = currentTarget.GetComponent<Collider2D>().bounds;

        Vector3 position = target.transform.position;   // 마커 위치 설정
        Highlight(position);
    }

    public void Highlight(Vector3 position)
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
