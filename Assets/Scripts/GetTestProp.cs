using UnityEngine;
using TMPro;

public class GetTestProp : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display = default;
    [SerializeField]
    GameObject testObject = default;
    // Start is called before the first frame update
    void Start()
    {
        display.SetText("ERROR");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = testObject.GetComponent<MovingSphere>().velocity;
        display.SetText("Val\n{0:3}\n{1:3}\n{2:3}", dir.x, dir.y, dir.z);
    }
}
