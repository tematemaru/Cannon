public enum EnemyType { Basic, Fast, Tank }

[System.Serializable]
public class EnemyWave
{
    public EnemyType type;
    public int count;
    public float delayBetweenSpawns;
}