using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;

    [SerializeField] private UIPlayer playerUI;
    [SerializeField] private TMP_Text loseText;
    [SerializeField] private WeaponUI weaponUI;

    public UIPlayer PlayerUI { get { return playerUI; } }
    public TMP_Text LoseText { get { return loseText; } }
    public WeaponUI WeaponUI { get { return weaponUI; } }

    private void Awake()
    {
        instance = this;
    }
}
