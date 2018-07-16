using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NTM_NetworkServer : MonoBehaviour {

    public delegate void ReciveControllerData(float m_HVAxis, bool m_);
    public event ReciveControllerData ReciveControllerData_event;

    void Start ()
    {
        NetworkServer.Listen(25000);
        NetworkServer.RegisterHandler(888, ServerReceiveMessage);
    }
	
	void Update ()
    {
        ReciveControllerData_event += (float a, bool b) => { };
    }

    void OnGUI()
    {
        string ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected:" + NetworkServer.connections.Count);
    }

    private void ServerReceiveMessage(NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        string[] deltas = msg.value.Split(';');

        ReciveControllerData_event(Convert.ToSingle(deltas[0]), true);
        Debug.Log(msg.value);
    }
}
