using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    private Color _color;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Miner miner))
        {

        }
    }
}
