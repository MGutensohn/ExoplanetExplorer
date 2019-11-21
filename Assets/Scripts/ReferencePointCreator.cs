using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARReferencePointManager))]
[RequireComponent(typeof(ARRaycastManager))]
public class ReferencePointCreator : MonoBehaviour
{
    public GameObject pointerPrefab;
    public Button placeReferenceButton;
    public Button toggleButton;
    public Button searchButton;
    public InputField searchInput;
    private Camera cam;
    private GameObject pointer;
    private Pose hitPose;
    public void RemoveAllReferencePoints()
    {
        foreach (var referencePoint in m_ReferencePoints)
        {
            m_ReferencePointManager.RemoveReferencePoint(referencePoint);
        }
        m_ReferencePoints.Clear();
    }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_ReferencePointManager = GetComponent<ARReferencePointManager>();
        m_ReferencePoints = new List<ARReferencePoint>();
    }

    public void CreatReferencePoint()
    {
        Debug.Log("ReferencePoints: " + m_ReferencePoints.Count);
        if (m_ReferencePoints.Count > 0)
            return;

        var referencePoint = m_ReferencePointManager.AddReferencePoint(hitPose);
        if (referencePoint == null)
        {
            Debug.Log("Error creating reference point");
        }
        else
        {
            m_ReferencePoints.Add(referencePoint);
            placeReferenceButton.gameObject.SetActive(false);

            toggleButton.gameObject.SetActive(true);
            searchInput.gameObject.SetActive(true);
            searchButton.gameObject.SetActive(true);

        }
    }

    void Start()
    {
        cam = Camera.main;
        pointer = Instantiate(pointerPrefab);
    }


    void Update()
    {
        if (m_ReferencePoints.Count == 0)
        {
            if (m_RaycastManager.Raycast(new Ray(cam.transform.position, cam.transform.forward), s_Hits, TrackableType.FeaturePoint))
            {
                hitPose = s_Hits[0].pose;
                pointer.transform.position = hitPose.position;
            }
        }
        else if (pointer != null)
        {
            Destroy(pointer);
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    List<ARReferencePoint> m_ReferencePoints;

    ARRaycastManager m_RaycastManager;

    ARReferencePointManager m_ReferencePointManager;
}
