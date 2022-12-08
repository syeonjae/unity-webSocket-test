using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class WebSockerServerManager : MonoBehaviour
{
    WebSocketServer wssv = new WebSocketServer("ws://localhost:8080");


    private void OnEnable()
    {
        wssv.Start();

        wssv.AddWebSocketService<UserPosInfo>("/UserPosInfo");

        if (wssv.IsListening)
        {
            Debug.Log($"Listening on port {wssv.Port}, and providing WebSocket services");

            foreach (var path in wssv.WebSocketServices.Paths)
                Debug.Log($"- {path}");
        }
    }


    private void OnDisable()
    {
        wssv.Stop();

        Debug.Log("Socket Server Stop...");

    }
}
