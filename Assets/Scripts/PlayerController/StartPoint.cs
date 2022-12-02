using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    private PlayerController player;
    private CameraController camera;

    public string pointName;

    void Start() {
        player = FindObjectOfType<PlayerController>();
        
        if (player.startPoint == pointName) {
            player.transform.position = transform.position;

            camera = FindObjectOfType<CameraController>();
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        }
    }
}
