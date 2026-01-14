using UnityEngine;

/// <summary>
/// The potential cause of a direct damage to a player
/// </summary>
public enum DirectDamageCause : byte
{
    Attack,
    Effect
}

public struct DirectDamageData
{
    public GameObject source;
    public DirectDamageCause cause;
    public short amount;

    public DirectDamageData(GameObject source, DirectDamageCause damageCause, short damageAmount)
    {
        this.source = source;
        cause = damageCause;
        amount = damageAmount;
    }
}

///<summary>
///The health manager of a player.
///</summary>
public class HealthManager : NetworkBehaviour
{
    const short MaxHealth = 1000;

    [SyncVar(Channel = Channel.Reliable, OnChange = nameof(OnClientHealthChanged))]
    short health;


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

    public override void OnStartServer()
    {
        base.OnStartServer();
        OnServerGameStarted();
    }

    [Server]
    void OnServerGameStarted()
    {
        health = startingHealth > 0 ? startingHealth : MaxHealth;
    }

    [Server]
    public void OnServerHeal(short amount)
    {
        //If damage is actually relevant
        if (amount == 0) { return; }
        //If you're not already dead
        if (health < 1) { return; } 
        health = (short)Mathf.Clamp(health + amount, 1, MaxHealth);
    }

    [Server]
    public bool OnServerDamageWithEffect(GameObject sourceOfDamage, short amount)
    {
        var damageData = new DirectDamageData(sourceOfDamage, DirectDamageCause.Effect, amount);
        return OnServerDamage(damageData);
    }

    /// <summary>
    /// Damages the player
    /// </summary>
    /// <param name="damageData"></param>
    /// <returns>True if the player dies because of the damage</returns>
    [Server]
    public bool OnServerDamage(DirectDamageData damageData)
    {
        //If you're not already dead
        if (health < 1) { return false; }
        health -= damageData.amount;
        if (health > 0)
        {
            return false;
        }
        OnServerDied();
        return true;
    }

    [Server]
    void OnServerDied()
    {
        //Respawn the player at a random location
    }
}


//----------


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
        OnServerGameStarted();
    }

    [Server]
    void OnServerGameStarted()
    {
        health = startingHealth > 0 ? startingHealth : MaxHealth;
    }

    [Server]
    public void OnServerHeal(short amount)
    {
        //If damage is actually relevant
        if (amount == 0) { return; }
        //If you're not already dead
        if (health < 1) { return; } 
        health = (short)Mathf.Clamp(health + amount, 1, MaxHealth);
    }

    [Server]
    public bool OnServerDamageWithEffect(GameObject sourceOfDamage, short amount)
    {
        var damageData = new DirectDamageData(sourceOfDamage, DirectDamageCause.Effect, amount);
        return OnServerDamage(damageData);
    }

    /// <summary>
    /// Damages the player
    /// </summary>
    /// <param name="damageData"></param>
    /// <returns>True if the player dies because of the damage</returns>
    [Server]
    public bool OnServerDamage(DirectDamageData damageData)
    {
        //If you're not already dead
        if (health < 1) { return false; }
        health -= damageData.amount;
        if (health > 0)
        {
            return false;
        }
        OnServerDied();
        return true;
    }

    [Server]
    void OnServerDied()
    {
        //Respawn the player at a random location
    }
}