using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : Subject
{
    // Nama dari point of interest
    [SerializeField] private string pointName;

    // Jika gameobject di disable akan menotify Observernya
    private void OnDisable()
    {
        Notify(pointName);
    }
}
