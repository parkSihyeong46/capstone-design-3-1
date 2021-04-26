using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionTabManager : MonoBehaviour
{
    private OptionState optionState = OptionState.INVENTORY;
    private OptionState prevOptionState = OptionState.INVENTORY;

    private const float normalYPosYPos = 295;
    private const float selectYPos = 281;

    public List<Transform> button;
    public List<GameObject> option;

    private void OnEnable()
    {
        for(int i = 0; i < option.Count; i ++)
        {
            if (null == option[i])
                continue;

            option[i].SetActive(false);
        }

        button[(int)optionState].localPosition = new Vector3(
            button[(int)optionState].localPosition.x, selectYPos, button[(int)optionState].localPosition.z);

        if (option.Count >= (int)optionState)
        {
            if (null == button[(int)optionState])
                return;

            option[(int)optionState].SetActive(true);      
        }
    }

    public void SetOptionState(int state)
    {
        if (0 > state || (int)OptionState.EXIT < state)
            return;

        prevOptionState = optionState;
        optionState = (OptionState)state;

        button[(int)prevOptionState].localPosition = new Vector3(
            button[(int)prevOptionState].localPosition.x, normalYPosYPos, button[(int)prevOptionState].localPosition.z);

        OnEnable();
    }
}

[SerializeField]
public enum OptionState
{
    INVENTORY = 0,
    STAT = 1,
    INTIMACY = 2,
    MAP = 3,
    CREATE = 4,
    COLLECT = 5,
    OPTION = 6,
    EXIT = 7,
}