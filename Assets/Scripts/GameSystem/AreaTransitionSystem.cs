using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitionSystem : MonoBehaviour
{
    Player_Manager player_Manager;
    [SerializeField] AreaTransitionSystem_UI areaTransitionUI;

    Transform playerPos;
    public GameObject connectedGate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        player_Manager = collision.GetComponent<Player_Manager>();
        playerPos = collision.gameObject.transform;    //플레이어의 위치 가져옴. 플레이어 위치를 변경할 수 있음

        areaTransitionUI.areaTransition = this;
        areaTransitionUI.TransitionFadeOut();
    }

    public void ChangeArea()
    {
        playerPos.position = new Vector2(connectedGate.transform.position.x, connectedGate.transform.position.y - 2);
    }
}
