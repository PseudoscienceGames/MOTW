using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
	public float zoom;
	public int zoomMin;
	public int zoomMax;
	public float zoomSpeed;
	public float cameraRotSpeed;
	public float camPanSpeed;
	public int scrollAreaSize;
	public bool screenEdgeScroll;

	public GameObject cameraPivot;

	public LayerMask mask;
	public Vector3 oldPos;

	public float smoothTime;
	private Vector3 velocity = Vector3.zero;

	public static CameraControl Instance;
	void Awake() { Instance = this; }

	void Update()
	{
		if (Input.GetAxis("Vertical") != 0)
			transform.position += transform.forward * Input.GetAxisRaw("Vertical") * camPanSpeed;
		if (Input.GetAxis("Horizontal") != 0)
			transform.position += transform.right * Input.GetAxisRaw("Horizontal") * camPanSpeed;
		//Pivots camera based on mouse movement
		if (Input.GetMouseButton(1))
		{
			transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraRotSpeed * Time.deltaTime, Space.Self);
			cameraPivot.transform.Rotate(-Vector3.right, Input.GetAxis("Mouse Y") * cameraRotSpeed * Time.deltaTime);
		}
		else if (screenEdgeScroll)
		{
			if (Input.mousePosition.x < scrollAreaSize)
				transform.position -= transform.right * camPanSpeed;
			if (Input.mousePosition.x > Camera.main.pixelWidth - scrollAreaSize)
				transform.position += transform.right * camPanSpeed;
			if (Input.mousePosition.y < scrollAreaSize)
				transform.position -= transform.forward * camPanSpeed;
			if (Input.mousePosition.y > Camera.main.pixelHeight - scrollAreaSize)
				transform.position += transform.forward * camPanSpeed;
		}

		//Zoom camera
		zoom = -Camera.main.transform.localPosition.z;
		if (zoom > zoomMin && zoom < zoomMax)
		{
			if (Camera.main.orthographic)
				Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
			else
			{
				zoom += -(Input.GetAxisRaw("Mouse ScrollWheel")) * zoomSpeed * zoom;
				if (zoom > zoomMin && zoom < zoomMax)
				{
					Camera.main.transform.localPosition = new Vector3(0, 0, -zoom);
				}
			}
		}
		if (Input.GetMouseButtonDown(2))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, mask))
				oldPos = hit.point;
		}
	}

	private void LateUpdate()
	{
		if (Input.GetMouseButton(2))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, mask))
			{
				Vector3 targetPosition = transform.position + (oldPos - hit.point);
				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
			}
		}
	}

	public void FocusCamera(GridLoc gridLoc)
	{
		StopAllCoroutines();
		StartCoroutine(SmoothFocus(gridLoc));
	}

	IEnumerator SmoothFocus(GridLoc gridLoc)
	{
		Vector3 initPos = transform.position;
		Vector3 targetPos = gridLoc.ToWorld();

		float timer = 0;
		while (timer <= 1)
		{
			timer += Time.deltaTime;
			transform.position = Vector3.Lerp(initPos, targetPos, timer * 10f);
			yield return null;
		}
	}
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraControl : MonoBehaviour
//{
//	public float camMoveSpeed = 10;
//	public float camRotSpeed = 150;
//	public float camZoomSpeed = 10;
//	Vector3 camMove;
//	Vector3 camRot;
//	public LayerMask mask;
//	public Vector3 oldPos;
//	public Transform cameraPivot;
//	public float xRot;
//	public float yRot;

//	public float smoothTime;
//	private Vector3 velocity = Vector3.zero;

//	private void Start()
//	{
//		cameraPivot = Camera.main.transform.parent;
//	}

//	private void Update()
//	{
//		if (Input.GetAxis("Mouse ScrollWheel") != 0)
//			Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * camZoomSpeed);

//		if (Input.GetButtonDown("HeightUp"))
//			transform.position += new Vector3(0, 1, 0);
//		if (Input.GetButtonDown("HeightDown"))
//			transform.position -= new Vector3(0, 1, 0);
//		camMove = new Vector3(Input.GetAxis("CamMovX"), 0, Input.GetAxis("CamMovZ"));
//		if (Input.GetMouseButton(1))
//		{
//			yRot = Input.GetAxisRaw("Mouse X") * camRotSpeed;
//			//xRot = Input.GetAxisRaw("Mouse Y") * camRotSpeed;
//		}
//		else
//		{
//			yRot = 0;
//			xRot = 0;
//		}
//		if (Input.GetMouseButtonDown(2))
//		{
//			RaycastHit hit;
//			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, mask))
//				oldPos = hit.point;
//		}

//	}

//	void LateUpdate ()
//	{
//		transform.Rotate(Vector3.up, yRot * Time.deltaTime, Space.Self);
//		cameraPivot.Rotate(-Vector3.right, xRot * Time.deltaTime);

//		if (camRot.y != 0)
//			transform.Rotate(camRot, camRotSpeed * Time.deltaTime);

//		if(camMove.x != 0 || camMove.z != 0)
//			transform.Translate(camMove * camMoveSpeed * Time.deltaTime, Space.Self);
//		if (Input.GetMouseButton(2))
//		{
//			RaycastHit hit;
//			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, mask))
//			{
//				Vector3 targetPosition = transform.position + (oldPos - hit.point);
//				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
//			}
//		}
//	}
//}
