using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDestination : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knight"))
        {
            Debug.Log("Stage Complete!");
            // Add logic for stage completion, e.g., load next level
        }
    }
}

