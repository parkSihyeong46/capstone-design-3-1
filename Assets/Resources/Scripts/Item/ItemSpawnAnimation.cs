using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnAnimation : MonoBehaviour
{
    FieldItem fieldItem;

    public bool isStartAnimation = false;
    public bool isDone = false;

    public float spreadSize = 0.7f;  // 흩어짐 범위
    public const float SPAWN_SPEED = 0.05f; // 이동시간 (스폰 속도)

    private const float ERROR_RANGE_GET = 0.1f;  // 오차범위 (아이템 획득)
    private const float ERROR_RANGE_STOP = 0.03f;  // 오차범위 (애니메이션 중지)
    private Vector3 targetPosition;
    private void Start()
    {
        fieldItem = GetComponent<FieldItem>();
    }
    private void Update()
    {
        if (!isStartAnimation)  // 애니메이션이 시작전 리턴
            return;

        if (transform.localPosition.x + ERROR_RANGE_GET >= targetPosition.x &&
            transform.localPosition.x - ERROR_RANGE_GET <= targetPosition.x &&
            transform.localPosition.y + ERROR_RANGE_GET >= targetPosition.y &&
            transform.localPosition.y - ERROR_RANGE_GET <= targetPosition.y
            )
        {
            fieldItem.isGet = true;
        }

        if (transform.localPosition.x + ERROR_RANGE_STOP >= targetPosition.x &&
            transform.localPosition.x - ERROR_RANGE_STOP <= targetPosition.x &&
            transform.localPosition.y + ERROR_RANGE_STOP >= targetPosition.y &&
            transform.localPosition.y - ERROR_RANGE_STOP <= targetPosition.y
            )
        {
            isStartAnimation = false;
            isDone = true;
        }

        transform.localPosition = Vector3.Slerp(transform.localPosition, targetPosition, SPAWN_SPEED);
    }

    public void StartSpawnAnimation()
    {
        isStartAnimation = true;
        targetPosition = new Vector3(
            transform.localPosition.x + Random.Range(-spreadSize, spreadSize), 
            transform.localPosition.y + Random.Range(-spreadSize, spreadSize),
            transform.localPosition.z);
    }
}
