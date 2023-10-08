using UnityEngine;
using System.Collections;

[System.Serializable]
public class BlendShape 
{
    [SerializeField]
    public string shapeName;

    [SerializeField]
    public int shapeIndex;

    [SerializeField]
    public float shapeValue;

    public BlendShape(string name, int index, float value = 0)
    {
        shapeName = name;
        shapeValue = value;
        shapeIndex = index;
    }

    public void Set(float value)
    {
        this.shapeValue = value;
        Debug.Log("Set " + shapeValue + " on " + shapeName);

    }
}
