using Cinemachine;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject duckCharacter;
    public GameObject catCharacter;
    public GameObject mouseCharacter;

    public GameObject minimap;
    public GameObject playerFollowCamera;
    public GameObject MainCamera;
    public GameObject DuckCam;
    public GameObject CatCam;
    public GameObject MouseCam;


    public GameObject currentCharacter;
    public Quaternion currentCamRotation;
    private Vector3 characterStartPosition; // Store the character's starting position
    private Quaternion characterStartRotation; // Store the character's starting rotation

    private MapFollowCam followScript;
    private CinemachineVirtualCamera virtualCamera;
   

    private void Start()
    {
        // Initially, set the Duck as the main character

        SwitchToDuck();

    }

    public void SwitchToDuck()
    {
        if (currentCharacter != null)
        {
            // Save the current character's position and rotation before deactivating it
            characterStartPosition = currentCharacter.transform.position;
            characterStartRotation = currentCharacter.transform.rotation;

            currentCharacter.SetActive(false);
            // Activate DuckCam and CatCam after setting up their positions and rotations

            Debug.Log("DuckNormal");
        }
        else
        {
            characterStartPosition = new Vector3(0, (float)0.1, -11);
            characterStartRotation = Quaternion.identity; // Set the default rotation if no current character
            currentCamRotation = Quaternion.identity;
            Debug.Log("DuckElse");
        }

        // Synchronize the positions and rotations of DuckCam and CatCam

        duckCharacter.transform.position = characterStartPosition;
        duckCharacter.transform.rotation = characterStartRotation; // Apply the saved rotation

        duckCharacter.SetActive(true);
        currentCharacter = duckCharacter;
      
        UpdateCameraSettings(DuckCam.transform);
    }

    public void SwitchToCat()
    {
        if (currentCharacter != null)
        {
            // Save the current character's position and rotation before deactivating it
            characterStartPosition = currentCharacter.transform.position;
            characterStartRotation = currentCharacter.transform.rotation;

            currentCharacter.SetActive(false);
            // Synchronize the positions and rotations of DuckCam and CatCam

            Debug.Log("CatNormal");
        }
        else
        {
            characterStartPosition = new Vector3(0, (float)0.1, -11);
            characterStartRotation = Quaternion.identity; // Set the default rotation if no current character
            currentCamRotation = Quaternion.identity;
            Debug.Log("CatElse");
        }

        catCharacter.transform.position = characterStartPosition;
        catCharacter.transform.rotation = characterStartRotation;

        catCharacter.SetActive(true);
        currentCharacter = catCharacter;
 
        // Update MapFollowCam and CinemachineVirtualCamera settings
        UpdateCameraSettings(CatCam.transform);

        Debug.Log(currentCamRotation.eulerAngles);
    }

    public void SwitchToMouse()
    {
        if (currentCharacter != null)
        {
            // Save the current character's position and rotation before deactivating it
            characterStartPosition = currentCharacter.transform.position;
            characterStartRotation = currentCharacter.transform.rotation;

            currentCharacter.SetActive(false);
            // Synchronize the positions and rotations of DuckCam and CatCam

            Debug.Log("MouseNormal");
        }
        else
        {
            characterStartPosition = new Vector3(0, (float)0.1, -11);
            characterStartRotation = Quaternion.identity; // Set the default rotation if no current character
            currentCamRotation = Quaternion.identity;
            Debug.Log("MouseElse");
        }

        mouseCharacter.transform.position = characterStartPosition;
        mouseCharacter.transform.rotation = characterStartRotation;

        mouseCharacter.SetActive(true);
        currentCharacter = mouseCharacter;

        UpdateCameraSettings(MouseCam.transform);

        Debug.Log(currentCamRotation.eulerAngles);
    }

    private void UpdateCameraSettings(Transform followTransform)
    {
        followScript = minimap.GetComponent<MapFollowCam>();
        if (followScript != null)
        {
            followScript.player = followTransform;
        }

        virtualCamera = playerFollowCamera.GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            virtualCamera.Follow = followTransform;
        }
    }

    
}