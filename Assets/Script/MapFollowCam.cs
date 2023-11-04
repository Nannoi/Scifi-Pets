using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFollowCam : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void LateUpdate ()
    {
        Vector3 newPosition = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

    
}
