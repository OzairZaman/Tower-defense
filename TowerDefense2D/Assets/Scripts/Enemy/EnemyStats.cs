using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public float value;
    public float movementModifier;

    public WaveManager owner; // Who owns this enemy?

    //private void OnDestroy()
    //{
    //    owner.enemyCount--; // Remove me from enemy count
    //}
}
