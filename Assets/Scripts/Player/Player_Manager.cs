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

    [SerializeField] private GameObject optionTab;
    public Item handItem;  // 손에 들고있는 아이템
   
    public static Player_Manager instance = null;   // 다른곳에서 playerManager를 참조하기 위한 static instance

    [Header("상태 및 설정 값")]
    public bool isMoving;
    public bool isAnimation;    //애니메이션 시작과 끝을 확인하는데 사용
    public float interactRange = 1.5f;

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
        if(isAnimation == false)
            player_Movement.Move();

        HoldItem();
    }

    private void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            if (optionTab.activeSelf)  // 옵션창 활성화 -> 사용 X
                return;

            if(isAnimation == false)        //애니메이션이 실행되고 있지 않을 경우
            {
                player_Interact.UseTool();          //도구(아이템)사용
                rigidbody.velocity = Vector2.zero;  //속도0으로 만들어 정지, isAnimation이 true가 되면 다시 Move 실행 가능
            }
        }
    }

    public void UseStamina()    //스태미나 사용 메소드
    {
        Tool tool;

        tool = ToolManager.Instance.GetTool(handItem);
        if (tool != null)
        {
            staminaBar.UseStamina(tool.UseStamina);
        }
    }

    void HoldItem()
    {
        if(handItem == null) { return; }

        if (handItem.ItemType == Item.ItemTypes.Seed)
        {
            animator.SetBool("Hold_Item", true);
        }
        else
        {
            animator.SetBool("Hold_Item", false);
        }
    }

    public void RunAnimation(string parameter)  //애니메이션 실행 명령 메소드 (trigger 한정)
    {
        animator.SetTrigger(parameter);
    }

    public void StartAnimation()    //애니메이션 시작 시점 이벤트용 메소드
    {
        
        isAnimation = true;
        rigidbody.velocity = Vector2.zero;
        animator.SetBool("Move", false); //이동 애니메이션 실행 안하려고 넣은 코드(이거 안쓰면 게이트 이동할 때 걷는 애니메이션 계속 나와서)
    }

    public void EndAniamtion()      //애니메이션 종료 시점 이벤트용 메소드
    {
        isAnimation = false;
    }

    public void PlayWalkSound()
    {
        GameManager.instance.PlayEffectSound(0);
    }

    public void PlayEffectSound()
    {
        switch(handItem.ItemId)
        {
            case Item.ItemID.Axe:
                GameManager.instance.PlayEffectSound(1);
                break;
            case Item.ItemID.Pick:
                GameManager.instance.PlayEffectSound(2);
                break;
            case Item.ItemID.Hoe:
                GameManager.instance.PlayEffectSound(3);
                break;
        }
    }
}
