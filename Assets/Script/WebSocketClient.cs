using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ProtoBuf;
using WebSocketSharp;
public class WebSocketClient : MonoBehaviour
{
    WebSocket ws = new WebSocket("ws://localhost:8080/UserPosInfo");

    private void Awake()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void Start()
    {

        ws.OnOpen += Ws_OnOpen;
        ws.OnClose += Ws_OnClose;
        ws.OnMessage += Ws_OnMessage;

        ws.Connect();

    }

    private void Update()
    {
        // Move Logic
        float mouseX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        float mouseZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        transform.Rotate(0, mouseX, 0);
        transform.Translate(0, 0, mouseZ);

        // Req Logic
        Vector3 pos = this.transform.position;
        var userPos = new Cw.UserPos
        {
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
