using UnityEngine;
using UnityEngine.UI;
using System;

public class Tile : MonoBehaviour
{
    private int gridX;
    private int gridY;
    private string tileType;
    private bool isSelected = false;
    private Action<Tile> onSelected;

    private Image tileImage;
    private Color originalColor;
    private Color selectedColor = new Color(1, 1, 0.5f, 1); // Yellow highlight

    public void Initialize(int x, int y, string type, Action<Tile> onSelectedCallback)
    {
        gridX = x;
        gridY = y;
        tileType = type;
        onSelected = onSelectedCallback;

        tileImage = GetComponent<Image>();
        originalColor = GetColorForType(type);
        tileImage.color = originalColor;

        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    private Color GetColorForType(string type)
    {
        return type switch
        {
            "Red" => Color.red,
            "Blue" => Color.blue,
            "Green" => Color.green,
            "Yellow" => Color.yellow,
            "Purple" => new Color(0.8f, 0, 1),
            "Orange" => new Color(1, 0.5f, 0),
            _ => Color.white
        };
    }

    private void OnClicked()
    {
        onSelected?.Invoke(this);
    }

    public void Select()
    {
        isSelected = true;
        tileImage.color = selectedColor;
        transform.localScale = new Vector3(1.1f, 1.1f, 1);
    }

    public void Deselect()
    {
        isSelected = false;
        tileImage.color = originalColor;
        transform.localScale = Vector3.one;
    }

    public void Remove()
    {
        GetComponent<Button>().interactable = false;
        Destroy(gameObject);
    }

    public void SetGridPosition(int x, int y)
    {
        gridX = x;
        gridY = y;
    }

    public int GetGridX() => gridX;
    public int GetGridY() => gridY;
    public string GetTileType() => tileType;
    public bool IsSelected() => isSelected;
}
