using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab = default;
    [SerializeField, Range(10, 100)]
    int resolution = 10;
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
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            point.localPosition = position;
        }
    }
}
