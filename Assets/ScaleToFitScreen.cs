using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{
    private List<SpriteRenderer> srs;

    public void Resize()
    {
        srs = GetSpriteRenderers(gameObject);
            

            // world height is always camera's orthographicSize * 2
            float worldScreenHeight = Camera.main.orthographicSize * 2;

        // world width is calculated by diving world height with screen heigh
        // then multiplying it with screen width
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // to scale the game object we divide the world screen width with the
        // size x of the sprite, and we divide the world screen height with the
        // size y of the sprite
        foreach (SpriteRenderer sr in srs)
            sr.transform.localScale = new Vector3(
                worldScreenWidth / sr.sprite.bounds.size.x,
                worldScreenHeight / sr.sprite.bounds.size.y, 1);
    }

    private List<SpriteRenderer> GetSpriteRenderers(GameObject gameObject)
    {
        List<SpriteRenderer> result = new();
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                result.Add(child.gameObject.GetComponent<SpriteRenderer>());
            result.AddRange(GetSpriteRenderers(child.gameObject));
        }
        return result;
    }
}