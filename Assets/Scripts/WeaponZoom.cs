using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    Camera fpsCamera;
    [SerializeField] float zoomedInFOV = 30f;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float desiredFOV;
    [SerializeField] float zoomedInSensitivity = 1f;
    [SerializeField] float zoomedOutSensitivity = 2f;

    RigidbodyFirstPersonController fpsController;

    Animator animator;

    bool zoomedInToggle = false;
    // Start is called before the first frame update
    void Start()
    {
        fpsController = GameObject.Find("Player").GetComponent<RigidbodyFirstPersonController>();
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            //Debug.Log("Found animator " + animator.name);
        }
        else
        {
            Debug.Log("Couldn't find animator ");
        }
        desiredFOV = zoomedOutFOV;
        fpsCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        if (fpsCamera.fieldOfView != desiredFOV)
        {
            fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, desiredFOV, 0.06f);
        }
            if (Input.GetMouseButtonDown(1))
        {
            zoomedInToggle = !zoomedInToggle;
            ToggleZoom();
        }
    }

    public void ToggleZoom()
    {
        desiredFOV = zoomedInToggle ? zoomedInFOV : zoomedOutFOV;
        float desiredSensitivity = zoomedInToggle ? zoomedInSensitivity : zoomedOutSensitivity;
        animator.SetBool("zoom", zoomedInToggle);
        fpsController.mouseLook.XSensitivity = desiredSensitivity;
        fpsController.mouseLook.YSensitivity = desiredSensitivity;
    }

    public void SetZoomedInToggle(bool v)
    {
        zoomedInToggle = v;
    }
}