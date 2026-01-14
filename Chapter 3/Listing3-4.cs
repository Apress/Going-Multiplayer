using UnityEngine;
using Unity.Netcode;
public class ChatManager : NetworkBehaviour
{
    //Invoked by a script when player types in the UI of the chat (not an RPC)
    public void OnClientSendMessageToServer(string message)
    {
    	string localPlayerUsername = "Some username";
    	ServerMoodMessageReceivedRpc(localPlayerUsername, message);
    }

    //Invoked on client, executed on the server
	[Rpc(SendTo.Server)]
    void ServerMoodMessageReceivedRpc(string senderName, string message)
    {
        /* Here's an example of the type of operation you could do on the server 
        to prevent malicious actions from bad actors. */
        string redactedMessage = OnServerFilterBadWords(message);
        ClientMoodMessageReceivedRpc(senderName, redactedMessage);
    }

    //Invoked on server, executed on the server (not an RPC)
    string OnServerFilterBadWords(string message)
    { 
    	//some logic here to filter out bad words 
    }

    //Invoked on server, executed on all clients including the host
    [Rpc(SendTo.ClientsAndHost)]
    void ClientMoodMessageReceivedRpc(string senderName, string message)
    {
        Debug.Log($"'{senderName}' said: {message}");
    }
}