using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Get : MonoBehaviour
{
    Transform playerPos;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float timeToLeave = 10f;

    private void Start()
    {
        // Awake에서 player 넣어주려고 하니까 Null 참조 에러 뜸
        // 그래서 Start로 바꿈
        playerPos = GameManager.instance.player.transform;
        
    }

    private void Update()
    {
        PickUpItem();
        DisapearItem();
    }

    void PickUpItem()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);

        // 아이템과 플레이어의 거리가 획득 가능 거리보다 크면 그냥 리턴
        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        // 아이템과 플레이어의 거리가 거의 겹쳤을 때 아이템 오브젝트 destroy(나중에는 프리펩으로 만들고 오브젝트풀링해서 SetActive false로 바꾸는게 좋을 수 있음) 
        if (distance < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    void DisapearItem()
    {
        if (timeToLeave > 0)
        {
            StartCoroutine("DisapearItem_Co");
        }
    }

    IEnumerator DisapearItem_Co()
    {
        Debug.Log(timeToLeave);

        while (timeToLeave >= 0)
        {
            timeToLeave -= 1f;
            yield return new WaitForSeconds(1);
        }

        if(timeToLeave <= 0)
        {
            Destroy(gameObject);
        }
    }
}
