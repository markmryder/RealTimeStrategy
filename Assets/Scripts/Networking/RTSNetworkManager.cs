using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class RTSNetworkManager : NetworkManager
{
	[SerializeField] private GameObject unitSpawnerPrefab = null;
	[SerializeField] private GameOverHandler gameOverHandlerPrefab = null;

	public override void OnServerAddPlayer(NetworkConnection conn)
	{
		base.OnServerAddPlayer(conn);

		GameObject unitSpawnerInstance = Instantiate(unitSpawnerPrefab, conn.identity.transform.position, conn.identity.transform.rotation);
		NetworkServer.Spawn(unitSpawnerInstance, conn);
	}

	public override void OnServerSceneChanged(string sceneName)
	{
		if (SceneManager.GetActiveScene().name.StartsWith("Scene_Map"))
		{
			GameOverHandler gameOverHandlerInstance = Instantiate(gameOverHandlerPrefab);

			NetworkServer.Spawn(gameOverHandlerInstance.gameObject);
		}
	}
}
