using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    public float offsetY;

    //让摄像头跟随Player移动
    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offsetY, transform.position.z);
    }
}
