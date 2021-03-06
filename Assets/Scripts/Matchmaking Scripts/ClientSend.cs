using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : PacketSendInterface
{
    public static void SendUpdateRequest()
    {
        using Packet _packet = new Packet((int)ClientMatchmakerPackets.updateRequest);
        SendTCPData(_packet);
    }

    public static void SendDisconnect()
    {
        using Packet _packet = new Packet((int)ClientMatchmakerPackets.disconnect);
        SendTCPData(_packet);
    }
}