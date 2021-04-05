using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    Player_Movement player_Movement;
    Player_Interact player_Interact;
    Player_Farming player_Farming;

    SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rigidbody;

    [Header("Temp State")]
    [SerializeField] bool toolSelected;
    [SerializeField] bool seedSelected;
    [SerializeField] bool wateringToolSelected;

    private void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
        player_Interact = GetComponent<Player_Interact>();
        player_Farming = GetComponent<Player_Farming>();

        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        player_Movement.Move();
        spriteRenderer.sortingOrder = -(Mathf.RoundToInt(transform.position.y));
    }

    private void Update()
    {
        //여기있는 기능들 밖으로 빼낼 생각임

        if (Input.GetMouseButtonDown(0) && player_Movement.isMoving == false)
        {
            //도구 선택했을 때(게임오브젝트 태그를 확인해서 어떤 도구인지 확인하고 그 도구에 맞는 동작 수행)
            if (toolSelected == true) { player_Farming.Plow(); }    //아직은 땅 갈기만 있음. 물뿌리개 추가 예정

            if(wateringToolSelected == true) { player_Farming.Watering(); } //지금은 임시로 물뿌리개 따로 떼놨는데 toolSelected에서 먼저 확인하고 그 다음 어떤 도구인지 확인하도록 변경하도록 할거임

            //아이템(묘목, 씨앗 등) 선택했을 때
            if (seedSelected == true) { player_Farming.Seed(); }    //임시 작물 프리펩 사용해서 현재는 하나만 심을 수 있음

            //그 외 상황은 상호작용
            else { player_Interact.Interact(); }
        }
        
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)    //현재 애니메이션이 끝나면 false로 바꿔서 반복 안함
        {
            animator.SetBool("usingTool", false);
        }
    }
}
