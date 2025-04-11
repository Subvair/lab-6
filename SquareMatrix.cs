using System;

public class SquareMatrix : ICloneable, IComparable<SquareMatrix> {
  private double[,] _data;
  public int Size { get; }

  public SquareMatrix(int size, bool randomize = false) {
    if (size <= 0) throw new MatrixException("Размер матрицы должен быть положительным");
    Size = size;
    _data = new double[size, size];
    if (randomize) FillRandom();
  }

  private void FillRandom() {
    Random rand = new Random();
    for (int i = 0; i < Size; i++)
      for (int j = 0; j < Size; j++)
        _data[i, j] = rand.NextDouble() * 10;
  }

  public static SquareMatrix operator +(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size) throw new MatrixException("Матрицы должны быть одного размера");
    SquareMatrix result = new SquareMatrix(a.Size);
    for (int i = 0; i < a.Size; i++)
      for (int j = 0; j < a.Size; j++)
        result._data[i, j] = a._data[i, j] + b._data[i, j];
    return result;
  }

  public static SquareMatrix operator *(SquareMatrix a, SquareMatrix b) {
    if (a.Size != b.Size) throw new MatrixException("Матрицы должны быть одного размера");
    SquareMatrix result = new SquareMatrix(a.Size);
    for (int i = 0; i < a.Size; i++)
      for (int j = 0; j < a.Size; j++)
        for (int k = 0; k < a.Size; k++)
          result._data[i, j] += a._data[i, k] * b._data[k, j];
    return result;
  }

  public double Determinant() {
    if (Size == 1) return _data[0, 0];
    if (Size == 2) return _data[0, 0] * _data[1, 1] - _data[0, 1] * _data[1, 0];

    double det = 0;
    for (int j = 0; j < Size; j++) {
      det += (j % 2 == 0 ? 1 : -1) * _data[0, j] * Minor(0, j).Determinant();
    }
    return det;
  }

  private SquareMatrix Minor(int row, int col) {
    SquareMatrix minor = new SquareMatrix(Size - 1);
    for (int i = 0, mi = 0; i < Size; i++) {
      if (i == row) continue;
      for (int j = 0, mj = 0; j < Size; j++) {
        if (j == col) continue;
        minor._data[mi, mj] = _data[i, j];
        mj++;
      }
      mi++;
    }
    return minor;
  }

  public static bool operator >(SquareMatrix a, SquareMatrix b) => a.Determinant() > b.Determinant();
  public static bool operator <(SquareMatrix a, SquareMatrix b) => a.Determinant() < b.Determinant();
  public static bool operator >=(SquareMatrix a, SquareMatrix b) => a.Determinant() >= b.Determinant();
  public static bool operator <=(SquareMatrix a, SquareMatrix b) => a.Determinant() <= b.Determinant();
  public static bool operator ==(SquareMatrix a, SquareMatrix b) => a.Equals(b);
  public static bool operator !=(SquareMatrix a, SquareMatrix b) => !a.Equals(b);

  public static explicit operator double(SquareMatrix m) => m.Determinant();

  public override bool Equals(object obj) {
    if (obj is not SquareMatrix other || Size != other.Size) return false;
    for (int i = 0; i < Size; i++)
      for (int j = 0; j < Size; j++)
        if (_data[i, j] != other._data[i, j]) return false;
    return true;
  }

  public override int GetHashCode() => _data.GetHashCode();

  public override string ToString() {
    string result = "";
    for (int i = 0; i < Size; i++) {
      for (int j = 0; j < Size; j++) {
        result += _data[i, j].ToString("F2") + " ";
      }
      result += "\n";
    }
    return result;
  }

  public int CompareTo(SquareMatrix other) => Determinant().CompareTo(other.Determinant());
  public object Clone() => new SquareMatrix(Size) { _data = (double[,])_data.Clone() };

  public double[,] Data => _data;
}