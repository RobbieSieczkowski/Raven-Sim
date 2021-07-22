using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private bool isTendMode;
    private bool isFlockMode;
    private bool isDmgControlMode;

    private float waitTime;
    public float minWaitTime;
    public float maxWaitTime;
    private bool isWaiting;
    private bool doneWaiting;
    private bool rotateToDefault;

    [HideInInspector]
    public float hunger;
    [HideInInspector]
    public float stress;
    [HideInInspector]
    public float harmony;

    public ProgressBar HungerBar;
    public ProgressBar StressBar;
    public ProgressBar HarmonyBar;

    // Start is called before the first frame update
    void Start()
    {
        isTendMode = true;
        isFlockMode = false;
        isDmgControlMode = false;
        isWaiting = false;
        doneWaiting = false;
        rotateToDefault = false;
        
        hunger = 50f;
        stress = 50f;
        harmony = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        // Circulates between three modes over semi-random intervals of time

        if (isTendMode)
        {
            if (!isWaiting && !doneWaiting)
            {
                StartCoroutine(Waiter());
            }
            if (doneWaiting)
            {
                isTendMode = false;
                isFlockMode = true;
                doneWaiting = false;
                rotateToDefault = true;
            }
        } else if (isFlockMode)
        {
            if (!isWaiting && !doneWaiting)
            {
                StartCoroutine(Waiter());
            }
            if (doneWaiting)
            {
                isFlockMode = false;
                isDmgControlMode = true;
                doneWaiting = false;
            }
        } else if (isDmgControlMode)
        {
            if (!isWaiting && !doneWaiting)
            {
                StartCoroutine(Waiter());
            }
            if (doneWaiting)
            {
                isDmgControlMode = false;
                isTendMode = true;
                doneWaiting = false;
            }
        }

        // Rotates player raven back to default z-axis rotation
        if (rotateToDefault)
        {
            playerPrefab.transform.rotation = new Quaternion(0f, playerPrefab.transform.rotation.y, 0f, 0f);
            rotateToDefault = false;
            //player.transform.Rotate(0, 0, Mathf.Lerp(0f, player.transform.position.z, 2f * Time.deltaTime), Space.Self);
        }

        // Updates harmony meter
        
        harmony = 100f - (hunger / 2f) - (stress / 2f);

        HungerBar.BarValue = hunger;
        StressBar.BarValue = stress;
        HarmonyBar.BarValue = harmony;
    }

    IEnumerator Waiter()
    {
        isWaiting = true;

        // To be figured out: Changes waitTime variable in indirect relation to stress level

        waitTime = Random.Range(minWaitTime, maxWaitTime);
        Debug.Log("Waiting for " + waitTime + " seconds...");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waited for " + waitTime + " seconds.");
        isWaiting = false;
        doneWaiting = true;
    }

    public bool GetITM()
    {
        return isTendMode;
    }

    public bool GetIFM()
    {
        return isFlockMode;
    }
    
    public bool GetIDCM()
    {
        return isDmgControlMode;
    }
}
