using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameOverHandler : NetworkBehaviour
{
	private List<UnitBase> bases = new List<UnitBase>();

	#region Server

	public override void OnStartServer()
	{
		//base.OnStartServer();
		UnitBase.ServerOnBaseDespawned += ServerHandleBaseDespawned;
		UnitBase.ServerOnBaseSpawned += ServerHandleBaseSpawned;
	}

	public override void OnStopServer()
	{
		//base.OnStopServer();
		UnitBase.ServerOnBaseSpawned -= ServerHandleBaseSpawned;
		UnitBase.ServerOnBaseDespawned -= ServerHandleBaseDespawned;
	}

	[Server]
	private void ServerHandleBaseSpawned(UnitBase unitBase)
	{
		bases.Add(unitBase);
	}
	[Server]
	private void ServerHandleBaseDespawned(UnitBase unitBase)
	{
		bases.Remove(unitBase);

		if(bases.Count != 1) { return; }

		Debug.Log("GameOver");
	}

	#endregion

	#region Client
	#endregion
}
