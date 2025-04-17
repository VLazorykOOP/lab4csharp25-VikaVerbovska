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
    // Поля
    protected uint[] IntArray; // масив
    protected uint size; // розмір вектора
    protected int codeError; // код помилки
    protected static uint num_vec; // кількість векторів

    // Конструктор без параметрів
    public VectorUInt()
    {
        size = 1;
        IntArray = new uint[size];
        IntArray[0] = 0;
        num_vec++;
    }

    // Конструктор з одним параметром - розмір вектора
    public VectorUInt(uint size)
    {
        this.size = size;
        IntArray = new uint[size];
        for (int i = 0; i < size; i++)
            IntArray[i] = 0;
        num_vec++;
    }

    // Конструктор із двома параметрами - розмір вектора та значення ініціалізації
    public VectorUInt(uint size, uint initialValue)
    {
        this.size = size;
        IntArray = new uint[size];
        for (int i = 0; i < size; i++)
            IntArray[i] = initialValue;
        num_vec++;
    }

    // Деструктор
    ~VectorUInt()
    {
        Console.WriteLine("Вектор знищений.");
    }

    // Властивості
    public uint Size => size; // Тільки для читання
    public int CodeError
    {
        get => codeError;
        set => codeError = value; // Доступ для читання і запису
    }

    // Індексатор
    public uint this[int index]
    {
        get
        {
            if (index < 0 || index >= size)
            {
                codeError = -1;
                return 0;
            }
            return IntArray[index];
        }
        set
        {
            if (index < 0 || index >= size)
                codeError = -1;
            else
                IntArray[index] = value;
        }
    }

    // Методи
    public void InputElements()
    {
        Console.WriteLine("Введіть елементи вектора:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Елемент {i + 1}: ");
            IntArray[i] = uint.Parse(Console.ReadLine());
        }
    }

    public void Print()
    {
        Console.Write("[");
        for (int i = 0; i < size; i++)
            Console.Write($"{IntArray[i]} {(i < size - 1 ? ", " : "")}");
        Console.WriteLine("]");
    }

    public void AssignValueToElements(uint value)
    {
        for (int i = 0; i < size; i++)
        {
            IntArray[i] = value;
        }
    }

    // Статичний метод для підрахунку кількості векторів
    public static uint GetVectorCount()
    {
        return num_vec;
    }

    // Перевантаження унарного інкремента (++)
    public static VectorUInt operator ++(VectorUInt v)
    {
        for (int i = 0; i < v.size; i++)
            v.IntArray[i]++;
        return v;
    }

    // Перевантаження унарного декремента (--)
    public static VectorUInt operator --(VectorUInt v)
    {
        for (int i = 0; i < v.size; i++)
            v.IntArray[i]--;
        return v;
    }

    // Перевантаження операцій == та !=
    public static bool operator ==(VectorUInt a, VectorUInt b)
    {
        if (a.size != b.size) return false;
        for (int i = 0; i < a.size; i++)
        {
            if (a.IntArray[i] != b.IntArray[i])
                return false;
        }
        return true;
    }

    public static bool operator !=(VectorUInt a, VectorUInt b)
    {
        return !(a == b);
    }

    // Перевантаження логічних операцій true/false
    public static explicit operator bool(VectorUInt v)
    {
        if (v.size == 0) return false;
        foreach (var element in v.IntArray)
        {
            if (element != 0)
                return true;
        }
        return false;
    }

    // Перевантаження операції заперечення !
    public static bool operator !(VectorUInt v)
    {
        return !((bool)v);
    }

    // Перевантаження побітових операцій
    public static VectorUInt operator ~(VectorUInt v)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = (uint)~(int)v.IntArray[i];
        return result;
    }

    // Перевантаження арифметичних операторів
    // + (додавання)
    public static VectorUInt operator +(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < a.size ? a.IntArray[i] : 0) + (i < b.size ? b.IntArray[i] : 0);
        return result;
    }

    public static VectorUInt operator +(VectorUInt v, int scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] + (uint)scalar;
        return result;
    }

    // - (віднімання)
    public static VectorUInt operator -(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < a.size ? a.IntArray[i] : 0) - (i < b.size ? b.IntArray[i] : 0);
        return result;
    }

    public static VectorUInt operator -(VectorUInt v, int scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] - (uint)scalar;
        return result;
    }

    // * (множення)
    public static VectorUInt operator *(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < a.size ? a.IntArray[i] : 0) * (i < b.size ? b.IntArray[i] : 0);
        return result;
    }

    public static VectorUInt operator *(VectorUInt v, int scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] * (uint)scalar;
        return result;
    }

    // / (ділення)
    public static VectorUInt operator /(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < b.size && b.IntArray[i] != 0) ? (i < a.size ? a.IntArray[i] : 0) / b.IntArray[i] : 0;
        return result;
    }

    // Для вектора і скаляра
    public static VectorUInt operator /(VectorUInt v, short scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = scalar != 0 ? v.IntArray[i] / (uint)scalar : 0;
        return result;
    }

    // % (остання від ділення)
    public static VectorUInt operator %(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < b.size && b.IntArray[i] != 0) ? (i < a.size ? a.IntArray[i] : 0) % b.IntArray[i] : 0;
        return result;
    }

    // Для вектора і скаляра
    public static VectorUInt operator %(VectorUInt v, short scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = scalar != 0 ? v.IntArray[i] % (uint)scalar : 0;
        return result;
    }

    // Перевантаження побітових операцій
    // | (побітове додавання)
    // Для двох векторів
    public static VectorUInt operator |(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < a.size ? a.IntArray[i] : 0) | (i < b.size ? b.IntArray[i] : 0);
        return result;
    }

    // Для вектора і скаляра
    public static VectorUInt operator |(VectorUInt v, uint scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] | scalar;
        return result;
    }

    // ^ (побітове додавання за модулем 2)
    // Для двох векторів
    public static VectorUInt operator ^(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < a.size ? a.IntArray[i] : 0) ^ (i < b.size ? b.IntArray[i] : 0);
        return result;
    }

    // Для вектора і скаляра
    public static VectorUInt operator ^(VectorUInt v, uint scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] ^ scalar;
        return result;
    }

    // >> (побітовий зсув право)
    // Для двох векторів
    public static VectorUInt operator >>(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < a.size ? a.IntArray[i] : 0) >> (i < b.size ? (int)b.IntArray[i] : 0);
        return result;
    }

    // Для вектора і скаляра
    public static VectorUInt operator >>(VectorUInt v, uint scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] >> (int)scalar;
        return result;
    }

    // << (побітовий зсув ліво)
    // Для двох векторів
    public static VectorUInt operator <<(VectorUInt a, VectorUInt b)
    {
        uint newSize = Math.Max(a.size, b.size);
        VectorUInt result = new VectorUInt(newSize);
        for (int i = 0; i < newSize; i++)
            result.IntArray[i] = (i < a.size ? a.IntArray[i] : 0) << (i < b.size ? (int)b.IntArray[i] : 0);
        return result;
    }

    // Для вектора і скаляра
    public static VectorUInt operator <<(VectorUInt v, uint scalar)
    {
        VectorUInt result = new VectorUInt(v.size);
        for (int i = 0; i < v.size; i++)
            result.IntArray[i] = v.IntArray[i] << (int)scalar;
        return result;
    }

    // Перевантаження порівняння
    // > (більше)
    public static bool operator >(VectorUInt a, VectorUInt b)
    {
        if (a.size != b.size) return false;
        for (int i = 0; i < a.size; i++)
        {
            if (a.IntArray[i] <= b.IntArray[i])
                return false;
        }
        return true;
    }

    // >= (більше або рівне)
    public static bool operator >=(VectorUInt a, VectorUInt b)
    {
        if (a.size != b.size) return false;
        for (int i = 0; i < a.size; i++)
        {
            if (a.IntArray[i] < b.IntArray[i])
                return false;
        }
        return true;
    }

    // < (менше)
    public static bool operator <(VectorUInt a, VectorUInt b)
    {
        if (a.size != b.size) return false;
        for (int i = 0; i < a.size; i++)
        {
            if (a.IntArray[i] >= b.IntArray[i])
                return false;
        }
        return true;
    }

    // <= (менше або рівне)
    public static bool operator <=(VectorUInt a, VectorUInt b)
    {
        if (a.size != b.size) return false;
        for (int i = 0; i < a.size; i++)
        {
            if (a.IntArray[i] > b.IntArray[i])
                return false;
        }
        return true;
    }
}




class MatrixUint
{
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

    // Конструктор з параметрами
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
            for (int j = 0; j < m; j++)
                IntArray[i, j] = initValue;
        codeError = 0;
        num_m++;
    }

    // Деструктор
    ~MatrixUint()
    {
        Console.WriteLine("Матриця знищена");
    }

    // Введення елементів з клавіатури
    public void InputElements()
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                Console.Write($"[{i}, {j}]: ");
                IntArray[i, j] = uint.Parse(Console.ReadLine());
            }
    }

    // Виведення елементів матриці
    public void Print()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
                Console.Write(IntArray[i, j] + " ");
            Console.WriteLine();
        }
    }

    // Індексатор для доступу до елементів
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

    // Властивості
    public int Rows => n;
    public int Columns => m;
    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    public static int NumMatrices => num_m;

    // Статичний метод, що підраховує кількість матриць
    public static void PrintNumMatrices()
    {
        Console.WriteLine($"Кількість матриць: {num_m}");
    }

    // Перевантаження операцій
    public static MatrixUint operator +(MatrixUint mat1, MatrixUint mat2)
    {
        if (mat1.n != mat2.n || mat1.m != mat2.m)
            return mat1; 

        MatrixUint result = new MatrixUint(mat1.n, mat1.m);
        for (int i = 0; i < mat1.n; i++)
            for (int j = 0; j < mat1.m; j++)
                result[i, j] = mat1[i, j] + mat2[i, j];
        return result;
    }

    public static MatrixUint operator +(MatrixUint mat, uint scalar)
    {
        MatrixUint result = new MatrixUint(mat.n, mat.m);
        for (int i = 0; i < mat.n; i++)
            for (int j = 0; j < mat.m; j++)
                result[i, j] = mat[i, j] + scalar;
        return result;
    }

    public static MatrixUint operator -(MatrixUint mat1, MatrixUint mat2)
    {
        if (mat1.n != mat2.n || mat1.m != mat2.m)
            return mat1;

        MatrixUint result = new MatrixUint(mat1.n, mat1.m);
        for (int i = 0; i < mat1.n; i++)
            for (int j = 0; j < mat1.m; j++)
                result[i, j] = mat1[i, j] - mat2[i, j];
        return result;
    }

    public static MatrixUint operator -(MatrixUint mat, uint scalar)
    {
        MatrixUint result = new MatrixUint(mat.n, mat.m);
        for (int i = 0; i < mat.n; i++)
            for (int j = 0; j < mat.m; j++)
                result[i, j] = mat[i, j] - scalar;
        return result;
    }

    public static MatrixUint operator *(MatrixUint mat1, MatrixUint mat2)
    {
        if (mat1.m != mat2.n)
            return mat1; 

        MatrixUint result = new MatrixUint(mat1.n, mat2.m);
        for (int i = 0; i < mat1.n; i++)
            for (int j = 0; j < mat2.m; j++)
                for (int k = 0; k < mat1.m; k++)
                    result[i, j] += mat1[i, k] * mat2[k, j];
        return result;
    }

    public static MatrixUint operator *(MatrixUint mat, uint scalar)
    {
        MatrixUint result = new MatrixUint(mat.n, mat.m);
        for (int i = 0; i < mat.n; i++)
            for (int j = 0; j < mat.m; j++)
                result[i, j] = mat[i, j] * scalar;
        return result;
    }

    // Перевантаження порівнянь
    public static bool operator ==(MatrixUint mat1, MatrixUint mat2)
    {
        if (mat1.n != mat2.n || mat1.m != mat2.m)
            return false;

        for (int i = 0; i < mat1.n; i++)
            for (int j = 0; j < mat1.m; j++)
                if (mat1[i, j] != mat2[i, j])
                    return false;
        return true;
    }

    public static bool operator !=(MatrixUint mat1, MatrixUint mat2)
    {
        return !(mat1 == mat2);
    }

    public static bool operator >(MatrixUint mat1, MatrixUint mat2)
    {
        // Порівняння елементів матриць за індексами
        for (int i = 0; i < mat1.n; i++)
            for (int j = 0; j < mat1.m; j++)
                if (mat1[i, j] <= mat2[i, j])
                    return false;
        return true;
    }

    public static bool operator <(MatrixUint mat1, MatrixUint mat2)
    {
        // Порівняння елементів матриць за індексами
        for (int i = 0; i < mat1.n; i++)
            for (int j = 0; j < mat1.m; j++)
                if (mat1[i, j] >= mat2[i, j])
                    return false;
        return true;
    }

    public static bool operator >=(MatrixUint mat1, MatrixUint mat2)
    {
        return !(mat1 < mat2);
    }

    public static bool operator <=(MatrixUint mat1, MatrixUint mat2)
    {
        return !(mat1 > mat2);
    }

    // Побітові операції
    public static MatrixUint operator |(MatrixUint mat1, MatrixUint mat2)
    {
        if (mat1.n != mat2.n || mat1.m != mat2.m)
            return mat1;

        MatrixUint result = new MatrixUint(mat1.n, mat1.m);
        for (int i = 0; i < mat1.n; i++)
            for (int j = 0; j < mat1.m; j++)
                result[i, j] = mat1[i, j] | mat2[i, j];
        return result;
    }

    public static MatrixUint operator ^(MatrixUint mat1, MatrixUint mat2)
    {
        if (mat1.n != mat2.n || mat1.m != mat2.m)
            return mat1;

        MatrixUint result = new MatrixUint(mat1.n, mat1.m);
        for (int i = 0; i < mat1.n; i++)
            for (int j = 0; j < mat1.m; j++)
                result[i, j] = mat1[i, j] ^ mat2[i, j];
        return result;
    }

    public static MatrixUint operator ~(MatrixUint mat)
    {
        MatrixUint result = new MatrixUint(mat.n, mat.m);
        for (int i = 0; i < mat.n; i++)
            for (int j = 0; j < mat.m; j++)
                result[i, j] = (uint)~(int)mat[i, j];
        return result;
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
                MatrixUint mat1 = new MatrixUint(2, 2);
                mat1.InputElements();
                MatrixUint mat2 = new MatrixUint(2, 2, 1);
                mat2.InputElements();

                Console.WriteLine("Матриця 1:");
                mat1.Print();

                Console.WriteLine("Матриця 2:");
                mat2.Print();

                MatrixUint result = mat1 + mat2;
                Console.WriteLine("Матриця 1 + Матриця 2:");
                result.Print();
                break;  

            default:
                Console.WriteLine("Невірний вибір!");
                break;
        }
    }
}

