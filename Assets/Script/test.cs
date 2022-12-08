using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.IO;

public class test : MonoBehaviour
{



    private void Awake()
    {

        var userPos = new Cw.UserPos
        {
            MemoberIndex = 1,
            PosX = 5,
            PosY = 5,
            PoxZ = 5,
        };

        using(var memoryStream = new MemoryStream())
        {
            Serializer.Serialize(memoryStream, userPos);
            var byteArray = memoryStream.ToArray();

        }

        using (var fileStream = File.Create("userPos.buf"))
        {
            Serializer.Serialize(fileStream, userPos);
            var byteArray = fileStream.ToString();

        }

        using (var fileStream = File.OpenRead("userPos.buf"))
        {
            var myUser = Serializer.Deserialize<Cw.UserPos>(fileStream);
            Debug.Log(myUser.PosX);
        }
    }

    private void PrintBytes(byte[] bytes)
    {
        foreach(byte i in bytes)
        {
            Debug.Log(i);
        }
    }





}
