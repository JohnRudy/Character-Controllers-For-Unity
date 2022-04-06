using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPCameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera cam;
    [SerializeField] private float camFollowDelta;

    private Vector3 camOffset;

    

    private void Awake ( ) {
        if ( target == null ) target = transform;
        if (cam == null) cam = Camera.main;

        camOffset = cam.transform.position - target.position;
    }

    private void LateUpdate ( ) => cam.transform.position = Vector3.Lerp ( cam.transform.position , target.position + camOffset , camFollowDelta * Time.deltaTime );
}
