using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStarGenerator : MonoBehaviour
{
    public GameObject starPrefab;

    public int numberOfStars;
    public float starSizeMax;
    public float starSizeMin;
    public float minAlpha;
    public float maxAlpha;

    // Start is called before the first frame update
    void Start()
    {
        GenerateStars();
    }

    private void GenerateStars()
    {
        float xmin, xmax, ymin, ymax;

        Vector2 viewPortDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        xmin = -viewPortDimensions.x;
        xmax = viewPortDimensions.x;
        ymin = -viewPortDimensions.y;
        ymax = viewPortDimensions.y;

        for (int i = 0; i < numberOfStars; i++)
        {
            CreateStar(Random.Range(xmin, xmax), Random.Range(ymin, ymax));
        }
    }

    private void CreateStar(float x, float y)
    {
        float size = Random.Range(starSizeMin, starSizeMax);
        float alpha = Random.Range(minAlpha, maxAlpha);
        GameObject star = Instantiate(starPrefab, new Vector2(x, y), transform.rotation, transform);
        star.transform.localScale = new Vector2(size, size);
        SpriteRenderer rend = star.GetComponent<SpriteRenderer>();
        rend.color = new Color(1, 1, 1, alpha);
    }
}
