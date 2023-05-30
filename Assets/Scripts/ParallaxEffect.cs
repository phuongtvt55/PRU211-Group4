using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Starting position for the parallax game object
    Vector2 startingPosition;

    // Starting Z value of the parallax game object
    float startingZ;

    // Distance that the camera has moved from the starting position of the parallax effect
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition; 
    float zDistanceFromTarget => transform.position.z - followTarget.position.z;

    // If object is in front of target, use near clip plane. Else...
    float clipPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // The futher the object from the layer, the faster the ParallaxEffect object will move. Drag it's Z value closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clipPlane;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
