namespace Moblik.Utils
{
    public enum ItemType
    {
        NONE, COIN, LIFE_PACK
    }

    public enum ArmourType
    {
        NONE, DEFENSE, SPEED
    }

    public enum AnimationType
    {
        NONE, IDLE, RUN, ATTACK, DEATH
    }

    public enum CharacterStates
    {
        IDLE, MOVE, JUMP, ATTACK, DEATH
    }

    public enum BossStates
    {
        INIT, IDLE, PATROL, PURSUE, ATTACK, DEATH
    }

    public enum GameStates
    {
        INTRO, GAMEPLAY, PAUSE, WIN, LOSE
    }

    public enum UIStatsDisplayType
    {
        NONE, HEALTH, AMMO, MANA
    }
}