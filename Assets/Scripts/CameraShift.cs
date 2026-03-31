using Unity.Cinemachine;
using UnityEngine;


public class CameraShift : MonoBehaviour
{
    [SerializeField] CinemachineCamera playerCam;
    
    public void SwitchCameraTo(CinemachineCamera targetCam)
    {
        playerCam.Priority = 5;
        targetCam.Priority = 20;
    }
}
