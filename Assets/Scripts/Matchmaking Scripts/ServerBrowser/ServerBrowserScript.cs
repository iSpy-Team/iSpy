using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ServerBrowserScript : MonoBehaviour
{
    public static ServerBrowserScript Singleton;
    private List<GameObject> contentList = new List<GameObject>();

    public EntryObject CurrentSelected;
    public string PlayerName;

    public GameObject EntryButtonPrefab;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else if (Singleton != this)
        {
            Debug.LogWarning($"ServerBrowserScript already existed, destorying...");
            Destroy(this);
        }
    }

    private void Start()
    {
        ServerEntries.onDatabaseUpdate += ServerEntries_onDatabaseUpdate;
    }

    /// <summary>
    /// Called when <c>ServerEntries.Singleton.Database gets updated</c>
    /// </summary>
    private void ServerEntries_onDatabaseUpdate()
    {
        // I know I should find a way to only update the updated ones, but I am running out of time so I just reset everything
        ResetAllData();

        // get server browser content
        var scrollRect = GetComponent<ScrollRect>();

        // foreach entries in the database, make an entry object
        for (int i = 0; i < ServerEntries.Singleton.Database.Count; i++)
        {
            var entry = ServerEntries.Singleton.Database[i];
            var entryObj = Instantiate(EntryButtonPrefab, scrollRect.content);
            entryObj.GetComponent<EntryObject>().UpdateData(i, entry);

            // add entry object to list of entry objects
            contentList.Add(entryObj);
        }
    }

    private void ResetAllData()
    {
        CurrentSelected = null;
        foreach (var entry in contentList)
        {
            Destroy(entry);
        }
        contentList.Clear();
    }

    public void B_ConnectToSelected()
    {
        if (ConnectChecks())
        {
            LobbyNetworkManager.ChangePort(ServerEntries.Singleton.Database[CurrentSelected.id].Port);
            MatchmakerClient.Singleton.Disconnect();
            NetworkClient.Connect(LobbyNetworkManager.GetAddress());
        }
    }

    public void B_RequestUpdate()
    {
        if (MatchmakerClient.Singleton.transport.socket.Connected)
        {
            ClientSend.SendUpdateRequest();
        }
    }

    public void ChangeName(string newName)
    {
        PlayerName = newName;
    }

    private bool ConnectChecks()
    {
        if (!CurrentSelected)
        {
            return false;
        }
        if (!string.IsNullOrEmpty(PlayerName))
        {
            return false;
        }
        return true;
    }
}