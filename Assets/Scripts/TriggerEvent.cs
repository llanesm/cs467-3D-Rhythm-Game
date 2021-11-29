using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameController gamecontroller;

    private void Start()
    {
        gamecontroller = GameObject.FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            gamecontroller.CurrNode = other.gameObject;
            IncHitPrecision(gamecontroller.CurrNode);
            //Debug.Log("NE: " + gamecontroller.HitPrecision_NE);
            //Debug.Log("NW: " + gamecontroller.HitPrecision_NW);
            //Debug.Log("SE: " + gamecontroller.HitPrecision_SE);
            //Debug.Log("SW: " + gamecontroller.HitPrecision_SW);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            DecHitPrecision(gamecontroller.CurrNode);
            // Debug.Log(gamecontroller.HitPrecision);
            if (gamecontroller.CurrNode.name == "NE" && gamecontroller.HitPrecision_NE == 0)
            {
                Destroy(gamecontroller.CurrNode);
                gamecontroller.ExistingNodes.Remove(gamecontroller.CurrNode);
                gamecontroller.Missed();
            } 
            else if (gamecontroller.CurrNode.name == "NW" && gamecontroller.HitPrecision_NW == 0) 
            {
                Destroy(gamecontroller.CurrNode);
                gamecontroller.ExistingNodes.Remove(gamecontroller.CurrNode);
                gamecontroller.Missed();
            }
            else if (gamecontroller.CurrNode.name == "SE" && gamecontroller.HitPrecision_SE == 0)
            {
                Destroy(gamecontroller.CurrNode);
                gamecontroller.ExistingNodes.Remove(gamecontroller.CurrNode);
                gamecontroller.Missed();
            }
            else if (gamecontroller.CurrNode.name == "SW" && gamecontroller.HitPrecision_SW == 0)
            {
                Destroy(gamecontroller.CurrNode);
                gamecontroller.ExistingNodes.Remove(gamecontroller.CurrNode);
                gamecontroller.Missed();
            }
            else if (gamecontroller.CurrNode.name == "N" && gamecontroller.HitPrecision_N == 0)
            {
                Destroy(gamecontroller.CurrNode);
                gamecontroller.ExistingNodes.Remove(gamecontroller.CurrNode);
                gamecontroller.Missed();
            }
            else if (gamecontroller.CurrNode.name == "E" && gamecontroller.HitPrecision_E == 0)
            {
                Destroy(gamecontroller.CurrNode);
                gamecontroller.ExistingNodes.Remove(gamecontroller.CurrNode);
                gamecontroller.Missed();
            }
            else if (gamecontroller.CurrNode.name == "S" && gamecontroller.HitPrecision_S == 0)
            {
                Destroy(gamecontroller.CurrNode);
                gamecontroller.ExistingNodes.Remove(gamecontroller.CurrNode);
                gamecontroller.Missed();
            }
            else if (gamecontroller.CurrNode.name == "W" && gamecontroller.HitPrecision_W == 0)
            {
                Destroy(gamecontroller.CurrNode);
                gamecontroller.ExistingNodes.Remove(gamecontroller.CurrNode);
                gamecontroller.Missed();
            }
        }
    }

    public void IncHitPrecision(GameObject node)
    {
        // Disgusting code, most likely a better way to handle this
        if (node.name == "N")
        {
            gamecontroller.HitPrecision_N++;
        } 
        else if (node.name == "NE")
        {
            gamecontroller.HitPrecision_NE++;
        }
        else if (node.name == "E")
        {
            gamecontroller.HitPrecision_E++;
        }
        else if (node.name == "SE")
        {
            gamecontroller.HitPrecision_SE++;
        }
        else if (node.name == "S")
        {
            gamecontroller.HitPrecision_S++;
        }
        else if (node.name == "SW")
        {
            gamecontroller.HitPrecision_SW++;
        }
        else if (node.name == "W")
        {
            gamecontroller.HitPrecision_W++;
        }
        else if (node.name == "NW")
        {
            gamecontroller.HitPrecision_NW++;
        }
    }

    public void DecHitPrecision(GameObject node)
    {
        if (node.name == "N")
        {
            gamecontroller.HitPrecision_N--;
        }
        else if (node.name == "NE")
        {
            gamecontroller.HitPrecision_NE--;
        }
        else if (node.name == "E")
        {
            gamecontroller.HitPrecision_E--;
        }
        else if (node.name == "SE")
        {
            gamecontroller.HitPrecision_SE--;
        }
        else if (node.name == "S")
        {
            gamecontroller.HitPrecision_S--;
        }
        else if (node.name == "SW")
        {
            gamecontroller.HitPrecision_SW--;
        }
        else if (node.name == "W")
        {
            gamecontroller.HitPrecision_W--;
        }
        else if (node.name == "NW")
        {
            gamecontroller.HitPrecision_NW--;
        }
    }
}
