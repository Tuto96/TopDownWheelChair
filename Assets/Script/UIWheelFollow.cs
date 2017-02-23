using UnityEngine;
using System.Collections;

public class UIWheelFollow : MonoBehaviour {
    public GameObject WheelchairWheel;

    private float _rotCurrent = 0;
    private float _rotPrevious = 0;
    // Update is called once per frame
    void Update () {
        _rotCurrent = WheelchairWheel.transform.localEulerAngles.x;
        transform.rotation = WheelchairWheel.transform.rotation;
	}
}
