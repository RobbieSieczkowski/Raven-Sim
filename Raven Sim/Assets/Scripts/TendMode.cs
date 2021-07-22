using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TendMode : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerPrefab;

    public float forwardSpeed;
    public float strafeSpeed;
    public float hoverSpeed;
    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float activeHoverSpeed;
    private float speedMultiplier;

    public float forwardAcceleration;
    public float strafeAcceleration;
    public float hoverAcceleration;

    public float lookRotateSpeed;
    private Vector2 lookInput;
    private Vector2 screenCenter;
    private Vector2 mouseDistance;

    private float rollInput;
    public float rollSpeed;
    public float rollAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }
    
    void FixedUpdate()
    {
        if (GetComponent<ModeManager>().GetITM())
        {
            // Embody individual raven; fly freely along all axes w/ front of raven aimed where cursor moves
            // Search for raven that lost the fight; will have a red indicator to signify high arousal
            // Groom/comfort raven to bring stress levels down

            if (Input.GetKey("left shift"))
            {
                speedMultiplier = 1.66f;
            }
            else
            {
                speedMultiplier = 1f;
            }

            lookInput.x = Input.mousePosition.x;
            lookInput.y = Input.mousePosition.y;

            mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
            mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

            mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

            rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
            
            playerPrefab.transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed * speedMultiplier, forwardAcceleration * Time.deltaTime);
            activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed * speedMultiplier, strafeAcceleration * Time.deltaTime);
            activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed * speedMultiplier, hoverAcceleration * Time.deltaTime);

            playerPrefab.GetComponent<Rigidbody>().position += playerPrefab.transform.forward * activeForwardSpeed * Time.deltaTime;
            playerPrefab.GetComponent<Rigidbody>().position += playerPrefab.transform.right * activeStrafeSpeed * Time.deltaTime;
            playerPrefab.GetComponent<Rigidbody>().position += playerPrefab.transform.up * activeHoverSpeed * Time.deltaTime;

            // Updates hunger meter

            if (GetComponent<ModeManager>().hunger < 100)
            {
                GetComponent<ModeManager>().hunger += .01f;
            }

            // Updates stress meter

            if (GetComponent<ModeManager>().stress < 100)
            {
                GetComponent<ModeManager>().stress += .01f;
            }
        }
    }
}
