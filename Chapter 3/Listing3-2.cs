using Mirror; 

public class Player : NetworkBehaviour 

{ 

    [SyncVar(hook = nameof(OnClientHealthChanged))] 

    int health = 100; 

     

    //Called on all clients when the value of “health” changes in some method on the server 

    void OnClientHealthChanged(int oldValue, int newValue) 

    { 

        UnityEngine.Debug.Log($“[Client] Health changed from {oldValue} to {newValue}”); 

    } 

} 