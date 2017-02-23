using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public float posSmoothTime = 0.3f;
    public float rotSmoothTime = 0.3f;
    public float cameraHeight = 5;
    public float cameraOffset = 10;
    public float cameraAngleFactor = 0.5f;

    private PlayerControl player;
    private Vector3 velocity = Vector3.zero;
    private Vector3 oldPosition = Vector3.zero;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }
    void Update()
    {
        float vel = player.GetSpeed().magnitude;
        // Desired new position, with an added height
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, cameraHeight - cameraOffset*vel, 0));
        // Smooth move the camera to the new position following the target (player)
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, posSmoothTime);

        // Match the player rotation to the camera rotation, without damping
        Quaternion targetRot = new Quaternion();
        targetRot.eulerAngles = new Vector3(90, target.eulerAngles.y - 90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSmoothTime * Time.deltaTime);
        oldPosition = target.transform.position;
    }
}