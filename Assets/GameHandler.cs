using System;
using System.Collections;
using System.Collections.Generic;
using Kurenaiz.Utilities.Types;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    private readonly Safe2DArray<Field> _squares = new Safe2DArray<Field> (5, 5);

    private void Start ()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                var field = new Field (transform.GetChild ((i * 5) + j).GetComponent<SpriteRenderer> (), false);
                _squares[i, j] = field;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown (0))
        {
            var editing = Input.GetKey (KeyCode.LeftShift);
            var mousePos = Input.mousePosition;
            var x = Mathf.FloorToInt ((mousePos.x * 5) / Screen.width);
            var y = Mathf.FloorToInt ((mousePos.y * 5) / Screen.height);
            if (_squares.WithinBounds (x, y))
                if (editing)
                    ToggleEditing (x, y);
                else
                    Toggle (x, y);
        }
    }

    void ToggleEditing (int x, int y)
    {
        var current = _squares[y, x];
        current.Toggle ();
        current.Renderer.color = current.On ? onColor : offColor;
    }

    void Toggle (int x, int y)
    {
        Field current;
        foreach (var field in GetAdjacents (x, y, out current))
        {
            if (field != null)
            {
                field.Toggle ();
                field.Renderer.color = field.On ? onColor : offColor;
            }
        }

        current.Toggle ();
        current.Renderer.color = current.On ? onColor : offColor;
    }

    Field[] GetAdjacents (int x, int y, out Field current)
    {
        Field[] adjacents = new Field[4];
        adjacents[0] = _squares[y + 1, x];
        adjacents[1] = _squares[y, x + 1];
        adjacents[2] = _squares[y - 1, x];
        adjacents[3] = _squares[y, x - 1];
        current = _squares[y, x];
        return adjacents;
    }
}

[RequireComponent (typeof(SpriteRenderer))]
class Field
{
    public bool On;
    public readonly SpriteRenderer Renderer;

    public void Toggle ()
    {
        On = !On;
    }

    public Field (SpriteRenderer renderer, bool @on)
    {
        this.On = @on;
        this.Renderer = renderer;
    }
}