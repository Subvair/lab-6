public static class MatrixExtensions
{
  public static SquareMatrix Transpose(this SquareMatrix matrix)
  {
    SquareMatrix transposedMatrix = new SquareMatrix(matrix.Size);
    var sourceData = matrix.Data;

    for (int rowIndex = 0; rowIndex < matrix.Size; rowIndex++)
      for (int columnIndex = 0; columnIndex < matrix.Size; columnIndex++)
        transposedMatrix.Data[rowIndex, columnIndex] = sourceData[columnIndex, rowIndex];

    return transposedMatrix;
  }

  public static double Trace(this SquareMatrix matrix)
  {
    double traceSum = 0;

    for (int diagonalIndex = 0; diagonalIndex < matrix.Size; diagonalIndex++)
      traceSum += matrix.Data[diagonalIndex, diagonalIndex];

    return traceSum;
  }
}
