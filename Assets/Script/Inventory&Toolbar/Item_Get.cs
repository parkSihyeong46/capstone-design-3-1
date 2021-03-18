using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Get : MonoBehaviour
{
    Transform playerPos;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float timeToLeave = 10f;

    public Item item;
    public int count = 1;

    private void Start()
    {
        // Awake에서 player 넣어주려고 하니까 Null 참조 에러 뜸
        // 그래서 Start로 바꿈
        playerPos = GameManager.instance.player.transform;
        DisapearItem(); // Update에서 사용하면 Update가 돌 때마다 실행이 되어버려서 Start에서 사용하도록 함(조언이 필요하다...)
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
        float distance = Vector3.Distance(transform.position, playerPos.position);

        // 아이템과 플레이어의 거리가 획득 가능 거리보다 크면 그냥 리턴
        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        // 아이템과 플레이어의 거리가 거의 겹쳤을 때 아이템 오브젝트 destroy(나중에 프리펩 만들면 오브젝트풀링해서 SetActive false로 바꾸는게 좋을 수 있음) 
        if (distance < 0.1f)
        {
            if(GameManager.instance.itemContainer != null)
            {
                GameManager.instance.itemContainer.Add(item, count);
            }
            else
            {
                Debug.LogWarning("No inventory container attached to the game manager");
            }
            Destroy(gameObject);
        }
    }

    public void DisapearItem()
    {
        if (timeToLeave > 0)
        {
            StartCoroutine("DisapearItem_Co");
        }
    }

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
