using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerScript : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (!isLocalPlayer) { return; }

        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(0, 5, moveZ);
        }
    }
}
