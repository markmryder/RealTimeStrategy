using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeSelected = null;
	[SerializeField] private UnitMovement unitMovement = null;

	public UnitMovement GetUnitMovement()
	{
		return unitMovement;
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

	#endregion

	#region Server
	#endregion
}
