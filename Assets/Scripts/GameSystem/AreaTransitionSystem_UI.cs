using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitionSystem_UI : MonoBehaviour
{
    [SerializeField] Player_Manager player_Manager;

    Animator animator;
    public AreaTransitionSystem areaTransition;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TransitionFadeOut()
    {
        animator.SetTrigger("AreaFadeOut");
    }
    public void TransitionFadeIn()
    {
        animator.SetTrigger("AreaFadeIn");
    }

    public void TransitionArea()
    {
        areaTransition.ChangeArea();    //애니메이션 이벤트로 사용하기위해 여기서 ChageArea 가져와서 실행
        
        //임시로 아래쪽 보도록 설정했음. 좌,우로 나오는 문도 있으니까 그쪽 바라보게 할 메소드 작성 필요함
        player_Manager.animator.SetFloat("Last_X", 0);
        player_Manager.animator.SetFloat("Last_Y", -1);
    }

    public void PlayerLock()
    {
        player_Manager.StartAnimation();
    }
    public void PlayerUnlock()
    {
        player_Manager.EndAniamtion();
    }
}
