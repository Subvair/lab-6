public static class MatrixExtensions {
  public static SquareMatrix Transpose(this SquareMatrix matrix) {
    SquareMatrix result = new SquareMatrix(matrix.Size);
    var data = matrix.Data;
    for (int i = 0; i < matrix.Size; i++)
      for (int j = 0; j < matrix.Size; j++)
        result.Data[i, j] = data[j, i];
    return result;
  }

  public static double Trace(this SquareMatrix matrix) {
    double trace = 0;
    for (int i = 0; i < matrix.Size; i++)
      trace += matrix.Data[i, i];
    return trace;
  }
}