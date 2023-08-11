using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 0.5f;
    //private float panBorderThickness = 10;
    private float scrollSpeed = 5f;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GameOver)
        {
            if (Input.GetKey("w") && transform.position.z <= 35/*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/)
            {
                transform.Translate(Vector3.forward * panSpeed * panSpeed, Space.World);
            }
            if (Input.GetKey("s") && transform.position.z >= -35/*|| Input.mousePosition.y <= panBorderThickness*/)
            {
                transform.Translate(Vector3.back * panSpeed * panSpeed, Space.World);

            }
            if (Input.GetKey("a") && transform.position.x >= -5/*|| Input.mousePosition.y <= panBorderThickness*/)
            {
                transform.Translate(Vector3.left * panSpeed * panSpeed, Space.World);

            }
            if (Input.GetKey("d") && transform.position.x <= 40/*|| Input.mousePosition.x <= Screen.height - panBorderThickness*/)
            {
                transform.Translate(Vector3.right * panSpeed * panSpeed, Space.World);

            }
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Vector3 pos = transform.position;
            pos.y -= scroll * 1000f * Time.deltaTime * scrollSpeed;
            pos.y = Mathf.Clamp(pos.y, 25, 70);
            transform.position = pos;
        }
    }
}
