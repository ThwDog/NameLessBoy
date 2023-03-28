using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New", menuName = "Charecter/enemy", order = 1)]
public class StatusData : ScriptableObject
{
    public int id;
    public string charname;
    public float[] position = new float[2];
    public GameObject characterGameObject;
    //public CharecterType type;
    public float maxHealth;
    public float health;
    public int damage;

    
}
