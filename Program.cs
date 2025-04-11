class Program {
  static void Main() {
    Console.Write("Введите размер матрицы: ");
    int size = int.Parse(Console.ReadLine());

    SquareMatrix m1 = new SquareMatrix(size, true);
    SquareMatrix m2 = new SquareMatrix(size, true);

    Console.WriteLine("\nМатрица 1:");
    Console.WriteLine(m1);

    Console.WriteLine("Матрица 2:");
    Console.WriteLine(m2);

    Console.WriteLine("Результат сложения матриц:");
    Console.WriteLine(m1 + m2);

    Console.WriteLine("Результат умножения матриц:");
    Console.WriteLine(m1 * m2);

    Console.WriteLine($"Определитель матрицы 1: {m1.Determinant():F4}");
    Console.WriteLine($"Определитель матрицы 2: {m2.Determinant():F4}\n");

    MatrixHandler handler = new TransposeHandler();
    handler.SetNext(new TraceHandler());
    handler.SetNext(new DiagonalHandler());

    handler.Handle(m1);
    }
}