using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    public ARRaycastManager RayCastManager;
    public GameObject objectToCreate;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    const TrackableType trackableTypes =
    TrackableType.FeaturePoint |
    TrackableType.PlaneWithinPolygon;

    void Update()
    {
        if(RayCastManager != null) {
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
                return;
            }

            if (RayCastManager.Raycast(touch.position, hits))
            {
                var hit = hits[0];
                objectToCreate = GameObject.Instantiate(objectToCreate, hit.pose.position, hit.pose.rotation);
            }
        } else {
            RayCastManager = FindObjectOfType<ARRaycastManager>();
        }
    }
}
