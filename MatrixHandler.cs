public abstract class MatrixHandler 
{
    protected MatrixHandler _next;
    
    public void SetNext(MatrixHandler nextHandler) 
    {
        _next = nextHandler;
    }

    public abstract void Handle(SquareMatrix matrix);
}
public class TransposeHandler : MatrixHandler 
{
  public override void Handle(SquareMatrix matrix) 
  {
    Console.WriteLine("\nТранспонированная матрица:");
    Console.WriteLine(matrix.Transpose());
    _next?.Handle(matrix);
  }
}

public class TraceHandler : MatrixHandler 
{
  public override void Handle(SquareMatrix matrix) 
  {
    Console.WriteLine($"След матрицы: {matrix.Trace():F2}");
    _next?.Handle(matrix);
  }
}

public class DiagonalHandler : MatrixHandler 
{
  public override void Handle(SquareMatrix matrix) 
  {
    Action<SquareMatrix> toDiagonal = delegate (SquareMatrix m) 
    {
      for (int i = 0; i < m.Size; i++)
        for (int j = 0; j < m.Size; j++)
          if (i != j) m.Data[i, j] = 0;
    };

    toDiagonal(matrix);
    Console.WriteLine("Матрица после приведения к диагональному виду:");
    Console.WriteLine(matrix);

    _next?.Handle(matrix);
  }
}

public class ExitHandler : MatrixHandler 
{
  public override void Handle(SquareMatrix matrix) 
  {
    Console.WriteLine("Завершение цепочки обработки.\n");
  }
}