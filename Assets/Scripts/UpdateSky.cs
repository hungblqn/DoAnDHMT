using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UpdateSky : MonoBehaviour
{
    ReflectionProbe baker;

    void Start()
    {
        baker = gameObject.AddComponent<ReflectionProbe>();
        baker.cullingMask = 0;
        baker.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
        baker.mode = ReflectionProbeMode.Realtime;
        baker.timeSlicingMode = ReflectionProbeTimeSlicingMode.NoTimeSlicing;

        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
        StartCoroutine(UpdateEnvironment());
    }

    IEnumerator UpdateEnvironment()
    {
        DynamicGI.UpdateEnvironment();
        baker.RenderProbe();
        yield return new WaitForEndOfFrame();
        RenderSettings.customReflection = baker.texture;
    }
}
