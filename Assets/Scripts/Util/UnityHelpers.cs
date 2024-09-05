using UnityEngine;

public static class UnityHelpers
{
    // Bu metod 3 ile 5 arasında rastgele bir tam sayı üretir
    public static int GetRandomIntInRange(int min, int max)
    {
        // min ve max sınırları arasında rastgele bir tam sayı üretir
        return Random.Range(min, max + 1);
    }
}