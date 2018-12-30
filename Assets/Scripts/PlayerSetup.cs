using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisalbe;

    public string remoteLayerTag = "RemotePlayer";
    public string nickname;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }

        RegisterNewPlayer();
    }

    private void RegisterNewPlayer()
    {
        string playerNetworkID = $"{nickname}_{GetComponent<NetworkIdentity>().netId}";
        transform.name = playerNetworkID;
    }

    private void AssignRemoteLayer() => gameObject.layer = LayerMask.NameToLayer(remoteLayerTag);

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisalbe.Length; i++)
        {
            componentsToDisalbe[i].enabled = false;
        }
    }
}
