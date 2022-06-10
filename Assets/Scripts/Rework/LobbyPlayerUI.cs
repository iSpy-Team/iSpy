using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System;

public class LobbyPlayerUI : NetworkBehaviour
{
    [SyncVar]
    public string DisplayName;

    [Command]
    public void CmdSetDisplayName(string displayName)
    {
        try
        {
            Debug.Log(displayName);
            DisplayName = displayName;
        }
        catch (Exception e)
        {
            // show a detailed error and let the user know what went wrong
            if (e.Source.Equals("Mirror"))
            {
                Debug.LogError("OnDeserialize failed for: object=" + e);
            }
            else
            {
                Debug.LogError(e);
            }
        }
    }
}
