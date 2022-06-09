using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Player.Weapons;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] List<Sprite> weaponSpriteList = new List<Sprite>();
    private Image sprite;

    [SerializeField] TMP_Text amountText;

    private WeaponSwap targetPlayer;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetTargetPlayer(WeaponSwap player)
    {
        targetPlayer = player;
    }

    public void UpdateSprite(string type, int amount)
    {
        gameObject.SetActive(true);
        sprite = GetComponent<Image>();
        
        switch(type)
        {
            case "Pistol":
                sprite.sprite = weaponSpriteList[0];
                break;
            case "Shotgun":
                sprite.sprite = weaponSpriteList[1];
                break;
            case "Sniper":
                sprite.sprite = weaponSpriteList[2];
                break;
            case "Rifle":
                sprite.sprite = weaponSpriteList[3];
                break;

            default:
                sprite.sprite = weaponSpriteList[0];
                break;
        }

        amountText.text = amount.ToString();
    }

    public void UpdateAmount(int amount)
    {
        amountText.text = amount.ToString();
    }
}
