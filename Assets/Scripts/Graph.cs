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
    [SerializeField, Range(2, 10)]
    float width = 2;
    [SerializeField]
    FunctionLibrary.FunctionNames function = default;
    [SerializeField, Range(0, 2)]
    float mixhandle = 0;
    Transform[] points;
    void Awake() {
        var position = Vector3.zero;
        var step = width / resolution;
        var scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for(int i = 0, k = 0;k < resolution;k++){
            position.z = (k + 0.5f) * step - width / 2;
            for(int j = 0;j < resolution;j++,i++){
                Transform point = Instantiate(pointPrefab);
                position.x = (j + 0.5f) * step - width / 2;
                position.y = position.x * position.x;
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
                points[i] = point;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        FunctionLibrary.control = mixhandle;

        float time = Time.time;
        for(int i = 0; i < points.Length; i++){
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, position.z, time);
            point.localPosition = position;
        }
    }

}
