using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] private Sprite[] emptyTextures;
    [SerializeField] private Sprite mineTexture;

    private bool isMine = false;

    private SpriteRenderer spriteRenderer;

    public bool IsMine => isMine;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Choose mine
        isMine = (Random.value < 0.15);

        // Fills the grid
        int x = (int) this.transform.position.x;
        int y = (int) this.transform.position.y;
        var elements = Playfield.Elements;
        elements[x, y] = this;
        Playfield.Elements = elements;
    }

    // When the element is clicked
    private void OnMouseUpAsButton()
    {
        if (isMine)
        {
            Playfield.UncoverMines();
            print("You lose...");
        }
        else
        {
            // Checks ajdacents mines
            int x = (int) this.transform.position.x;
            int y = (int) this.transform.position.y;
            LoadTexture(Playfield.AdjacentMines(x, y));

            // Uncovers neighbors
            Playfield.FloodFillUncover(x, y, new bool[Playfield.Width, Playfield.Height]);

            if (Playfield.IsFinished())
            {
                print("You win");
            }
        }
    }

    public void LoadTexture(int adjacentCount)
    {
        spriteRenderer.sprite = (isMine ? mineTexture : emptyTextures[adjacentCount]);
    }

    public bool IsCovered()
    {
        return spriteRenderer.sprite.texture.name.Equals("Default");
    }
}