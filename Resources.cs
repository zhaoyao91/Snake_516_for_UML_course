enum Direction
{
    Center,
    Up,
    Down,
    Left,
    Right
}

class Position
{
    public int X;
    public int Y;
    public Position(Position pos)
    {
        X = pos.X;
        Y = pos.Y;
    }

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

enum ObjectType
{
    GridObject,
    SnakeObject,
    Wall,
    Food,
    SnakeHead,
    SnakeBody
}