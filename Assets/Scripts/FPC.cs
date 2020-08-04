using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPC : MonoBehaviour
{
    public enum TypeFPC 
    {
        MouseAndKeyboard, 
        JoystickAndJoystick, 
        JoystickAndTouchPanel 
    }
    public TypeFPC typeControll;
    public CharacterController playerFPC;
    public Camera camera_PlayerFPC;

    public Joystick joystickMove;
    public Joystick joystickMouse;
    public FixedTouchField TouchFiledMouse;

    public GameObject Controll_Joysticks;
    public GameObject Controll_Touch;

    private float xMove;
    private float zMove;
    private Vector3 moveDirection;

    private float xRot;
    private float yRot;
    private float xRotCurrent;
    private float yRotCurrent;

    private float currentVelosityX;
    private float currentVelosityY;

    [SerializeField] float speedMove = 5f;
    [SerializeField] float sensetiveMouse = 0.2f;
    [SerializeField] float smoothTime = 0.1f;

    [SerializeField] float maxRotDown = 60f;
    [SerializeField] float maxRotUp = -60f;

    public RayCastHit scriptRayCastHit;

    private void Start()
    {
        playerFPC = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (typeControll == TypeFPC.JoystickAndTouchPanel)
        {
            MoveMobile();
            TouchPanelRotMobile();
            Controll_Joysticks.SetActive(false);
            Controll_Touch.SetActive(true);
            joystickMove.gameObject.SetActive(true);
            sensetiveMouse = 0.2f;
        }
        if (typeControll == TypeFPC.MouseAndKeyboard)
        {
            Move();
            MouseRot();
            Controll_Joysticks.SetActive(false);
            Controll_Touch.SetActive(false);
            joystickMove.gameObject.SetActive(false);
            sensetiveMouse = 4f;
        }
        if(typeControll == TypeFPC.JoystickAndJoystick)
        {
            MoveMobile();
            MouseRotMobile();
            Controll_Joysticks.SetActive(true);
            Controll_Touch.SetActive(false);
            joystickMove.gameObject.SetActive(true);
            sensetiveMouse = 3f;
        }



    }
    private void Move()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");

        moveDirection = new Vector3(xMove, 0f, zMove);
        moveDirection = transform.TransformDirection(moveDirection);

        playerFPC.Move(moveDirection * speedMove * Time.deltaTime);
    }

    private void MouseRot()
    {
        xRot += Input.GetAxis("Mouse X") * sensetiveMouse;
        yRot += Input.GetAxis("Mouse Y") * sensetiveMouse;
        yRot = Mathf.Clamp(yRot, maxRotUp, maxRotDown);

        xRotCurrent = Mathf.SmoothDamp(xRotCurrent, xRot, ref currentVelosityX, smoothTime * Time.deltaTime);
        yRotCurrent = Mathf.SmoothDamp(yRotCurrent, yRot, ref currentVelosityY, smoothTime * Time.deltaTime);

        camera_PlayerFPC.transform.rotation = Quaternion.Euler(-yRotCurrent, xRotCurrent, 0f);
        playerFPC.transform.rotation = Quaternion.Euler(0f, xRotCurrent, 0f);
    }

    private void MoveMobile()
    {
        xMove = joystickMove.Horizontal();
        zMove = joystickMove.Vertical();

        moveDirection = new Vector3(xMove, 0f, zMove);
        moveDirection = transform.TransformDirection(moveDirection);

        playerFPC.Move(moveDirection * speedMove * Time.deltaTime);
    }

    private void MouseRotMobile()
    {
        xRot += joystickMouse.HorizontalMouse() * sensetiveMouse;
        yRot += joystickMouse.VerticalMouse() * sensetiveMouse;
        yRot = Mathf.Clamp(yRot, maxRotUp, maxRotDown);

        camera_PlayerFPC.transform.rotation = Quaternion.Euler(-yRot, xRot, 0f);
        playerFPC.transform.rotation = Quaternion.Euler(0f, xRot, 0f);
    }

    private void TouchPanelRotMobile()
    {
        xRot += TouchFiledMouse.TouchDist.x * sensetiveMouse;
        yRot += TouchFiledMouse.TouchDist.y * sensetiveMouse;
        yRot = Mathf.Clamp(yRot, maxRotUp, maxRotDown);

        xRotCurrent = Mathf.SmoothDamp(xRotCurrent, xRot, ref currentVelosityX, smoothTime * Time.deltaTime);
        yRotCurrent = Mathf.SmoothDamp(yRotCurrent, yRot, ref currentVelosityY, smoothTime * Time.deltaTime);

        camera_PlayerFPC.transform.rotation = Quaternion.Euler(-yRotCurrent, xRotCurrent, 0f);
        playerFPC.transform.rotation = Quaternion.Euler(0f, xRotCurrent, 0f);
    }

}
