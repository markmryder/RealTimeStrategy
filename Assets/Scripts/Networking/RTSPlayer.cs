using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField] private List<Unit> myUnits = new List<Unit>();


	#region Server
	public override void OnStartServer()
	{
		Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
		Unit.ServerOnUnitSpawned += ServerHandleUnitDespawned;
	}

	public override void OnStopServer()
	{
		Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
		Unit.ServerOnUnitSpawned -= ServerHandleUnitDespawned;
	}

	private void ServerHandleUnitSpawned(Unit unit)
	{
		if(unit.connectionToClient.connectionId != connectionToClient.connectionId) { return; }

		myUnits.Add(unit);
	}
	private void ServerHandleUnitDespawned(Unit unit)
	{
		if (unit.connectionToClient.connectionId != connectionToClient.connectionId) { return; }

		myUnits.Remove(unit);
	}

	#endregion

	#region Client
	public override void OnStartClient()
	{
		if(!isClientOnly) { return; }

		Unit.AuthorityOnUnitSpawned += AuthorityHandleUnitSpawned;
		Unit.AuthorityOnUnitSpawned += AuthorityHandleUnitDespawned;
	}
	public override void OnStopClient()
	{
		if (!isClientOnly) { return; }

		Unit.AuthorityOnUnitSpawned -= AuthorityHandleUnitSpawned;
		Unit.AuthorityOnUnitSpawned -= AuthorityHandleUnitDespawned;
	}
	private void AuthorityHandleUnitSpawned(Unit unit)
	{
		if(!hasAuthority) { return; }
		myUnits.Add(unit);
	}
	private void AuthorityHandleUnitDespawned(Unit unit)
	{
		if (!hasAuthority) { return; }
		myUnits.Remove(unit);
	}

	#endregion
}
