using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu, enterNamePanel;

    [SerializeField]
    private TMP_InputField nameInputField;

    //[SerializeField]
    //RectTransform mainMenuTf, lobbyTf, enterNamePanelTf;
    [SerializeField]
    private RoomNetManager networkManager = null;

    [SerializeField]
    private string address;

    /*void OnEnable()
    {
        PlayerNameInput.OnClicked += ShowMainMenu;
    }
    void OnDisable()
    {
        PlayerNameInput.OnClicked -= ShowMainMenu;
    }
    void Start()
    {
        mainMenu = GameObject.Find("Main Menu");
        mainMenu.SetActive(false);
        enterNamePanel = GameObject.Find("Enter Name Panel");
        nameInputField = GameObject.Find("Name Input Field").GetComponent<TMP_InputField>();
        mainMenuTf = GameObject.Find("Menu Canvas").GetComponent<RectTransform>();
        mainMenuTf.DOAnchorPos(Vector2.zero, 0.25f);
    }*/

    public void SwipeUp()
    {
    }

    public void SwipeDown()
    {
    }

    public void SwipeRight()
    {
    }

    public void SwipeLeft()
    {
    }

    public void ShowMainMenu()
    {
        if (nameInputField.text != null)
        {
            Debug.Log(nameInputField.text);
            enterNamePanel.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void JoinGame()
    {
        networkManager.networkAddress = address;
        networkManager.StartClient();
        //ShowLobby();
    }
}