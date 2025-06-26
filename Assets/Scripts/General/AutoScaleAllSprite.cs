using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoScaleAllSprite : MonoBehaviour
{
    void Start()
    {
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ApplyScale();
        }

        void ApplyScale()
        {
            var sprites = FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None);

            float worldScreenHeight = Camera.main.orthographicSize * 2f;
            float worldScreenWidth = worldScreenHeight * Screen.width / (float)Screen.height;

            foreach (var sr in sprites)
            {
                Vector3 scale = new Vector3(
                    worldScreenWidth / sr.bounds.size.x,
                    worldScreenHeight / sr.bounds.size.y,
                    1);

                sr.transform.localScale = scale;
            }
        }
    }
}
