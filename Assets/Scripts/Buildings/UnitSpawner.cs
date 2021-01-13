using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;
using System;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
	[SerializeField] private GameObject unitPrefab = null;
	[SerializeField] private Transform unitSpawnPoint = null;
	[SerializeField] private Health health = null;

	#region Server

	public override void OnStartServer()
	{
		health.ServerOnDie += ServerHandleDie;
	}

	[Server]
	private void ServerHandleDie()
	{
		NetworkServer.Destroy(gameObject);
	}

	public override void OnStopServer()
	{
		health.ServerOnDie -= ServerHandleDie;
	}


	[Command]
	private void CmdSpawnUnit()
	{
		GameObject unitInstance = Instantiate(unitPrefab, unitSpawnPoint.position, unitSpawnPoint.rotation);
		NetworkServer.Spawn(unitInstance, connectionToClient);
	}
	#endregion

	#region Client

	public void OnPointerClick(PointerEventData eventData)
	{
		if(eventData.button != PointerEventData.InputButton.Left) { return; }
		if (!hasAuthority) { return; }
		CmdSpawnUnit();
	}

	#endregion
}
