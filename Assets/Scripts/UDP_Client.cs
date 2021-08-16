using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDP_Client : MonoBehaviour
{
    void Update()
    {
        //UDPTest();
    }

    void UDPTest()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UdpClient client = new UdpClient(5600);
            try
            {
                client.Connect("127.0.0.1", 8080);
                byte[] sendBytes = Encoding.ASCII.GetBytes("Hello GEOEXPERT");
                client.Send(sendBytes, sendBytes.Length);
                Debug.Log("Отправил сообщение серверу");
            }
            catch(Exception e)
            {
                print("Exception thrown " + e.Message);
            }
        }
    }
}
