using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployBlade : MonoBehaviour
{
    public GameObject blade;
    public AudioSource audio;

    public float extendSpeed = 0.1f;
    private bool weaponActive = true;
    private float scaleMin = 0f;
    private float scaleMax;
    private float extendDelta;
    private float scaleCurrent;
    private float localScaleX;
    private float localScaleZ;

    void Start() {
        localScaleX = blade.transform.localScale.x;
        localScaleZ = blade.transform.localScale.z;
        scaleMax = blade.transform.localScale.y;
        scaleCurrent = scaleMax;
        extendDelta = scaleMax / extendSpeed;
        blade.transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        blade.transform.localPosition = new Vector3(0, scaleCurrent, 0);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!weaponActive) {
                audio.Play();
            }
            extendDelta = weaponActive ? -Mathf.Abs(extendDelta) : Mathf.Abs(extendDelta);
        }

        scaleCurrent += extendDelta * Time.deltaTime;
        scaleCurrent = Mathf.Clamp(scaleCurrent, scaleMin, scaleMax);
        blade.transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        blade.transform.localPosition = new Vector3(0, scaleCurrent, 0);
        weaponActive = scaleCurrent > 0;

        if (weaponActive && !blade.activeSelf) {
            blade.SetActive(true);
        }
        else if (!weaponActive && blade.activeSelf) {
            blade.SetActive(false);
        }
    }
}