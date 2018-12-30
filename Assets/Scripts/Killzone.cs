using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Killzone : NetworkBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnpoints;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            RpcRespawn(other.gameObject);
    }

    [ClientRpc]
    private void RpcRespawn(GameObject player)
    {
        if (isLocalPlayer)
        {
            var connection = player.GetComponent<NetworkIdentity>().connectionToClient;
            var newPlayer = Instantiate(playerPrefab, spawnpoints[Random.Range(0, spawnpoints.Length)].position, Quaternion.identity);
            Destroy(player);
            NetworkServer.ReplacePlayerForConnection(connection, newPlayer, 0);
        }
            
    }

}
