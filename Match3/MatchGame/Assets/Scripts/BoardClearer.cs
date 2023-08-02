using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Board))]
public class BoardClearer : MonoBehaviour
{
    public Board board;

    private void Awake()
    {
        board = GetComponent<Board>();
    }

    // clear the entire Board
    public void ClearBoard()
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                ClearPieceAt(i, j);

                if (board.particleManager != null)
                {
                    board.particleManager.ClearPieceFXAt(i, j);
                }
            }
        }
    }
    // clear the GamePiece at position (x,y) in the Board
    public void ClearPieceAt(int x, int y)
    {
        GamePiece pieceToClear = board.allGamePieces[x, y];

        if (pieceToClear != null)
        {
            board.allGamePieces[x, y] = null;
            Destroy(pieceToClear.gameObject);
        }

        //HighlightTileOff(x,y);
    }

    // clear a list of GamePieces (plus a potential sublist of GamePieces destroyed by bombs)
    public void ClearPieceAt(List<GamePiece> gamePieces, List<GamePiece> bombedPieces)
    {
        foreach (GamePiece piece in gamePieces)
        {
            if (piece != null)
            {
                // clear the GamePiece
                ClearPieceAt(piece.xIndex, piece.yIndex);

                // add a score bonus if we clear four or more pieces
                int bonus = 0;
                if (gamePieces.Count >= 4)
                {
                    bonus = 20;
                }

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ScorePoints(piece, board.scoreMultiplier, bonus);

                    TimeBonus timeBonus = piece.GetComponent<TimeBonus>();

                    if (timeBonus != null)
                    {
                        GameManager.Instance.AddTime(timeBonus.bonusValue);
                        //                        Debug.Log("BOARD Adding time bonus from " + piece.name + " of " + timeBonus.bonusValue + "seconds");
                    }

                    GameManager.Instance.UpdateCollectionGoals(piece);
                }

                // play particle effects for pieces...
                if (board.particleManager != null)
                {
                    // ... cleared by bombs
                    if (bombedPieces.Contains(piece))
                    {
                        board.particleManager.BombFXAt(piece.xIndex, piece.yIndex);
                    }
                    // ... cleared normally
                    else
                    {
                        board.particleManager.ClearPieceFXAt(piece.xIndex, piece.yIndex);
                    }
                }
            }
        }
    }




    // clear and refill one position of the Board (used by Booster)
    public void ClearAndRefillBoard(int x, int y)
    {
        if (board.boardQuery.IsWithinBounds(x, y))
        {
            GamePiece pieceToClear = board.allGamePieces[x, y];
            List<GamePiece> listOfOne = new List<GamePiece>();
            listOfOne.Add(pieceToClear);
            board.ClearAndRefillBoard(listOfOne);
        }
    }




}
