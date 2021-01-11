using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using System;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeSelected = null;
	[SerializeField] private UnitMovement unitMovement = null;
	[SerializeField] private Targeter targeter = null;

	public static event Action<Unit> ServerOnUnitSpawned;
	public static event Action<Unit> ServerOnUnitDespawned;

	public static event Action<Unit> AuthorityOnUnitSpawned;
	public static event Action<Unit> AuthorityOnUnitDespawned;



	public UnitMovement GetUnitMovement()
	{
		return unitMovement;
	}

	public Targeter GetTargeter()
	{
		return targeter;
	}


	#region Client
	[Client]
	public void Select()
	{
		if (!hasAuthority) { return; }
		onSelected?.Invoke();
	}
	[Client]
	public void Deselect()
	{
		if (!hasAuthority) { return; }
		onDeSelected?.Invoke();
	}

	public override void OnStartClient()
	{
		if (!hasAuthority || !isClientOnly) { return; }
		AuthorityOnUnitSpawned?.Invoke(this);
	}
	public override void OnStopClient()
	{
		if (!hasAuthority || isClientOnly) { return; }
		AuthorityOnUnitDespawned?.Invoke(this);
	}

	#endregion

	#region Server

	public override void OnStartServer()
	{
		ServerOnUnitSpawned?.Invoke(this);
	}

	public override void OnStopServer()
	{
		ServerOnUnitDespawned?.Invoke(this);	
	}

	#endregion
}
