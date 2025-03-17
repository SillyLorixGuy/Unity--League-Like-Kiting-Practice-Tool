using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//To Do:
//Fix Center Camera


public class CameraScript : MonoBehaviour
{
    GameObject player;

    //Locked Cam Vars
    public Transform target;
    public Vector3 offset = new Vector3(-6, 10, 0);
    public float smoothSpeed = 999f;

    private Quaternion fixedRotation = Quaternion.Euler(70f, 90f, 0f);

    //Unlocked Camera Vars
    public float camSpeed = 20;
    public float screenSizeThickness = 10;

    //Switch
    public bool camToggle = false;

    //Center Camera
    private bool isCameraLocked = false;

    void Start()
    {
        player = FindFirstObjectByType<Movement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CameraSwitch();
        }
        if (camToggle)
        {
            MoveUnlockedCamera();
        }
        else
        {
            MoveLockedCamera();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isCameraLocked)
        {
            CenterCameraOnPlayer();
        }
    }

    void MoveLockedCamera()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.rotation = fixedRotation;
    }

    void MoveUnlockedCamera()
    {
        Vector3 pos = transform.position;

        //Up
        if (Input.mousePosition.y <= Screen.height - screenSizeThickness)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }
        //Down
        if (Input.mousePosition.y >= -screenSizeThickness)
        {
            pos.x += camSpeed * Time.deltaTime;
        }
        //Left
        if (Input.mousePosition.x >= Screen.height - screenSizeThickness)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }
        //Right
        if (Input.mousePosition.x <= -screenSizeThickness)
        {
            pos.z += camSpeed * Time.deltaTime;
        }
        transform.position = pos;

    }

    void CameraSwitch()
    {
        if (camToggle)
        {
            camToggle = false;
        }
        else
        {
            camToggle = true;
        }
    }

    void CenterCameraOnPlayer()
    {
        Vector3 targetPosition = target.position + offset;
        StartCoroutine(SmoothMove(targetPosition));
    }

    System.Collections.IEnumerator SmoothMove(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < 0.3f) // Duration of smooth movement
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / 0.3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
