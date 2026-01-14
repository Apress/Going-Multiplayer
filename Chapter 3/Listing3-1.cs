public class PlayerState : NetworkBehaviour 

{ 

    NetworkVariable<int> ammoLeftInWeapon = new NetworkVariable<int>(default, NetworkVariableReadPermission.Owner, NetworkVariableWritePermission.Server); 

} 