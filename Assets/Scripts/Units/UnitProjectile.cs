using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UnitProjectile : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private int damageToDeal = 20;
    [SerializeField] private float LaunchForce = 10f;
    [SerializeField] private float destroyedAfterSeconds = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * LaunchForce;
    }

	public override void OnStartServer()
	{
        Invoke(nameof(DestroySelf), destroyedAfterSeconds);
	}

	[Server]
    private void DestroySelf()
	{
        NetworkServer.Destroy(gameObject);
	}

    [ServerCallback]
	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent<NetworkIdentity>(out NetworkIdentity networkIdentity))
		{
            if(networkIdentity.connectionToClient == connectionToClient) { return; }
		}
        if(other.TryGetComponent<Health>(out Health health))
		{
            health.DealDamage(damageToDeal);
		}
        DestroySelf();
	}


}
