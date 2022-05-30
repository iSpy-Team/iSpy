using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Database handling
/// </summary>
public class ServerEntries
{
    private static ServerEntries singleton;
    private readonly Dictionary<int, ServerDataEntry> serverList = new Dictionary<int, ServerDataEntry>();

    /// <summary>
    /// The one and only entry database
    /// </summary>
    public static ServerEntries Singleton
    { get { if (singleton == null) singleton = new ServerEntries(); return singleton; } }

    /// <summary>
    /// The main database, contains the latest updated state for every server
    /// </summary>
    public Dictionary<int, ServerDataEntry> Database => serverList;

    public static event Action OnDatabaseUpdate;

    /// <summary>
    /// Inserts a new entry in the database
    /// </summary>
    /// <param name="_entry">new entry</param>
    public void SetData(ServerDataEntry _entry)
    {
        // check if the entry doesn't exist
        foreach (var e in serverList)
        {
            if (_entry.Port == e.Value.Port)
            {
                Debug.LogWarning($"Duplicate entry, destroying");
                return;
            }
        }

        // add the entry to server list
        serverList.Add(_entry.Port, _entry);
        OnDatabaseUpdate?.Invoke();
    }

    /// <summary>
    /// Updates a data entry in the database
    /// </summary>
    /// <param name="_port">entry index</param>
    /// <param name="_PlayerCount">entry's new player count</param>
    /// <param name="_isRunning">entry's new running state</param>
    public void UpdateData(int _port, int _PlayerCount, bool _isRunning)
    {
        serverList[_port].UpdateEntry(_PlayerCount);
        serverList[_port].UpdateEntry(_isRunning);

        OnDatabaseUpdate?.Invoke();
    }
}