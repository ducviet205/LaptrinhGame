using UnityEngine;

[System.Serializable]
public class Borders
{
    public float minXOffset = 1.5f;
    public float maxXOffset = 1.5f;
    public float minYOffset = 1.5f;
    public float maxYOffset = 1.5f;

    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour
{
    // Template dùng
    public static PlayerMoving instance;

    public Borders borders;
    public float moveSpeed = 30f;

    Camera cam;
    Rigidbody2D rb;

    Vector2 targetPos;
    bool isHolding;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        ResizeBorders();
        targetPos = rb.position;
    }

    private void Update()
    {
        isHolding = false;

        if (Input.GetMouseButton(0))
        {
            isHolding = true;
            targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (!isHolding)
        {
            targetPos = rb.position;
        }
    }

    private void FixedUpdate()
    {
        if (!isHolding) return;

        Vector2 nextPos = Vector2.MoveTowards(
            rb.position,
            targetPos,
            moveSpeed * Time.fixedDeltaTime
        );

        nextPos.x = Mathf.Clamp(nextPos.x, borders.minX, borders.maxX);
        nextPos.y = Mathf.Clamp(nextPos.y, borders.minY, borders.maxY);

        rb.MovePosition(nextPos);
    }

    void ResizeBorders()
    {
        borders.minX = cam.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = cam.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = cam.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = cam.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }
}
