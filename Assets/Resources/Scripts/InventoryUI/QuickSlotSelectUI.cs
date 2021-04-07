using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotSelectUI : MonoBehaviour
{
    Vector2 wheelInput;

    private const int FIRST_SELECT_UI_X = -302;
    private const int LAST_SELECT_UI_X = 303;

    private Transform quickSlotSelectUI;

    private void Start()
    {
        quickSlotSelectUI = this.transform;
    }

    private void Update()
    {
        wheelInput = Input.mouseScrollDelta;

        if (wheelInput.y > 0) // 휠 올렸을 때
        {
            quickSlotSelectUI.Translate(Vector3.left * 55);

            if (quickSlotSelectUI.localPosition.x < FIRST_SELECT_UI_X)
            {
                quickSlotSelectUI.localPosition = new Vector3(LAST_SELECT_UI_X, quickSlotSelectUI.localPosition.y, 0);
            }
        }
        else if (wheelInput.y < 0) // 휠 내렸을 때
        {
            quickSlotSelectUI.Translate(Vector3.right * 55);

            if (quickSlotSelectUI.localPosition.x > LAST_SELECT_UI_X)
            {
                quickSlotSelectUI.localPosition = new Vector3(FIRST_SELECT_UI_X, quickSlotSelectUI.localPosition.y, 0);
            }
        }
    }
}
