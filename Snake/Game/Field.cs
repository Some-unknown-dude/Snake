namespace Game
{
    internal class Field
    {
        public readonly int width;
        public readonly int height;
        private const int minLength = 3;
        private const int maxLength = 1000;
        private List<(int, int)> snake = new List<(int, int)> { (0, 2), (0, 1), (0, 0) };
        private (int, int) direction = (0, 1);
        private (int, int) food;
        private (int, int) lastTailPosition = (0, 3);
        private Random random;

        public Field(int width, int height)
        {
            if (width < minLength || width > maxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height < minLength || height > maxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            this.width = width;
            this.height = height;
            this.random = new Random((int)DateTime.Now.Ticks);
            this.SpawnFood();
        }

        public bool Next()
        {
            (int, int) head = (this.snake[0].Item1 + this.direction.Item1, this.snake[0].Item2 + this.direction.Item2);
            if (head.Item1 >= 0 && head.Item1 < this.height && head.Item2 >= 0 && head.Item2 < width && !this.snake.Contains(head))
            {
                this.snake.Insert(0, head);
                if (head != this.food)
                {
                    this.lastTailPosition = snake[^1];
                    snake.RemoveAt(this.snake.Count - 1);
                }
                else
                {
                    this.SpawnFood();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public (int, int) Direction
        {
            get => this.direction;
            set
            {
                this.direction = value;
            }
        }

        public IReadOnlyList<(int, int)> Snake => this.snake;

        public (int, int) Food => this.food;

        public (int, int) LastTailPosition => this.lastTailPosition;

        private void SpawnFood()
        {
            (int, int)[] freeSquares = GetAllFreeSquares().ToArray();
            (int, int) food = freeSquares[random.Next(0, freeSquares.Length)];
            this.food = food;
        }

        private IEnumerable<(int, int)> GetAllFreeSquares()
        {
            for (int i = 0; i < this.height; ++i)
            {
                for (int j = 0; j < this.width; ++j)
                {
                    if (!this.snake.Contains((i, j)))
                    {
                        yield return (i, j);
                    }
                }
            }
        }
    }
}
