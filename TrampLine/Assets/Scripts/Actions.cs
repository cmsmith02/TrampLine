using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
public class Actions : MonoBehaviour {
    public PostProcessVolume postProcessVolume;

    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float cooldownDuration = 2.0f;
    [SerializeField] private Slider slider;

    private Vignette vignette;
    private float nextReadyTime = 0f;

    public List<GameObject> tramps;

    private float defaultFixedDeltaTime;

    private void Awake()
    {
        defaultFixedDeltaTime = Time.fixedDeltaTime;
        postProcessVolume.profile.TryGetSettings(out vignette);
    }

    private void Update()
    {
        float remaining = nextReadyTime - Time.time;

        slider.value = Mathf.Clamp01(1f - remaining / cooldownDuration);
    }

    public void TriggerSlowMotion(float slowTimeScale, float inDuration, float activeDuration, float outDuration)
    {
        StartCoroutine(SlowMotionRoutine(slowTimeScale, inDuration, activeDuration, outDuration));
    }

    private IEnumerator SlowMotionRoutine(float targetScale, float inTime, float activeTime, float outTime)
    {
        float startScale = Time.timeScale;
        float elapsed = 0f;

        while (elapsed < inTime)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / inTime;
            Time.timeScale = Mathf.Lerp(startScale, targetScale, t);
            vignette.intensity.value = Mathf.Lerp(0f, 0.25f, t);
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
            yield return null;
        }
        Time.timeScale = targetScale;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;

        yield return new WaitForSecondsRealtime(activeTime);

        elapsed = 0f;
        startScale = Time.timeScale;
        while (elapsed < outTime)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / outTime;
            Time.timeScale = Mathf.Lerp(startScale, 1f, t);
            vignette.intensity.value = Mathf.Lerp(startScale, 0f, t);
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
            yield return null;
        }
        vignette.intensity.value = 0f;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
        nextReadyTime = Time.time + cooldownDuration;
    }
    void OnSpawnFlat() {
        Vector3 position = spawnPoint.position;
        Quaternion rotation = spawnPoint.rotation;
        GameObject prefab = Instantiate(prefabToSpawn, position, rotation);
        tramps.Add(prefab);

        if (tramps.Count > 5) {
            Destroy(tramps[0]);
            tramps.RemoveAt(0);
        }
    }
     void OnSpawnLeft() {
        Vector3 position = spawnPoint.position;
        Quaternion rotation = Quaternion.Euler(0, 0, 45);
        GameObject prefab = Instantiate(prefabToSpawn, position, rotation);
        tramps.Add(prefab);

        if (tramps.Count > 5) {
            Destroy(tramps[0]);
            tramps.RemoveAt(0);
        }
    }
     void OnSpawnRight() {
        Vector3 position = spawnPoint.position;
        Quaternion rotation = Quaternion.Euler(0, 0, -45);
        GameObject prefab = Instantiate(prefabToSpawn, position, rotation);
        tramps.Add(prefab);

        if (tramps.Count > 5) {
            Destroy(tramps[0]);
            tramps.RemoveAt(0);
        }
    }

    void OnSlowTimeAbility()
    {
        if (!(Time.time >= nextReadyTime)) return;
        TriggerSlowMotion(0.2f, 0.5f, 2.0f, 0.5f);
    }
}