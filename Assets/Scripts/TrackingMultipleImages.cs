using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Этот скрипт создавался для одновременного распознования и отслеживания нескольких изображений
/// БЫЛИ БАГИ, НЕ РАБОТАЛ ТАК, КАК ДОЛЖЕН. ПОКА ОТКАЗАЛСЯ ОТ НЕГО В ПОЛЬЗУ ПУЛЬТА С КУЧЕЙ КНОПОК
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackingMultipleImages : MonoBehaviour
{
    [SerializeField] private GameObject[] placeablePrefabs;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach(ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        if (placeablePrefabs != null)
        {
            string name = trackedImage.referenceImage.name;
            Vector3 position = trackedImage.transform.position;

            GameObject prefab = spawnedPrefabs[name];
            prefab.SetActive(true);
            prefab.transform.position = position;

            foreach (GameObject go in spawnedPrefabs.Values)
            {
                if(go.name != name)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
