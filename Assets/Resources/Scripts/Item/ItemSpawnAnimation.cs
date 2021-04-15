using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnAnimation : MonoBehaviour
{
    public bool isStartAnimation = false;
    public bool isGet = false;

    public float spreadSize = 0.7f;  // 흩어짐 범위
    private Vector3 targetPosition;

    private void Update()
    {
        if (!isStartAnimation)  // 애니메이션이 시작전 리턴
            return;

        if (Mathf.Abs(transform.localPosition.x + 0.1f) >= Mathf.Abs(targetPosition.x) &&
            Mathf.Abs(transform.localPosition.x - 0.1f) <= Mathf.Abs(targetPosition.x) &&
            Mathf.Abs(transform.localPosition.y + 0.1f) >= Mathf.Abs(targetPosition.y) &&
            Mathf.Abs(transform.localPosition.y - 0.1f) <= Mathf.Abs(targetPosition.y)
            )
        {
            isGet = true;
        }

        if (Mathf.Abs(transform.localPosition.x + 0.03f) >= Mathf.Abs(targetPosition.x) &&
            Mathf.Abs(transform.localPosition.x - 0.03f) <= Mathf.Abs(targetPosition.x) &&
            Mathf.Abs(transform.localPosition.y + 0.03f) >= Mathf.Abs(targetPosition.y) &&
            Mathf.Abs(transform.localPosition.y - 0.03f) <= Mathf.Abs(targetPosition.y)
            )
        {
            isStartAnimation = true;
        }

        transform.localPosition = Vector3.Slerp(transform.localPosition, targetPosition, 0.05f);
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
