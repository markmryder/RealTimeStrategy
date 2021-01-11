using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeSelected = null;


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
