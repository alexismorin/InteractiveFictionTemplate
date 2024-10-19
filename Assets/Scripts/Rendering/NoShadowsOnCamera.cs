using UnityEngine;

public class NoShadowsOnCamera : MonoBehaviour
{

    [SerializeField]
    bool disableShadows = true;
    [SerializeField]
    bool lowerShadowResolution = false;

    // Internals

    float storedShadowDistance;
    ShadowResolution storedShadowResolution;

    void Awake()
    {
        storedShadowDistance = QualitySettings.shadowDistance;
        storedShadowResolution = QualitySettings.shadowResolution;
    }


    void OnPreRender()
    {
        if (disableShadows)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
        }
        else
        {
            if (lowerShadowResolution)
            {
                QualitySettings.shadowResolution = ShadowResolution.Low;
            }
        }

    }

    void OnPostRender()
    {
        if (disableShadows)
        {
            QualitySettings.shadows = ShadowQuality.All;
        }
        else
        {
            if (lowerShadowResolution)
            {
                QualitySettings.shadowResolution = storedShadowResolution;
            }
        }


    }



}
