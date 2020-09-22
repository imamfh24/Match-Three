using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 finalPosition;
    private float swipeAngle;
    private Vector3 tempPosition;
    // Menampung data posisi tile
    public float xPosition;
    public float yPosition;
    public int column;
    public int row;
    private int previousColumn, previousRow; // Menyimpan indeks kolom dan row yang sebelumnya dipilih
    private Grid grid;
    private GameObject otherTile;
    public bool isMatched = false;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        xPosition = transform.position.x;
        yPosition = transform.position.y;
        column = Mathf.RoundToInt((xPosition - grid.startPos.x) / grid.offset.x);
        row = Mathf.RoundToInt((yPosition - grid.startPos.y) / grid.offset.x);
    }

    private void Update()
    {
        CheckMates();
        xPosition = (column * grid.offset.x) + grid.startPos.x;
        yPosition = (row * grid.offset.y) + grid.startPos.y;
        SwipeTile();
    }

    void SwipeRightMove()
    {
        if(column + 1 < grid.gridSizeX)
        {
            //Menukar posisi tile dengan sebelah kanannya
            otherTile = grid.tiles[column + 1, row];
            otherTile.GetComponent<Tile>().column -= 1;
            column += 1;
        }
    }

    void SwipeUpMove()
    {
        if(row + 1 < grid.gridSizeY)
        {
            //Menukar posisi tile dengan sebelah atasnya
            otherTile = grid.tiles[column, row + 1];
            otherTile.GetComponent<Tile>().row -= 1;
            row += 1;
        }
    }

    void SwipeLeftMove()
    {
        if(column - 1 >= 0)
        {
            //Menukar posisi tile dengan sebelah kiri nya
            otherTile = grid.tiles[column - 1, row];
            otherTile.GetComponent<Tile>().column += 1;
            column -= 1;
        }
    }

    void SwipeDownMove()
    {
        if(row - 1 >= 0)
        {
            //Menukar posisi tile dengan sebelah bawahnya
            otherTile = grid.tiles[column, row - 1];
            otherTile.GetComponent<Tile>().row += 1;
            row -= 1;
        }
    }

    private void OnMouseDown()
    {
        //Mendapatkan titik awal sentuhan jari
        firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        //Mendapatkan titik akhir sentuhan jari
        finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        //Menghitung sudut antara posisi awal dan posisi akhir
        swipeAngle = Mathf.Atan2(finalPosition.y - firstPosition.y, finalPosition.x - firstPosition.x) * 180 / Mathf.PI;
        MoveTile();
    }

    private void MoveTile()
    {
        previousColumn = column;
        previousRow = row;

        if (swipeAngle > -45 && swipeAngle <= 45)
        {
            //Right Swipe, tambah method SwipeRight
            SwipeRightMove();
            Debug.Log("Right Swipe");
        }
        else if (swipeAngle > 45 && swipeAngle <= 135)
        {
            //Up Swipe, tambah method SwipeUp
            SwipeUpMove();
            Debug.Log("Up Swipe");
        }
        else if (swipeAngle > 135 || swipeAngle <= -135)
        {
            //Left Swipe, tambah method SwipeLeft
            SwipeLeftMove();
            Debug.Log("Left Swipe");
        }
        else if (swipeAngle < -45 && swipeAngle >= -135)
        {
            //Down Swipe, tambah method SwipeDown
            SwipeDownMove();
            Debug.Log("Down Swipe");
        }
        StartCoroutine(CheckMove());
    }

    void SwipeTile()
    {
        if(Mathf.Abs(xPosition - transform.position.x) > .1f)
        {
            //Move towards the target
            Vector3 tempPosition = new Vector2(xPosition, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        } else
        {
            //Directly set the position
            tempPosition = new Vector2(xPosition, transform.position.y);
            transform.position = tempPosition;
            grid.tiles[column, row] = gameObject;
        }

        if(Mathf.Abs(yPosition - transform.position.y) > .1f)
        {
            //Move towards the target
            Vector3 tempPosition = new Vector2(transform.position.x, yPosition);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        } else
        {
            //Directly set the position
            Vector3 tempPosition = new Vector2(transform.position.x, yPosition);
            transform.position = tempPosition;
            grid.tiles[column, row] = gameObject;
        }
    }

    private void CheckMates()
    {
        //Check horizontal matching
        if (column > 0 && column < grid.gridSizeX - 1)
        {
            //Check samping kiri dan kanan nya
            GameObject leftTile = grid.tiles[column - 1, row];
            GameObject rightTile = grid.tiles[column + 1, row];
            if (leftTile != null && rightTile != null)
            {
                if (leftTile.CompareTag(gameObject.tag) && rightTile.CompareTag(gameObject.tag))
                {
                    isMatched = true;
                    rightTile.GetComponent<Tile>().isMatched = true;
                    leftTile.GetComponent<Tile>().isMatched = true;
                }
            }
        }
        //Check vertical matching
        if (row > 0 && row < grid.gridSizeY - 1)
        {
            //Check samping atas dan bawahnya
            GameObject upTile = grid.tiles[column, row + 1];
            GameObject downTile = grid.tiles[column, row - 1];
            if (upTile != null && downTile != null)
            {
                if (upTile.CompareTag(gameObject.tag) && downTile.CompareTag(gameObject.tag))
                {
                    isMatched = true;
                    downTile.GetComponent<Tile>().isMatched = true;
                    upTile.GetComponent<Tile>().isMatched = true;
                }
            }
        }

        if (isMatched)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.grey;
        }
    }

    IEnumerator CheckMove()
    {
        yield return new WaitForSeconds(.5f);
        //Cek jika tile nya tidak sama kembalikan, jika ada yang sama panggil DestroyMatches
        if (otherTile != null)
        {
            if (!isMatched && !otherTile.GetComponent<Tile>().isMatched)
            {
                otherTile.GetComponent<Tile>().row = row;
                otherTile.GetComponent<Tile>().column = column;
                row = previousRow;
                column = previousColumn;
            }
            else
            {
                grid.DestroyMatches();
            }
        }
        otherTile = null;
    }
}
