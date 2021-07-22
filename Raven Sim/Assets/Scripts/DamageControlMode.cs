using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControlMode : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject fightCenter;
    public float speed;
    private float speedMultiplier;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ModeManager>().GetIDCM())
        {
            // Embody individual raven; fly in a revolution around a central axis, where a raven fight is occuring
            // Shield other ravens and "push" them away from the fight as they slowly gravitate closer around the fight

            if (Input.GetKey("left shift"))
            {
                speedMultiplier = 1.66f;
            }
            else
            {
                speedMultiplier = 1f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                playerPrefab.transform.RotateAround(fightCenter.transform.position, Vector3.up, -speed * speedMultiplier * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerPrefab.transform.RotateAround(fightCenter.transform.position, Vector3.up, speed * speedMultiplier * Time.deltaTime);
            }

            // Updates stress meter

            if (GetComponent<ModeManager>().stress < 100)
            {
                GetComponent<ModeManager>().stress += .01f;
            }
        }
        
        //stress = GetComponent<ModeManager>().stress;
    }

    // Initiates a fight between two ravens
    public void SpawnFight()
    {
        fightCenter.SetActive(true);
    }

    // Ends active raven fight
    public void EndFight()
    {
        fightCenter.SetActive(false);
    }
}
