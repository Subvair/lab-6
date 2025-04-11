public abstract class MatrixHandler
{
  protected MatrixHandler _nextHandler;

  public void SetNext(MatrixHandler nextHandler)
  {
    _nextHandler = nextHandler;
  }

  public abstract void Handle(SquareMatrix matrix);
}

public class TransposeHandler : MatrixHandler
{
  public override void Handle(SquareMatrix matrix)
  {
    Console.WriteLine("\nТранспонированная матрица:");
    Console.WriteLine(matrix.Transpose());
    _nextHandler?.Handle(matrix);
  }
}

public class TraceHandler : MatrixHandler
{
  public override void Handle(SquareMatrix matrix)
  {
    Console.WriteLine($"След матрицы: {matrix.Trace():F2}");
    _nextHandler?.Handle(matrix);
  }
}

public class DiagonalHandler : MatrixHandler
{
  public override void Handle(SquareMatrix matrix)
  {
    Action<SquareMatrix> convertToDiagonal = delegate (SquareMatrix inputMatrix)
    {
      for (int rowIndex = 0; rowIndex < inputMatrix.Size; rowIndex++)
        for (int columnIndex = 0; columnIndex < inputMatrix.Size; columnIndex++)
          if (rowIndex != columnIndex)
            inputMatrix.Data[rowIndex, columnIndex] = 0;
    };

    convertToDiagonal(matrix);
    Console.WriteLine("Матрица после приведения к диагональному виду:");
    Console.WriteLine(matrix);

    _nextHandler?.Handle(matrix);
  }
}

public class ExitHandler : MatrixHandler
{
  public override void Handle(SquareMatrix matrix)
  {
    Console.WriteLine("Завершение цепочки обработки.\n");
  }
}
