using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

	PlayerMechControllerScript currentRobot;

	Vector3 cameraOffset = new Vector3(0, 10, -5);
	float camFollowSpeed = 4.8f;
	float camTurnTime = 0.5f;
	float curCamTurnSpeed = 0.0f;
	Vector3 camNextPos;
	Vector3 camNextRot;

	// Start is called before the first frame update
	void Start()
    {
		currentRobot = GameObject.Find("PlayerMech").GetComponent<PlayerMechControllerScript>();
		camNextPos = GameObject.Find("PlayerMech").transform.position;
	}

    // Update is called once per frame
    void LateUpdate()
    {
		TurnCamera();
	}

	void TurnCamera() {
		camNextRot = new Vector3(Camera.main.transform.eulerAngles.x, currentRobot.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
		camNextRot.y = Mathf.SmoothDampAngle(Camera.main.transform.eulerAngles.y, camNextRot.y, ref curCamTurnSpeed, camTurnTime);
		Camera.main.transform.eulerAngles = camNextRot;

		camNextPos = Vector3.MoveTowards(camNextPos, currentRobot.transform.position, camFollowSpeed * Time.deltaTime);
		Camera.main.transform.position = camNextPos + Vector3.up * cameraOffset.y + Vector3.right * cameraOffset.z * Mathf.Sin(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad) + Vector3.forward * cameraOffset.z * Mathf.Cos(Camera.main.transform.eulerAngles.y * Mathf.Deg2Rad);
	}

}
