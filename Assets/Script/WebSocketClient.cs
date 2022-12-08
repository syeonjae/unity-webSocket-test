using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ProtoBuf;
using WebSocketSharp;
public class WebSocketClient : MonoBehaviour
{
    WebSocket ws = new WebSocket("ws://localhost:8080/UserPosInfo");


    private void Start()
    {

        ws.OnOpen += Ws_OnOpen;
        ws.OnClose += Ws_OnClose;
        ws.OnMessage += Ws_OnMessage;

        ws.Connect();

    }

    private void Update()
    {
        // Req Logic
        Vector3 pos = this.transform.position;
        var userPos = new Cw.UserPos
        {
            name = gameObject.name,
            MemoberIndex = 1,
            PosX = (int)pos.x,
            PosY = (int)pos.y,
            PoxZ = (int)pos.z,

        };
        using (var memoryStream = new MemoryStream())
        {
            Serializer.Serialize(memoryStream, userPos);
            var byteArray = memoryStream.ToArray();
            ws.Send(byteArray);
        }

    }

    void Ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);

    }

    void Ws_OnOpen(object sender, System.EventArgs e)
    {

    }

    void Ws_OnClose(object sender, CloseEventArgs e)
    {
        ws.Close();
    }

}
