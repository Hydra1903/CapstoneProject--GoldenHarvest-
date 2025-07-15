using System.Linq;
using UnityEngine;

public enum AnimalType { None, WhiteSheep, CreamSheep, BlackSheep, BlackGoat, WhiteGoat }

public static class AnimalFactory
{
    public static AnimalBaseFac CreateAnimal(AnimalType type, Vector3 position)
    {
        GameObject prefab = null;

        switch (type)
        {
            case AnimalType.BlackGoat:
                prefab = Resources.Load<GameObject>("Prefabs/BlackGoat");
                break;
            case AnimalType.WhiteGoat:
                prefab = Resources.Load<GameObject>("Prefabs/WhiteGoat");
                break;
            case AnimalType.WhiteSheep:
                prefab = Resources.Load<GameObject>("Prefabs/WhiteSheep");
                break;
            case AnimalType.BlackSheep:
                prefab = Resources.Load<GameObject>("Prefabs/BlackSheep");
                break;
            case AnimalType.CreamSheep:
                prefab = Resources.Load<GameObject>("Prefabs/CreamSheep");
                break;
            default:
                Debug.LogWarning("Invalid or unselected animal type.");
                return null;
        }

        if (prefab != null)
        {
            GameObject obj = GameObject.Instantiate(prefab, position, Quaternion.identity);
            AnimalBaseFac animal = obj.GetComponent<AnimalBaseFac>();
            SimpleAI ai = obj.GetComponent<SimpleAI>();
            if (ai != null)
            {
                GameObject[] pointObjects = GameObject.FindGameObjectsWithTag("WanderPoint");
                ai.wanderPoints = pointObjects.Select(p => p.transform).ToArray();
            }
            //else if (ai != null)
            //{
            //    GameObject[] pointObjects = GameObject.FindGameObjectsWithTag("WanderPoint2");
            //    ai.wanderPoints = pointObjects.Select(p => p.transform).ToArray();
            //}
        }

        return null;
    }
}
