using System;
using Serilog;

public class Triangle
{
    public Tuple<string, Tuple<int, int>[]> OutputCheck(string a, string b, string c)
    {
        //преобразование введенных значений сторон в числа с плавающей точкой
        float? sideA = ToSingleNullable(a);
        float? sideB = ToSingleNullable(b);
        float? sideC = ToSingleNullable(c);

        if (sideA.HasValue && sideB.HasValue && sideC.HasValue)
        {
            //проверка на существование треугольника
            if (IsTriangle(sideA.Value, sideB.Value, sideC.Value))
            {
                //логирование успешного запроса
                Log.Information($"Успешный запрос, a={a}, b={b}, c={c}, Треугольник");

                //определение типа треугольника
                string triangleType = DetermineTriangleType(sideA.Value, sideB.Value, sideC.Value);
                Console.WriteLine("Тип треугольника: " + triangleType);

                if (triangleType != "Не треугольник")
                {
                    //вычисление и вывод координат вершин треугольника
                    var vertices = CalculateVertices(sideA.Value, sideB.Value, sideC.Value);
                    Console.WriteLine("Координаты вершин:");

                    Log.Information($"Вершина A: ({vertices[0].Item1}, {vertices[0].Item2})");
                    Log.Information($"Вершина B: ({vertices[1].Item1}, {vertices[1].Item2})");
                    Log.Information($"Вершина C: ({vertices[2].Item1}, {vertices[2].Item2})");

                    return Tuple.Create(triangleType, vertices);
                }
                else
                {
                    return Tuple.Create(triangleType, new Tuple<int, int>[] { Tuple.Create(-1, -1), Tuple.Create(-1, -1), Tuple.Create(-1, -1) });
                }
            }
            else
            {
                //логирование неуспешного запроса (не существует треугольника)
                Log.Information($"Неуспешный запрос a={a}, b={b}, c={c}, Не треугольник");
                return Tuple.Create("Не треугольник", new Tuple<int, int>[] { Tuple.Create(-1, -1), Tuple.Create(-1, -1), Tuple.Create(-1, -1) });
            }
        }
        else
        {
            Log.Information($"Неуспешный запрос. a={a}, b={b}, c={c}, переданы нечисловые данные.");
            return Tuple.Create("", new Tuple<int, int>[] { Tuple.Create(-2, -2), Tuple.Create(-2, -2), Tuple.Create(-2, -2) });
        }
    }

    private bool IsTriangle(float a, float b, float c)
    {
        //проверка существования треугольника по условию неравенств треугольника
        return a + b > c && a + c > b && b + c > a;
    }

    private string DetermineTriangleType(float a, float b, float c)
    {
        //определение типа треугольника
        if (a == b && b == c)
            return "Равносторонний";
        else if (a == b || a == c || b == c)
            return "Равнобедренный";
        else
            return "Разносторонний";
    }

    private Tuple<int, int>[] CalculateVertices(float a, float b, float c)
    {
        // Масштабирование координат вершин треугольника к полям 100x100 px
        //  расчет производится по предположению что вершина A (0,0) - начало координат.
        //Вершина B(a, 0) - находится на оси X.
        // Вершина C(a / 2, (sqrt(3) / 2) * a) , по умолчанию равностороний
        int fieldWidth = 100;
        int fieldHeight = 100;
        int xA, yA, xB, yB, xC, yC;

        if (IsTriangle(a, b, c))
        {
            xA = 0;
            yA = fieldHeight;
            xB = (int)((a / c) * fieldWidth);
            yB = fieldHeight;
            xC = (int)((0.5 * a / c) * fieldWidth);
            yC = 0;
        }
        else
        {
            // Если не является треугольником, устанавливаем координаты в (-1, -1)
            xA = -1;
            yA = -1;
            xB = -1;
            yB = -1;
            xC = -1;
            yC = -1;
        }

        return new Tuple<int, int>[] { Tuple.Create(xA, yA), Tuple.Create(xB, yB), Tuple.Create(xC, yC) };
    }


    private float? ToSingleNullable(string value)
    {
        //преобразование строки в число с плавающей точкой с учетом возможных ошибок
        if (string.IsNullOrEmpty(value))
            return null;
        else
        {
            float number;
            if (float.TryParse(value, out number))
                return number;
            return null;
        }
    }

   
}