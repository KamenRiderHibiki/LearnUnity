using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 使用 using static 引入静态类
using static UnityEngine.Mathf;
using static FunctionLibrary;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab = default;
    [SerializeField, Range(10, 100)]
    int resolution = 10;
    [SerializeField, Range(0, 1)]
    float function = 0;
    [SerializeField, Range(2, 10)]
    float width = 2;
    Transform[] points;
    void Awake() {
        var position = Vector3.zero;
        var step = width / resolution;
        var scale = Vector3.one * step;
        points = new Transform[resolution];
        for(int i = 0;i < resolution;i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.SetParent(transform, false);
            position.x = (i + 0.5f) * step - width / 2;
            position.y = position.x * position.x * position.x;
            point.localPosition = position;
            point.localScale = scale;
            points[i] = point;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        for(int i = 0; i < resolution; i++){
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = GetYValueByX(function, position.x, time);
            point.localPosition = position;
        }
    }

    // get Y value
    float GetYValueByX(float control, float x ,float time)
    {
        int floorControl = Floor(control);
        int conbineRate = control - floorControl;
        float y = 0;
        switch (floorControl)
        {
            case 0:
                y = Sin(Wave(x, time)) * (1.0f - conbineRate) + Sin(MultiWave(x, time)) * conbineRate;
                break;
            case 1:
                y = Sin(Wave(x, time)) * (1.0f - control) + Sin(MultiWave(x, time)) * control;
                break;
            default:
                float y = Sin(Wave(x, time)) * (1.0f - control) + Sin(MultiWave(x, time)) * control;
                break;
        }
        return y;
    }
}
