using UnityEngine;
using DG.Tweening;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public int minCircleCount = 5;
    public int maxCircleCount = 10;
    public float spawnDuration = 0.5f;
    public float bounceHeight = 0.5f;

    private void Start()
    {
        SpawnCircles();
    }

    private void SpawnCircles()
    {
        int circleCount = Random.Range(minCircleCount, maxCircleCount + 1);

        for (int i = 0; i < circleCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject newCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);

            // Apply entrance animation using DOTween
            newCircle.transform.localScale = Vector3.zero;
            newCircle.transform.DOScale(Vector3.one, spawnDuration);

            // Apply bouncing animation using DOTween
            newCircle.transform.position += Vector3.up * bounceHeight; // Move circle up
            newCircle.transform.DOMoveY(spawnPosition.y, spawnDuration / 2f) // Bounce animation
                .SetEase(Ease.OutQuad) // You can adjust the easing function
                .OnComplete(() => newCircle.transform.DOPunchPosition(Vector3.down * bounceHeight, spawnDuration / 2f)); // Bounce down
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnRadius = circlePrefab.GetComponent<CircleCollider2D>().radius;

        // Define the bounds of the spawn area
        float minX = -5.0f; // Adjust these values based on your desired spawn area
        float maxX = 5.0f;
        float minY = -1.0f;
        float maxY = 4.0f;

        Vector3 spawnPosition;
        bool isValidPosition = false;

        do
        {
            spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

            isValidPosition = true;
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(spawnPosition, spawnRadius))
            {
                if (collider.CompareTag("Circle"))
                {
                    isValidPosition = false;
                    break;
                }
            }
        } while (!isValidPosition);

        return spawnPosition;
    }


}
