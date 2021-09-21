using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// этот скрипт удаляет всех потомков объекта на котором он размещён
public class DestroyScene : MonoBehaviour
{
    public void DestroySceneButton()
    {
        foreach (Transform child in transform) 
        {
            //if (child.gameObject.name != "BasePlanePrefab") // вроде бы он в этом проекте ничего не делает
            //{
                Destroy(child.gameObject);
            //}
        }
    }
}