using Unity.Netcode; 

public class Player : NetworkBehaviour 
{ 

    List<int> myCardIDs; //all cards' IDs of the player/bot 

    void OnClientTryToPlayCard(int cardID) //client-side method called by the player
    { 
        ServerPlayCardRpc(cardID); 
    } 

     

    void OnServerActAsBot() //bot’s logic  

    { 

        //some code here that lets bots figure out what to do         
        int cardID = 10;
        OnServerPlayCard(cardID); 

    } 

    [Rpc(SendTo.Server)] //the player calls this method  

    void ServerPlayCardRpc(int cardID) 

    { 

        //sanity checks here for player-performed actions  

        if (myCardIDs.Contains(cardID)) 

        { 

            OnServerPlayCard(cardID); 

            return; 

        } 

        //this is a cheating attempt, or an error 

    } 

    //the bot calls this method directly, as it doesn’t cheat 

    void OnServerPlayCard(int cardID) {/* play the card! */} 

} 