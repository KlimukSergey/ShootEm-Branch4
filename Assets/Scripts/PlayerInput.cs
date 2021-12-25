using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameManager manager;
    private ZoomCamera camera;
    public bool isAlive = true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        camera = FindObjectOfType<ZoomCamera>();
    }
    private void Update()
    {
        if (isAlive)
        {
            float x = Input.GetAxis(GameData.HORIZONTAL_AXIS);
            float y = Input.GetAxis(GameData.VERTICAL_AXIS);
            playerMovement.Move(x, y);

            float FOW = Input.GetAxis(GameData.CAMERAZOOM_AXIS);
            camera.Zoom(FOW); //  Cinemachine Field Of View
            if (Input.GetKeyDown(KeyCode.Escape))
                manager.EscapeMenu();
            if (Input.GetKeyDown(KeyCode.F))
                GetComponentInChildren<Shooting>().SuperShoot();
        }
    }
}
