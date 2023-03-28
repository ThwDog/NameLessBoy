using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterStatus : MonoBehaviour
{
    public StatusData type;

    private void Start()
    {
        type.health = type.maxHealth;
    }
}
