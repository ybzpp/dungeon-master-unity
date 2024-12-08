using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform Rotation;
    public Camera Camera;
    public List<CameraRigPositionData> PositionDatas = new List<CameraRigPositionData>();

    private void Start()
    {
        SelectPosition("Base");
    }

    [ContextMenu("BakePosition")]
    public void BakePosition()
    {
        PositionDatas.Add(new CameraRigPositionData()
        {
            position = transform.position,
            rotation = Rotation.localRotation,
            distanceCamera = Camera.transform.localPosition.z,
            fov = Camera.fieldOfView
        });
    }

    public string TestSelectPositionId;
    [ContextMenu("SelectPosition")]
    public void SelectPosition()
    {
        SelectPosition(TestSelectPositionId);
    }

    public void SelectPosition(string id)
    {
        foreach (var item in PositionDatas)
        {
            if (item.id == id)
            {
                transform.position = item.position;
                transform.rotation = Quaternion.identity;
                Rotation.localPosition = Vector3.zero;
                Rotation.localRotation = item.rotation;
                Camera.transform.localPosition = new Vector3(0, 0, item.distanceCamera);
                Camera.transform.localEulerAngles = Vector3.zero;
                Camera.fieldOfView = item.fov;
            }
        }
    }

    [System.Serializable]
    public struct CameraRigPositionData
    {
        public string id;
        public Vector3 position;
        public Quaternion rotation;
        public float distanceCamera;
        public float fov;
    }
}

