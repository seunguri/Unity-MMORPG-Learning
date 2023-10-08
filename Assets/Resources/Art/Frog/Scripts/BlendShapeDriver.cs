using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways()]
public class BlendShapeDriver : MonoBehaviour
{
    [SerializeField]
    public List<SkinnedMeshRenderer> meshes;

    //We need to make them manually so the animator can use them
    public BlendShape Afraid = new BlendShape("Afraid", 0);
    public BlendShape Angry = new BlendShape("Angry", 1);
    public BlendShape Blink_Half = new BlendShape("Blink_Half", 2);
    public BlendShape Blink = new BlendShape("Blink", 3);
    public BlendShape Concerned = new BlendShape("Concerned", 4);
    public BlendShape Confused = new BlendShape("Confused", 5);
    public BlendShape Excited = new BlendShape("Excited", 6);
    public BlendShape Focused = new BlendShape("Focused", 7);
    public BlendShape Happy = new BlendShape("Happy", 8);
    public BlendShape Sad = new BlendShape("Sad", 9);
    public BlendShape Satisfied = new BlendShape("Satisfied", 10);
    public BlendShape Surprised = new BlendShape("Surprised", 11);
    public BlendShape Twelve = new BlendShape("Twelve", 12);

    public bool submeshesUseRemappedIndexes;
    public bool isFrog;

    public List<BlendShape> shapes;
    public List<float> values
    {
        get
        {
            return new List<float>()
            {
                m_afraid,
                m_angry,
                m_blink_half,
                m_blink,
                m_concerned,
                m_confused,
                m_excited,
                m_focused,
                m_happy,
                m_sad,
                m_satisfied,
                m_surprised,
                m_twelve
            };
        }
    }

    public List<int> remappedIndexes = new List<int>(){
        0,
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        11,
        12
    };

    public List<string> valueNames
    {
        get
        {
            return new List<string>()
            {
                "m_afraid",
                "m_angry",
                "m_blink_half",
                "m_blink",
                "m_concerned",
                "m_confused",
                "m_excited",
                "m_focused",
                "m_happy",
                "m_sad",
                "m_satisfied",
                "m_surprised",
                "m_twelve"
            };
        }
    }

    [SerializeField]
    [Range(0, 1)]
    private float m_afraid;
    [SerializeField]
    private float m_angry;
    [SerializeField]
    private float m_blink_half;
    [SerializeField]
    private float m_blink;
    [SerializeField]
    private float m_concerned;
    [SerializeField]
    private float m_confused;
    [SerializeField]
    private float m_excited;
    [SerializeField]
    private float m_focused;
    [SerializeField]
    private float m_happy;
    [SerializeField]
    private float m_sad;
    [SerializeField]
    private float m_satisfied;
    [SerializeField]
    private float m_surprised;
    [SerializeField]
    private float m_twelve;
    [SerializeField]
    private float m_thirteen;


    private void Reset()
    {
        meshes = new List<SkinnedMeshRenderer>();
        shapes = new List<BlendShape>() {
                Afraid,
                Angry,
                Blink_Half,
                Blink,
                Concerned,
                Confused,
                Excited,
                Focused,
                Happy,
                Sad,
                Satisfied,
                Surprised,
                Twelve
            };
    }

    private void Update()
    {
        for (int i = 0; i < meshes.Count; i++)
        {
            SkinnedMeshRenderer mesh = meshes[i];

            foreach (BlendShape shape in shapes)
            {
                int shapeIndex = shape.shapeIndex;
                float value = values[shapeIndex];

                //Change the index
                if (i > 0 && submeshesUseRemappedIndexes)
                {
                    shapeIndex = remappedIndexes[shapeIndex];

                }
                if (shapeIndex == -1) { continue; }

                if (mesh.sharedMesh.blendShapeCount <= shapeIndex) { continue; }
                
                mesh.SetBlendShapeWeight(shapeIndex, value);
            }
        }

    }


}
