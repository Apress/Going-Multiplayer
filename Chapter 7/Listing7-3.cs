using UnityEngine;

public class ColorManager : NetworkBehaviour
{
    NetworkVariable<Color32> m_NetworkedColor = new NetworkVariable<Color32>();
    [SerializeField]
    Material m_Material;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsClient)
        {
            //Manually load the initial Color to catch up with the state of the network variable.
            //Useful when re-connecting or hot-joining a session
            OnClientColorChanged(m_Material.color, m_NetworkedColor.Value);
            m_NetworkedColor.OnValueChanged += OnClientColorChanged;
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if (IsClient)
        {
            m_NetworkedColor.OnValueChanged -= OnClientColorChanged;
        }
    }


    void OnClientColorChanged(Color32 previousColor, Color32 newColor)
    {
        m_Material.color = newColor;
    }
}