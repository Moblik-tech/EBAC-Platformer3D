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
        NONE, IDLE, MOVE, JUMP, ATTACK, DEATH
    }

    public enum BossStates
    {
        NONE, INIT, IDLE, PATROL, PURSUE, ATTACK, DEATH
    }

    public enum GameStates
    {
        NONE, INTRO, GAMEPLAY, PAUSE, WIN, LOSE
    }

    public enum UIStatsDisplayType
    {
        NONE, HEALTH, AMMO, MANA
    }

    public enum MusicType
    {
        NONE, CALM, BATTLE, VICTORY, LOSE
    }

    public enum SFXType
    {
        NONE, COIN, LIFE_PACK, SHOOT, WALK, HIT, DEATH, JUMP
    }
}