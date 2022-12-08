using System;
using System.IO;
using UnityEngine;
using ProtoBuf;
using WebSocketSharp;
using WebSocketSharp.Server;

public class UserPosInfo : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Cw.UserPos feed = Serializer.Deserialize<Cw.UserPos>(new MemoryStream(e.RawData));
        Debug.Log($"{feed.name} : ({feed.PosX}, {feed.PosY}, {feed.PoxZ})");

    }
}
