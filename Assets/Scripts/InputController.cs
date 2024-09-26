using Unity.Burst.Intrinsics;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("- - - Настройки управления - - -")]
    [SerializeField] private Vector2 mouseSensivitySpawner;

    [HideInInspector] public Vector2 inputMouse;
    [HideInInspector] public bool drop;
    [HideInInspector] public bool aim;

    // Update is called once per frame
    void Update()
    {
        //inputMoveble = transform.rotation * new Vector3(0, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //inputMovebleLoc = new Vector3(0, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Drop Item
        drop = Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Mouse0);
        aim = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0);

        // Move Spawner
        inputMouse.x = Input.GetAxis("Mouse X") * mouseSensivitySpawner.x;
        inputMouse.y = Input.GetAxis("Mouse Y") * mouseSensivitySpawner.y;
    }
}
