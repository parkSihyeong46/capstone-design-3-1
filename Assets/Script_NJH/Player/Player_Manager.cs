using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public Character character;

    Player_Movement player_Movement;
    Player_Interact player_Interact;
    Player_Farming player_Farming;

    private StaminaBar staminaBar = null;

    SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rigidbody;

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

            player_Interact.UseTool();
        }
        //else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)    //현재 애니메이션이 끝나면 false로 바꿔서 반복 안함. 그냥 Loop 체크 안하면 되지않나?
        //{
        //    animator.SetBool("Work", false);
        //}
    }

    public void UseStamina()
    {
        Tool tool;

        tool = ToolManager.Instance.GetTool(handItem);
        if (tool != null)
        {
            staminaBar.UseStamina(tool.UseStamina);
        }
    }
}
