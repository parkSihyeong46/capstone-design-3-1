using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject player;
    private Inventory inventory;

    public GameObject talkPanel;
    public GameObject shopObj;
    public bool isOpenShop;
    public bool isOpenTalkPanel;

    public AudioClip[] clips;
    AudioSource audioSource;

    void Awake()
    {
        instance = this;
        inventory = Inventory.Instance;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        // 장비 사용 테스트, 초심자세트 인벤에 생성
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.Axe));
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.Pick));
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.Hoe));
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.WateringCans));
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.CauliflowerSeed));
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.ParsnipSeed));
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(!(GameManager.instance.isOpenShop))
            {
                InputEventManager.Instance.OpenOptionTab();
            }
            else
            {
                shopObj.SetActive(false);
            }
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public enum EffectSound
    {
        SAND_WALK = 0,
        AXE = 1,
        PICK = 2,
        HOE = 3,
        SWING = 4,
    }

    public void PlayEffectSound(int effectSound)
    {
        switch(effectSound)
        {
            case (int)EffectSound.SAND_WALK:
                audioSource.clip = clips[0];
                if(!audioSource.isPlaying)
                    audioSource.Play();
                break;
            case (int)EffectSound.AXE:
                audioSource.clip = clips[1];
                if (audioSource.isPlaying)
                    audioSource.Stop();
                audioSource.Play();
                break;
            case (int)EffectSound.PICK:
                audioSource.clip = clips[2];
                if (audioSource.isPlaying)
                    audioSource.Stop();
                audioSource.Play();
                break;
            case (int)EffectSound.HOE:
                audioSource.clip = clips[3];
                if (audioSource.isPlaying)
                    audioSource.Stop();
                audioSource.Play();
                break;
            case (int)EffectSound.SWING:
                audioSource.clip = clips[4];
                if (audioSource.isPlaying)
                    audioSource.Stop();
                audioSource.Play();
                break;
        }
    }
}
