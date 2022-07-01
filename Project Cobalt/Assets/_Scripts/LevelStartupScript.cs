using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MechConstructorScript))]
public class LevelStartupScript : MonoBehaviour
{

    [SerializeField] Vector3 playerStartPos = new Vector3();
    [SerializeField] float playerStartAngle = 0.0f;
    
    void Awake()
    {
        GetComponent<MechConstructorScript>().ConstructMech(playerStartPos, Quaternion.AngleAxis(playerStartAngle, Vector3.up));
    }

	void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(playerStartPos, Quaternion.AngleAxis(playerStartAngle, Vector3.up), Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one + Vector3.up);
        Gizmos.DrawRay(Vector3.zero, Vector3.forward);
	}

}
