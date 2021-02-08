using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject bridge;
    [SerializeField] private Material defaultPressurePlate;
    [SerializeField] private Material activePressurePlate;

    private MeshRenderer pressurePlateMesh;

    private List<Collider> objectsOnPlate = new List<Collider>();

    private void Start()
    {
        pressurePlateMesh = transform.parent.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsOnPlate.Contains(other))
        {
            objectsOnPlate.Add(other);
            if (!bridge.activeSelf)
            {
                bridge.SetActive(true);
                pressurePlateMesh.material = activePressurePlate;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsOnPlate.Contains(other))
        {
            objectsOnPlate.Remove(other);
            if(objectsOnPlate.Count == 0)
            {
                bridge.SetActive(false);
                pressurePlateMesh.material = defaultPressurePlate;
            }
        }
    }
}
