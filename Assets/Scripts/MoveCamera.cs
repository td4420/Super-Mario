using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject character;
    float distanceY;
    float minX , maxX , minY, maxY;
    int stage;
    // Start is called before the first frame update
    void Start()
    {
        minX = -0.05f; maxX = 28.6f; minY = -5.4f; maxY = 8.6f;
        transform.position = new Vector3(minX, maxY, 0);
        character.transform.position = new Vector3(-3.4f, 6.73f, 1);
        distanceY = Mathf.Abs(character.transform.position.y - transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (character.transform.position.y > 5)
        {
            stage = 3;
        }
        else if (character.transform.position.y > -1 && character.transform.position.y < 1)
        {
            stage = 2;
            maxY = 1.6f;
        }
        else if(character.transform.position.y < -6 && character.transform.position.y > -8)
        {
            stage = 1;
        }
        if (stage != 1)
        {
            transform.position = new Vector3(Mathf.Clamp(character.transform.position.x, minX, maxX),
                                         Mathf.Clamp(character.transform.position.y + distanceY, minY, maxY),
                                         0);
        }
        else
        {
            transform.position = new Vector3(Mathf.Clamp(character.transform.position.x, minX, maxX), minY,0);
        }
    }
}
