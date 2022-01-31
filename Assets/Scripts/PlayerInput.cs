using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Shooting shooting;
    private GameManager manager;
    
    private ZoomCamera _camera;
    public bool isAlive = true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        shooting = GetComponentInChildren<Shooting>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _camera = FindObjectOfType<ZoomCamera>();
    }
    private void Update()
    {
        if (isAlive)
        {
            float x = Input.GetAxis(GameData.HORIZONTAL_AXIS);
            float y = Input.GetAxis(GameData.VERTICAL_AXIS);
            playerMovement.Move(x, y);

            float FOW = Input.GetAxis(GameData.CAMERAZOOM_AXIS);
            _camera.Zoom(FOW); //  Cinemachine Field Of View

                // Pause
            if (Input.GetKeyDown(KeyCode.Escape))
                manager.EscapeMenu();

                // SuperShoot
            if(Input.GetAxis(GameData.SWEETBALL)!=0)
                shooting.SuperShoot();
 
               // AIM
            if(Input.GetAxis(GameData.AIM)!=0)
                playerMovement.AIM();

                // SHOOT
           // if(Input.GetAxis(GameData.SHOOT)!=0)
                

        }
    }
}
