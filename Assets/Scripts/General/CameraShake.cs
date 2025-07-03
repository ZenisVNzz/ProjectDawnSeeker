using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public ShakeData shakeData;

    public void ShakeCamera()
    {
        CameraShakerHandler.Shake(shakeData);
    }
}
