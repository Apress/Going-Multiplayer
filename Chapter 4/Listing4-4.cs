///<summary>
///The health manager of a player.
///</summary>
public class HealthManager : NetworkBehaviour
{
    const short MaxHealth = 1000;

    [SyncVar(Channel = Channel.Reliable, OnChange = nameof(OnClientHealthChanged))]
    short health;

    [Command]
    void CmdUpdateHealth(short newHealth)
    {
        //Commands are RPCs run on the server, but invoked by the client.
        health = newHealth;
    }

    [Client]
    void OnClientHealthChanged(short oldValue, short newValue)
    {
        if (isLocalPlayer)
        {
            //update only the local player UI
        }
        else
        {
            //update an healthbar on top of the character of a remote player
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        OnClientGameStarted();
    }

    [Client]
    void OnClientGameStarted()
    {
        health = startingHealth > 0 ? startingHealth : MaxHealth;
        CmdUpdateHealth(health);
    }

    [Client]
    public void OnClientHeal(short amount)
    {
        //If damage is actually relevant
        if (amount == 0) { return; }
        //If you're not already dead
        if (health < 1) { return; } 
        health = (short)Mathf.Clamp(health + amount, 1, MaxHealth);
        CmdUpdateHealth(health);
    }

    [Client]
    public bool OnClientDamageWithEffect(GameObject sourceOfDamage, short amount)
    {
        var damageData = new DirectDamageData(sourceOfDamage, DirectDamageCause.Effect, amount);
        return OnClientDamage(damageData);
    }

    /// <summary>
    /// Damages the player
    /// </summary>
    /// <param name="damageData"></param>
    /// <returns>True if the player dies because of the damage</returns>
    [Client]
    public bool OnClientDamage(DirectDamageData damageData)
    {
        //If you're not already dead
        if (health < 1) { return false; }
        health -= damageData.amount;
        CmdUpdateHealth(health);
        if (health > 0)
        {
            return false;
        }
        OnClientDied();
        return true;
    }

    [Client]
    void OnClientDied()
    {
        //ask the server to respawn
        CmdRespawn(Vector.Zero);
    }

    [Command]
    void CmdRespawn(Vector3 respawnPosition)
    {
        //Respawn the player at the desired location
    }
}