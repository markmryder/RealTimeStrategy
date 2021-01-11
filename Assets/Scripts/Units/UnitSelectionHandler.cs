using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System;

public class UnitSelectionHandler : MonoBehaviour
{
    private Camera mainCamera;

	private List<Unit> selectedUnits = new List<Unit>();

	[SerializeField] private LayerMask layerMask = new LayerMask();


	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void Update()
	{
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			//start selection area
			foreach (Unit selectedUnit in selectedUnits)
			{
				selectedUnit.Deselect();
			}
			selectedUnits.Clear();
		}
		else if (Mouse.current.leftButton.wasReleasedThisFrame)
		{
			ClearSelectionArea();
		}
	}

	private void ClearSelectionArea()
	{
		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

		if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

		if(!hit.collider.TryGetComponent<Unit>(out Unit unit)) { return; }

		if (!unit.hasAuthority) { return; }

		selectedUnits.Add(unit);

		foreach(Unit selectedUnit in selectedUnits)
		{
			selectedUnit.Select();
		}

	}
}
