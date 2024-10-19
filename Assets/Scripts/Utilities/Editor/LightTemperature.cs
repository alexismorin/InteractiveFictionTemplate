using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Rendering;

public class LightTemperature {

    [MenuItem("Tools/Force Light Temperature Selection")]
    static void EnableLightTemperature() {
        GraphicsSettings.lightsUseLinearIntensity = true;
        GraphicsSettings.lightsUseColorTemperature = true;
    }

}
