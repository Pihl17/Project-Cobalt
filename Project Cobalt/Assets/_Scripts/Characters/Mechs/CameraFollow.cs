using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public enum CameraPosition { Default, Sky }
	static Vector3[] cameraOffsets = new Vector3[] { new Vector3(0, 10, -5), new Vector3(0, 15, -3.5f) };
	static float[] rotationOffsets = new float[] { 45, 70 };
	const float camTransitionTime = 1.5f;

	Transform followTarget;
	public CameraPosition cameraPosition = CameraPosition.Default;

	//const float camFollowSpeed = 4.8f;
	const float camFollowTime = 0.2f;
	const float camTurnTime = 0.4f;
	
	Vector3 curCamFollowVel = new Vector3();
	float curCamTurnSpeed = 0.0f;

	Vector3 currentCameraVel;
	float currentXRotationVel;
	float currentXRotation;

	Vector3 currentOffset;
	Vector3 camNextPos;
	Vector3 camNextRot;

	// Start is called before the first frame update
	void Start()
    {
		followTarget = transform.parent;
		transform.SetParent(null, true);
		camNextPos = followTarget.position;
		currentOffset = cameraOffsets[(int)cameraPosition];
		currentXRotation = rotationOffsets[(int)cameraPosition];
	}

    // Update is called once per frame
    void LateUpdate()
    {
		TurnCamera();
	}
	
	void TurnCamera() {
		if (!followTarget)
			return;

		currentXRotation = Mathf.SmoothDampAngle(currentXRotation, rotationOffsets[(int)cameraPosition], ref currentXRotationVel, camTransitionTime);
		currentOffset = Vector3.SmoothDamp(currentOffset, cameraOffsets[(int)cameraPosition], ref currentCameraVel, camTransitionTime);

		camNextRot = new Vector3(currentXRotation, followTarget.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
		camNextRot.y = Mathf.SmoothDampAngle(Camera.main.transform.eulerAngles.y, camNextRot.y, ref curCamTurnSpeed, camTurnTime);
		Camera.main.transform.eulerAngles = camNextRot;

		//camNextPos = Vector3.MoveTowards(camNextPos, followTarget.position, camFollowSpeed * Time.deltaTime);
		camNextPos = Vector3.SmoothDamp(camNextPos, followTarget.position, ref curCamFollowVel, camFollowTime);
		Camera.main.transform.position = camNextPos + Vector3.up * currentOffset.y + Vector3.right * currentOffset.z * Mathf.Sin(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad) + Vector3.forward * currentOffset.z * Mathf.Cos(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad);
	}

}
