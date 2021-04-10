using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    Player_Movement player_Movement;
    Player_Interact player_Interact;
    Player_Farming player_Farming;

    private StaminaBar staminaBar = null;

    SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rigidbody;

    [Header("Temp State")]
    [SerializeField] bool toolSelected;
    [SerializeField] bool seedSelected;
    [SerializeField] bool wateringToolSelected;

    public Item handItem;  // 손에 들고있는 아이템
   
    public static Player_Manager instance = null;   // 다른곳에서 playerManager를 참조하기 위한 static instance

    private void Awake()    // 다른곳에서 instance를 사용하게 되면 하나의 playerManager만을 참조할 수 있도록 설정
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // 씬이 넘어가도 playerManager는 삭제되지 않도록 설정
        }
        else
        {
            Destroy(this.gameObject);   // 씬이 넘어갔는데 그곳에도 다른 playerManager가 있다면 둘 중 하나는 없어지도록 설정
        }

        staminaBar = GameObject.FindWithTag("StaminaBar").GetComponent<StaminaBar>();
    }
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

        //레이어 정렬
        spriteRenderer.sortingOrder = -(Mathf.RoundToInt(transform.position.y));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && player_Movement.isMoving == false)
        {
            if (Inventory.Instance.IsOpen)  // 인벤토리 활성화 일 경우 사용 X
                return;

            UseTool();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)    //현재 애니메이션이 끝나면 false로 바꿔서 반복 안함. 그냥 Loop 체크 안하면 되지않나?
        {
            animator.SetBool("usingTool", false);
        }
    }

    void UseTool()
    {
        //***********************************************************************************************************************************************************************************
        //*                                                                                                                                                                                 *
        //*    지금은 임시로 물뿌리개랑 씨앗 등 전부 따로 분리했는데 toolSelected에서 먼저 확인하고 그 다음 어떤 아이템인지 확인하도록 변경하도록 할거임(UseItem이라는 메소드 만들 예정)    *
        //*                                                                                                                                                                                 *
        //***********************************************************************************************************************************************************************************

        //도구 선택했을 때(게임오브젝트 태그를 확인해서 어떤 도구인지 확인하고 그 도구에 맞는 동작 수행)


        if (seedSelected == true) { player_Farming.Seed(); } //임시 작물 프리펩 사용해서 현재는 한가지만 심을 수 있음


        if (handItem == null)   // 아이템을 들고있을 때만 수행하도록
            return;

        if (handItem.ItemType != Item.ItemTypes.Tool)   // 들고있는 아이템이 tool이 아니면 종료
            return; // 땅파기, 물주기는 아이템이 있어야만 수행 가능하니까

        switch (handItem.ItemId)
        {
            case Item.ItemID.Axe: // 나무캐기
                break;
            case Item.ItemID.Pick: // 돌부수기
                break;
            case Item.ItemID.Hoe:  // 땅 갈구기
                player_Farming.Plow();
                break;
            case Item.ItemID.WateringCans: // 물주기
                player_Farming.Watering();
                break;
        }
        UseStamina();   // 스태미나 소모

        // 위와 아래 코드 중복되는 것들이 있어 수정이 필요함

        if (toolSelected == true) { player_Farming.Plow(); }
        if (wateringToolSelected == true) { player_Farming.Watering(); }
       

        //그 외 상황은 상호작용
        else { player_Interact.Interact(); }

        player_Farming.Harvesting();
    }

    private void UseStamina()
    {
        Tool tool;

        tool = ToolManager.Instance.GetTool(handItem);
        if (tool != null)
        {
            staminaBar.UseStamina(tool.UseStamina);
        }
    }
}
