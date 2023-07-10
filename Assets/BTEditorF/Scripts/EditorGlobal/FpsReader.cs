using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class FpsReader : MonoBehaviour
    {
        private float deltaTime;
        private Text FpsText;
        private void Start()
        {
            FpsText = gameObject.GetComponent<Text>();
        }
        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            FpsText.text = Mathf.Ceil(fps).ToString();
        }
    }
}
