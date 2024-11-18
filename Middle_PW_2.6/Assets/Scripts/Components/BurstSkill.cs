using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstSkill : MonoBehaviour, ISkill
{
    public float shootDelay = 2f;

    private float _shootTime = 0;

    public float _burst = 1f;

    public void ExecuteBurst()
    {
        if (Time.time < _shootTime + shootDelay) return;

        _shootTime = Time.time;

        var pos = transform.position;

        pos += new Vector3(0, 0, _burst);

        transform.position = pos;
    }
}
