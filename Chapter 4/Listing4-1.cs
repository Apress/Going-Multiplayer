using UnityEngine; 
using Unity.Netcode; 

public class MyNetworkManager : MonoBehaviour 
{ 
    const string defaultServerListenAddress = "0.0.0.0";

    //The device that acts as a server calls this on startup
    public void StartServer(ushort listeningPort)
    {
        SetNetworkPortAndAddress(defaultServerListenAddress, listeningPort, defaultServerListenAddress);
        m_NetworkManager.StartServer(); //or StartHost(), if you also want the server to be a Client
    }

    //The client calls this to connect to the server 
    public void ConnectToServer(string serverIP, ushort serverPort)
    {
        SetNetworkPortAndAddress(serverIP, serverPort, defaultServerListenAddress);
        m_NetworkManager.StartClient();
    }

    void SetNetworkPortAndAddress(string address, ushort port, string serverListenAddress)
    {
        var transport = GetComponent<UnityTransport>();
        transport.SetConnectionData(address, port, serverListenAddress);
    }
}