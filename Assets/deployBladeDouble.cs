using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployBladeDouble : MonoBehaviour {
    public GameObject blade1;
    public GameObject blade2;
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
        localScaleX = blade1.transform.localScale.x;
        localScaleZ = blade1.transform.localScale.z;
        scaleMax = blade1.transform.localScale.y;
        scaleCurrent = scaleMax;
        extendDelta = scaleMax / extendSpeed;
        blade1.transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        blade2.transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        blade1.transform.localPosition = new Vector3(0, scaleCurrent, 0);
        blade2.transform.localPosition = new Vector3(0, -scaleCurrent, 0);
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
        blade1.transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        blade2.transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);
        blade1.transform.localPosition = new Vector3(0, scaleCurrent, 0);
        blade2.transform.localPosition = new Vector3(0, -scaleCurrent, 0);
        weaponActive = scaleCurrent > 0;

        if (weaponActive && !blade1.activeSelf) {
            blade1.SetActive(true);
            blade2.SetActive(true);
        }
        else if (!weaponActive && blade1.activeSelf) {
            blade1.SetActive(false);
            blade2.SetActive(false);
        }
    }
}