using System;

public class SquareMatrix : ICloneable, IComparable<SquareMatrix>
{
  private double[,] _data;
  public int Size { get; }

  public SquareMatrix(int size, bool randomize = false)
  {
    if (size <= 0) throw new MatrixException("Размер матрицы должен быть положительным");

    Size = size;
    _data = new double[size, size];

    if (randomize) FillRandom();
  }

  private void FillRandom()
  {
    Random random = new Random();

    for (int row = 0; row < Size; row++)
      for (int column = 0; column < Size; column++)
        _data[row, column] = random.NextDouble() * 10;
  }

  public static SquareMatrix operator +(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix.Size != secondMatrix.Size) throw new MatrixException("Матрицы должны быть одного размера");

    SquareMatrix resultMatrix = new SquareMatrix(firstMatrix.Size);

    for (int row = 0; row < firstMatrix.Size; row++)
      for (int column = 0; column < firstMatrix.Size; column++)
        resultMatrix._data[row, column] = firstMatrix._data[row, column] + secondMatrix._data[row, column];

    return resultMatrix;
  }

  public static SquareMatrix operator *(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix.Size != secondMatrix.Size) throw new MatrixException("Матрицы должны быть одного размера");

    SquareMatrix resultMatrix = new SquareMatrix(firstMatrix.Size);

    for (int row = 0; row < firstMatrix.Size; row++)
      for (int column = 0; column < firstMatrix.Size; column++)
        for (int inner = 0; inner < firstMatrix.Size; inner++)
          resultMatrix._data[row, column] += firstMatrix._data[row, inner] * secondMatrix._data[inner, column];

    return resultMatrix;
  }

  public double Determinant()
  {
    if (Size == 1) return _data[0, 0];

    if (Size == 2) return _data[0, 0] * _data[1, 1] - _data[0, 1] * _data[1, 0];

    double determinant = 0;

    for (int column = 0; column < Size; column++)
    {
      determinant += (column % 2 == 0 ? 1 : -1) * _data[0, column] * Minor(0, column).Determinant();
    }

    return determinant;
  }

  private SquareMatrix Minor(int excludedRow, int excludedColumn)
  {
    SquareMatrix minorMatrix = new SquareMatrix(Size - 1);

    for (int originalRow = 0, minorRow = 0; originalRow < Size; originalRow++)
    {
      if (originalRow == excludedRow) continue;

      for (int originalColumn = 0, minorColumn = 0; originalColumn < Size; originalColumn++)
      {
        if (originalColumn == excludedColumn) continue;

        minorMatrix._data[minorRow, minorColumn] = _data[originalRow, originalColumn];
        minorColumn++;
      }

      minorRow++;
    }

    return minorMatrix;
  }

  public static bool operator >(SquareMatrix leftMatrix, SquareMatrix rightMatrix) => leftMatrix.Determinant() > rightMatrix.Determinant();
  public static bool operator <(SquareMatrix leftMatrix, SquareMatrix rightMatrix) => leftMatrix.Determinant() < rightMatrix.Determinant();
  public static bool operator >=(SquareMatrix leftMatrix, SquareMatrix rightMatrix) => leftMatrix.Determinant() >= rightMatrix.Determinant();
  public static bool operator <=(SquareMatrix leftMatrix, SquareMatrix rightMatrix) => leftMatrix.Determinant() <= rightMatrix.Determinant();
  public static bool operator ==(SquareMatrix leftMatrix, SquareMatrix rightMatrix) => leftMatrix.Equals(rightMatrix);
  public static bool operator !=(SquareMatrix leftMatrix, SquareMatrix rightMatrix) => !leftMatrix.Equals(rightMatrix);

  public static explicit operator double(SquareMatrix matrix) => matrix.Determinant();

  public override bool Equals(object obj)
  {
    if (obj is not SquareMatrix otherMatrix || Size != otherMatrix.Size) return false;

    for (int row = 0; row < Size; row++)
      for (int column = 0; column < Size; column++)
        if (_data[row, column] != otherMatrix._data[row, column]) return false;

    return true;
  }

  public override int GetHashCode() => _data.GetHashCode();

  public override string ToString()
  {
    string result = "";

    for (int row = 0; row < Size; row++)
    {
      for (int column = 0; column < Size; column++)
      {
        result += _data[row, column].ToString("F2") + " ";
      }

      result += "\n";
    }

    return result;
  }

  public int CompareTo(SquareMatrix otherMatrix) => Determinant().CompareTo(otherMatrix.Determinant());
  public object Clone() => new SquareMatrix(Size) { _data = (double[,])_data.Clone() };
  public double[,] Data => _data;
}
