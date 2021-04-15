using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    private Transform playerTransform;
    private ItemSpawnAnimation itemSpawnAnimaton;

    private Item item = null;
    public bool isGet = false;

    public const float MAGNET_RADIUS = 2.0f;
    private Vector3 accumPosition;
    public void Init(Item item)
    {
        this.item = item;
        itemSpawnAnimaton = GetComponent<ItemSpawnAnimation>();
        playerTransform = Player_Manager.instance.transform;
    }

    private void Update()
    {
        if (!itemSpawnAnimaton.isDone)
            return;

        if(Vector3.Distance(playerTransform.position, transform.localPosition) <= MAGNET_RADIUS)
        {
            accumPosition += (playerTransform.position - transform.localPosition).normalized * Time.deltaTime;
            transform.localPosition += accumPosition;
        }
    }

    public void TriggerPlayer()
    {
        Inventory.Instance.AddItem(item);
        GetItemUIManager.Instance.PrintUI(item);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGet)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerPlayer();
            Destroy(gameObject);
        }
    }
}
