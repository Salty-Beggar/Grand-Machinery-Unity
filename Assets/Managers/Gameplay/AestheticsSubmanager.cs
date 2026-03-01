

using UnityEngine;
using UnityEngine.UI;

public class AestheticsSubmanager : Manager {

    public void __DEBUG_DrawSprite(Vector2 position, Sprite sprite) {
        Texture2D texture = sprite.texture;
        float rowValue = sprite.rect.width/texture.width;
        float columnValue = sprite.rect.height/texture.height;
        Rect sprRect = new Rect(sprite.rect.x/texture.width, sprite.rect.y/texture.height, rowValue, columnValue);
        Graphics.DrawTexture(
            new Rect(
                position.x-sprite.pivot.x,
                position.y+sprite.rect.height-sprite.pivot.y,
                sprite.rect.width,
                -sprite.rect.height
            ),
            texture, sprRect, 0, 0, 0, 0
        );
    }
    public void __DEBUG_DrawSprite(Vector2 position, Sprite sprite, Vector2 flip) {
        Texture2D texture = sprite.texture;
        float rowValue = sprite.rect.width/texture.width;
        float columnValue = sprite.rect.height/texture.height;
        Rect sprRect = new Rect(sprite.rect.x/texture.width, sprite.rect.y/texture.height, rowValue, columnValue);
        Graphics.DrawTexture(
            new Rect(
                position.x - flip.x*sprite.pivot.x,
                position.y+sprite.rect.height+flip.y*sprite.pivot.y,
                flip.x*sprite.rect.width,
                -flip.y*sprite.rect.height
            ),
            texture, sprRect, 0, 0, 0, 0
        );
    }
}