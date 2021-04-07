using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Get : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Rigidbody2D rigidbody;

    Transform playerPos;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float timeToLeave = 10f;

    public Item item;
    public int count = 1;

    Vector2 playerCenterPos;    //bottom이 플레이어의 축이기때문에 실제로 사용할 플레이어의 중심좌표(이미지상의 중심)

    // Awake에서 player 넣어주려고 하니까 Null 참조 에러 뜸. 그래서 Start로 바꿈
    private void Start()
    {
        playerPos = GameManager.instance.player.transform;  //게임매니저에 플레이어를 넣어놨기 때문에 이 코드를 사용하면 굳이 이 스크립트를 사용하는 오브젝트에 따로 플레이어를 넣지 않아도 됨
        DisapearItem();                                     // Update에서 사용하면 Update가 돌 때마다 실행이 되어버려서 Start에서 사용하도록 함(조언이 필요하다...)
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PickUpItem();
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.icon;
    }

    void PickUpItem()
    {
        playerCenterPos = new Vector2(playerPos.position.x, playerPos.position.y + 0.5f);   //update에서 실행되기 때문에 플레이어 중심 좌표를 계속 갱신
        float distance = Vector3.Distance(transform.position, playerCenterPos);             //bottom이 축인 플레이어 좌표가 아닌 이미지의 중심 좌표와의 거리

        // 아이템과 플레이어의 거리가 획득 가능 거리보다 크면 그냥 리턴
        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, playerCenterPos, speed * Time.deltaTime);

        // 아이템과 플레이어의 거리가 거의 겹쳤을 때 아이템 오브젝트 destroy(오브젝트풀링해서 SetActive false로 바꾸는게 좋을 수 있음) 
        if (distance < 0.1f)
        {
            //인벤토리에 아이템이 있으면 기존 아이템 카운트만 증가
            if(GameManager.instance.itemContainer != null)
            {
                GameManager.instance.itemContainer.Add(item, count);
            }
            else
            {
                Debug.LogWarning("No inventory container attached to the game manager");
            }
            Destroy(gameObject);    //획득 후 파괴
        }
    }

    //아이템 소멸 메소드
    public void DisapearItem()
    {
        //소멸까지 남은 시간이 0보다 클 때만 코루틴 실행
        if (timeToLeave > 0)
        {
            StartCoroutine("DisapearItem_Co");
        }
    }

    //아이템 소멸 코루틴(기본 20초 카운트. 2분으로 늘릴까 생각중임)
    IEnumerator DisapearItem_Co()
    {
        while (timeToLeave >= 0)
        {
            Debug.Log(timeToLeave);
            timeToLeave -= 1f;
            yield return new WaitForSeconds(1);
        }

        if(timeToLeave <= 0)
        {
            Destroy(gameObject);
        }
    }
}
