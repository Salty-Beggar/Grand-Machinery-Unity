

using System;
using UnityEngine;
using UnityEngine.UIElements;
using Entities;
using UnityEditor;
using UnityEditor.UIElements;
using Unity.VisualScripting;

[Serializable]
public class RectCollisionMask : CollisionMask {
    public Vector2 Start, End;

    public RectCollisionMask(Vector2 Start, Vector2 End, Vector2 position) : base(position) {
        this.Type = CollisionMaskType.Rect;
        this.Start = Start;
        this.End = End;
    }

    public RectCollisionMask(Vector2 Start, Vector2 End, EntityScript entity) : base(entity) {
        Type = CollisionMaskType.Rect;
        this.Start = Start;
        this.End = End;
    }

    public Vector2 GetStart() {
        Vector2 currentStart = Start*flip;
        return rotation switch {
            0 => currentStart,
            1 => new Vector2(currentStart.y * -1, currentStart.x),
            2 => currentStart * -1,
            _ => new Vector2(currentStart.y, currentStart.x * -1),
        };
    }

    public Vector2 GetEnd() {
        Vector2 currentEnd = End*flip;
        return rotation switch {
            0 => currentEnd,
            1 => new Vector2(currentEnd.y * -1, currentEnd.x),
            2 => currentEnd * -1,
            _ => new Vector2(currentEnd.y, currentEnd.x * -1),
        };
    }

    public override bool IsPlaceMeeting(Vector2 offset, RectCollisionMask CollisionMask, bool isInclusive)
    { // OBSERVATION_COLLISION001: The code may be less optimized than it could be by using more clear and clever matrix math. I'm not sure of that though, and if it's true I'm not sure if its impact on the game's code is big enough.
        var _CM = CollisionMask;

        Vector2 CurrentPosition = followsEntity ? (Vector2)entity.transform.position+offset : position+offset;
        Vector2 OtherPosition = _CM.followsEntity ? (Vector2)_CM.entity.transform.position : _CM.position;

        Vector2 currentStart = GetStart();
        Vector2 currentEnd = GetEnd();
        Vector2 xBorders;
        Vector2 yBorders;
        if (currentStart.x < currentEnd.x) xBorders = new(currentStart.x, currentEnd.x);
        else xBorders = new(currentEnd.x, currentStart.x);
        if (currentStart.y < currentEnd.y) yBorders = new(currentStart.y, currentEnd.y);
        else yBorders = new(currentEnd.y, currentStart.y);

        Vector2 otherStart = _CM.GetStart();
        Vector2 otherEnd = _CM.GetEnd();
        Vector2 xBordersOther;
        Vector2 yBordersOther;
        if (otherStart.x < otherEnd.x) xBordersOther = new(otherStart.x, otherEnd.x);
        else xBordersOther = new(otherEnd.x, otherStart.x);
        if (otherStart.y < otherEnd.y) yBordersOther = new(otherStart.y, otherEnd.y);
        else yBordersOther = new(otherEnd.y, otherStart.y);

        if (CurrentPosition.x < OtherPosition.x) {
            if (CurrentPosition.y < OtherPosition.y) {
                return CurrentPosition.x+xBorders.y > OtherPosition.x+xBordersOther.x
                    && CurrentPosition.y+yBorders.y > OtherPosition.y+yBordersOther.x;
            }else {
                return CurrentPosition.x+xBorders.y > OtherPosition.x+xBordersOther.x 
                    && OtherPosition.y+yBordersOther.y > CurrentPosition.y+yBorders.x;
            }
        }else {
            if (CurrentPosition.y < OtherPosition.y) {
                return OtherPosition.x+xBordersOther.y > CurrentPosition.x+xBorders.x
                    && CurrentPosition.y+yBorders.y > OtherPosition.y+yBordersOther.x;
            }else {
                return OtherPosition.x+xBordersOther.y > CurrentPosition.x+xBorders.x 
                    && OtherPosition.y+yBordersOther.y > CurrentPosition.y+yBorders.x;
            }
        }
    }
}

/*[CustomPropertyDrawer(typeof(RectCollisionMask))]
public class RectCollisionMaskDrawer : PropertyDrawer {
    bool balls;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 120;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedObject serializedObject = property.serializedObject;
        Debug.Log(serializedObject.FindPropertyOrFail("wow"));
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position = new Rect(position.x, position.y+20, position.width, position.height);

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new("(Rect. col. mask)"));
        position = new Rect(position.x+20, position.y+20, position.width-20, position.height);

        Rect followsEntityRect = new Rect(position.x, position.y, 100, position.height);
        GUIContent followsEntityLabel = new("Follows Entity");
        SerializedProperty followsEntityAttribute = property.FindPropertyRelative("followsEntity");
        followsEntityRect = new(EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), followsEntityLabel).x, 0, position.width, position.height);
        EditorGUI.PropertyField(followsEntityRect, followsEntityAttribute, GUIContent.none);
        position = new Rect(position.x, position.y+20, position.width, position.height);

        if (followsEntityAttribute.boolValue) {
            Rect positionRect = new Rect(position.x, position.y, 100, position.height);
            GUIContent positionLabel = new("Position");
            positionRect = new(EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), positionLabel).x+20, position.y, 100, position.height);
            EditorGUI.PropertyField(positionRect, property.FindPropertyRelative("position"), GUIContent.none);
            position = new Rect(position.x, position.y+20, position.width, position.height);
        }else {
            Rect entityRect = new Rect(position.x, position.y, 100, position.height);
            GUIContent entityLabel = new("Followed Entity");
            entityRect = new(EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), entityLabel).x+20, position.y, 100, 18);
            EditorGUI.PropertyField(entityRect, property.FindPropertyRelative("entity"), GUIContent.none);
            position = new Rect(position.x, position.y+20, position.width, position.height);
        }

        Rect startRect = new Rect(position.x, position.y, 100, position.height);
        GUIContent startLabel = new("Start");
        startRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), startLabel);
        EditorGUI.PropertyField(startRect, property.FindPropertyRelative("Start"), GUIContent.none);
        position = new Rect(position.x, position.y+20, position.width, position.height);

        Rect endRect = new Rect(position.x, position.y, 100, position.height);
        GUIContent endLabel = new("End");
        endRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), endLabel);
        EditorGUI.PropertyField(endRect, property.FindPropertyRelative("End"), GUIContent.none);
        position = new Rect(position.x, position.y+20, position.width, position.height);

        EditorGUI.EndProperty();
    }
}
*/