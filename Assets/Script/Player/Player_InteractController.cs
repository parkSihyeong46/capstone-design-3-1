using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InteractController : MonoBehaviour
{
    Player player;
    Character character;

    Rigidbody2D rigidbody;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeofInteractiveArea = 1.2f;
    [SerializeReference] Highlight_Controller highlightController;

    private void Awake()
    {
        player = GetComponent<Player>();
        character = GetComponent<Character>();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Check();

        if (Input.GetMouseButtonDown(0) && player.isMoving == false)
        {
            Interact();
        }
        else if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            player.animator.SetBool("usingTool", false);
        }
    }

    public void Check()
    {
        Vector2 position = rigidbody.position + player.directionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractiveArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }

        highlightController.Hide();
    }

    public void Interact()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        player.animator.SetBool("usingTool", true);

        Vector2 position = rigidbody.position + player.directionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractiveArea);

        foreach (Collider2D c in colliders)
        {
            // 상호작용 할 콜라이더를 hit에 저장
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    }
}
