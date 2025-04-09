using System;

using System;

class Triangle
{
    private int a, b, c;
    private int color;

    public Triangle(int sideA, int sideB, int sideC, int color)
    {
        if (sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA)
        {
            a = sideA;
            b = sideB;
            c = sideC;
            this.color = color;
        }
        else
        {
            throw new ArgumentException("Неможливо створити трикутник з такими сторонами!");
        }
    }

    public int this[int index]
    {
        get
        {
            if (index == 0) return a;
            if (index == 1) return b;
            if (index == 2) return c;
            if (index == 3) return color;
            throw new IndexOutOfRangeException("Неправильний індекс");
        }
        set
        {
            if (index == 0) a = value;
            else if (index == 1) b = value;
            else if (index == 2) c = value;
            else if (index == 3) color = value;
            else throw new IndexOutOfRangeException("Неправильний індекс");
        }
    }

    public static Triangle operator ++(Triangle t)
    {
        return new Triangle(t.a + 1, t.b + 1, t.c + 1, t.color);
    }

    public static Triangle operator --(Triangle t)
    {
        return new Triangle(t.a - 1, t.b - 1, t.c - 1, t.color);
    }

    public static Triangle operator *(Triangle t, int scalar)
    {
        return new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);
    }

    public static bool operator true(Triangle t)
    {
        return t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a;
    }

    public static bool operator false(Triangle t)
    {
        return !(t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a);
    }

    public static explicit operator string(Triangle t)
    {
        return $"{t.a},{t.b},{t.c},{t.color}";
    }

    public static explicit operator Triangle(string str)
    {
        string[] parts = str.Split(',');
        return new Triangle(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Сторони: {a}, {b}, {c}, Колір: {color}");
    }
}

class VectorUInt
{
    private uint[] elements;
    private uint size;
    private int codeError;
    private static uint numVectors = 0;

    public VectorUInt() // Конструктор без параметрів
    {
        size = 1;
        elements = new uint[size];
        elements[0] = 0;
        numVectors++;
    }

    public VectorUInt(uint size, uint initialValue = 0) // Конструктор з параметром
    {
        this.size = size;
        elements = new uint[size];
        for (int i = 0; i < size; i++)
            elements[i] = initialValue;
        numVectors++;
    }

    public uint Size => size; // Тільки для читання
    public int CodeError { get; set; } // Доступ для читання і запису

    public uint this[int index] // Індексатор
    {
        get
        {
            if (index < 0 || index >= size)
            {
                codeError = -1;
                return 0;
            }
            return elements[index];
        }
        set
        {
            if (index < 0 || index >= size)
                codeError = -1;
            else
                elements[index] = value;
        }
    }

    public static VectorUInt operator +(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.elements[i] = (i < a.size ? a.elements[i] : 0) + (i < b.size ? b.elements[i] : 0);
        return result;
    }

    public static VectorUInt operator +(VectorUInt v, int scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.elements[i] = v.elements[i] + (uint)scalar;
        return result;
    }

    public void Print()
    {
        Console.Write("[");
        for (int i = 0; i < size; i++)
            Console.Write($"{elements[i]} {(i < size - 1 ? ", " : "")}");
        Console.WriteLine("]");
    }
}


class MatrixUint
{
    // Захищені поля
    protected uint[,] IntArray;
    protected int n, m;
    protected int codeError;
    protected static int num_m = 0;

    // Конструктор без параметрів
    public MatrixUint()
    {
        n = 1;
        m = 1;
        IntArray = new uint[n, m];
        codeError = 0;
        num_m++;
    }

    // Конструктор з двома параметрами
    public MatrixUint(int rows, int cols)
    {
        n = rows;
        m = cols;
        IntArray = new uint[n, m];
        codeError = 0;
        num_m++;
    }

    // Конструктор з трьома параметрами
    public MatrixUint(int rows, int cols, uint initValue)
    {
        n = rows;
        m = cols;
        IntArray = new uint[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                IntArray[i, j] = initValue;
            }
        }
        codeError = 0;
        num_m++;
    }

    // Деструктор
    ~MatrixUint()
    {
        Console.WriteLine("Matrix destroyed");
    }

    // Введення елементів з клавіатури
    public void InputElements()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write($"[{i}, {j}]: ");
                IntArray[i, j] = uint.Parse(Console.ReadLine());
            }
        }
    }

    // Виведення матриці
    public void Print()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                Console.Write(IntArray[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    // Властивості для розмірів матриці
    public int Rows => n;
    public int Cols => m;

    // Властивість для поля codeError
    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    // Індексатор з двома індексами
    public uint this[int i, int j]
    {
        get
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                return IntArray[i, j];
            codeError = -1;
            return 0;
        }
        set
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                IntArray[i, j] = value;
            else
                codeError = -1;
        }
    }

    // Індексатор з одним індексом (лінійний доступ)
    public uint this[int k]
    {
        get
        {
            int i = k / m;
            int j = k % m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                return IntArray[i, j];
            codeError = -1;
            return 0;
        }
        set
        {
            int i = k / m;
            int j = k % m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                IntArray[i, j] = value;
            else
                codeError = -1;
        }
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть завдання:");
        Console.WriteLine("1 - Робота з трикутником");
        Console.WriteLine("2 - Робота з вектором");
        Console.WriteLine("3 - Робота з матрицею");
        Console.Write("Введіть номер завдання: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Triangle t1 = new Triangle(3, 4, 5, 1);
                t1.PrintInfo();
                t1++;
                t1.PrintInfo();
                break;

            case 2:
                VectorUInt v1 = new VectorUInt(5, 2);
                VectorUInt v2 = new VectorUInt(3, 3);
                VectorUInt v3 = v1 + v2;
                v1.Print();
                v2.Print();
                v3.Print();
                break;

            case 3:
                MatrixUint matrix = new MatrixUint(2, 3, 1);
                matrix.Print();
                matrix[0, 1] = 5;
                matrix.Print();
                break;

            default:
                Console.WriteLine("Невірний вибір!");
                break;
        }
    }
}

