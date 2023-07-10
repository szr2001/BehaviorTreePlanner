using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class FpsReader : MonoBehaviour
    {
        private float deltaTime;
        private Text FpsText;
        private void Awake()
        {
            FpsText = gameObject.GetComponent<Text>();
            StartCoroutine(UpdateFps());
        }

        private IEnumerator UpdateFps()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
                float fps = 1.0f / deltaTime;
                FpsText.text = $"Fps: {Mathf.Ceil(fps)}";
            }
        }
    }
}
