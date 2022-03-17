using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string levelName;
    public void DestroyLevel()
    {
        Destroy(gameObject);
    }
}
