using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControllers : MonoBehaviour {

    [SerializeField] private NTM_NetworkServer NTM_NetworkServer_ref;

    public float AxisY;

	void Start ()
    {
        NTM_NetworkServer_ref.ReciveControllerData_event += ReciveControllerData;
	}
	
	private void ReciveControllerData(float AxisY, bool Throttle)
    {
        this.AxisY = AxisY;
    }
}
