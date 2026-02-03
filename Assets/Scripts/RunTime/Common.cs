using System;

[Flags]
public enum EEquipmentPosition
{
    UpLeft = 1 << 0,
    UpRight = 1 << 1,
    RightUp = 1 << 2,
    RightDown = 1 << 3,
    DownLeft = 1 << 4,
    DownRight = 1 << 5,
    LeftUp = 1 << 6,
    LeftDown = 1 << 7,
    Up = UpLeft | UpRight,
    Right = RightUp | RightDown,
    Down = DownLeft | DownRight,
    Left = LeftUp | LeftDown
}

[Flags]
public enum EDoorPosition
{
    UpLeft = 1 << 0,
    UpRight = 1 << 1,
    RightUp = 1 << 2,
    RightDown = 1 << 3,
    DownLeft = 1 << 4,
    DownRight = 1 << 5,
    LeftUp = 1 << 6,
    LeftDown = 1 << 7,
    Up = UpLeft | UpRight,
    Right = RightUp | RightDown,
    Down = DownLeft | DownRight,
    Left = LeftUp | LeftDown

}

public enum ERoomType
{
    O_2_2,  // 4Ä­ ³×¸ð
    I_2_1,  // °¡·Î·Î ±ä 2Ä­
    I_1_2   // ¼¼·Î·Î ±ä 2Ä­
}

public static class Common
{
    public static float z_offset = -0.01f;
}
