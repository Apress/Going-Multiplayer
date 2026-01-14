public class DeathZone : NetworkBehaviour
{
    int damage = 10;
    
    void OnTriggerEnter(Collider col)
    {
        if (isServer)
        {
           OnServerTriggerEnter(col);
        }
        if (isClient)
        {
            OnClientTriggerEnter(col);
        }
    }

    void OnServerTriggerEnter(Collider col)
    {
        var healthManager = col.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            if (healthManager.OnServerDamageWithEffect(gameObject, damage))
            {
                Debug.Log("I killed something");
            }
        }   
    }

    void OnClientTriggerEnter(Collider col)
    {
        //play some VFX, sound, etc... 
    }
}