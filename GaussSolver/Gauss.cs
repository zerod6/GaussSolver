using System;

namespace GaussSolver
{
    public static class Gauss
    {
        // Метод для решения системы линейных уравнений методом Гаусса
        public static object SolveGaussElimination(double[,] matrix)
        {
            int n = matrix.GetLength(0);  // Количество уравнений
            int m = matrix.GetLength(1) - 1;  // Количество переменных
            double[] result = new double[m];  // Массив для решения
            Logger.LogMessage("Запуск метода Гаусса.", Logger.LogLevel.INFO);

            // Прямой ход Гаусса: приведение матрицы к верхнему треугольному виду
            for (int i = 0; i < n; i++)
            {
                int maxRow = i;
                double max = Math.Abs(matrix[i, i]);

                // Поиск максимального элемента для минимизации погрешностей
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(matrix[k, i]) > max)
                    {
                        max = Math.Abs(matrix[k, i]);
                        maxRow = k;
                    }
                }
                Logger.LogMessage($"Выбор главного элемента в столбце {i}, строка {maxRow}.", Logger.LogLevel.DEBUG);
                if (Math.Abs(matrix[maxRow, i]) < 1e-10)
                {
                    // Если столбец с переменной не имеет ненулевого элемента, это может быть переопределенная система
                    Logger.LogMessage("Система имеет переопределенные уравнения, возможно бесконечно много решений.", Logger.LogLevel.WARNING);
                    return "У СЛАУ бесконечно много решений.";
                }

                // Перестановка строк для обеспечения ненулевого главного элемента
                if (maxRow != i)
                {
                    Logger.LogMessage($"Перестановка строк {i} и {maxRow}.", Logger.LogLevel.DEBUG);
                    SwapRows(matrix, i, maxRow, m + 1);
                }

                // Приведение матрицы к верхнему треугольному виду
                for (int k = i + 1; k < n; k++)
                {
                    if (matrix[k, i] != 0)
                    {
                        double factor = matrix[k, i] / matrix[i, i];
                        for (int j = i; j < m + 1; j++)
                        {
                            matrix[k, j] -= matrix[i, j] * factor;
                        }
                    }
                }
            }

            // Проверка на несовместность или бесконечно много решений
            for (int i = 0; i < n; i++)
            {
                bool isZeroRow = true;
                for (int j = 0; j < m; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        isZeroRow = false;
                        break;
                    }
                }

                if (isZeroRow && matrix[i, m] != 0)
                {
                    Logger.LogMessage("Система несовместна.", Logger.LogLevel.ERROR);
                    return "Система несовместна. У СЛАУ нет решений.";
                }

                if (isZeroRow && matrix[i, m] == 0)
                {
                    Logger.LogMessage("Бесконечно много решений.", Logger.LogLevel.WARNING);
                    return "У СЛАУ бесконечно много решений.";
                }
            }

            // Обратный ход: решение системы с верхним треугольным видом
            for (int i = n - 1; i >= 0; i--)
            {
                if (Math.Abs(matrix[i, i]) < 1e-10)
                {
                    Logger.LogMessage("Бесконечно много решений.", Logger.LogLevel.WARNING);
                    return "У СЛАУ бесконечно много решений.";
                }

                result[i] = matrix[i, m] / matrix[i, i];
                Logger.LogMessage($"Решение для переменной {i}: {result[i]}.", Logger.LogLevel.DEBUG);

                // Обновляем свободные члены
                for (int k = i - 1; k >= 0; k--)
                {
                    matrix[k, m] -= matrix[k, i] * result[i];
                }
            }
            Logger.LogMessage("Решение получено.", Logger.LogLevel.INFO);
            return result;
        }
        // Метод наименьших квадратов для решения переопределенной системы
        public static double[] SolveLeastSquares(double[,] matrix)
        {
            int n = matrix.GetLength(0);  // Количество уравнений
            int m = matrix.GetLength(1) - 1;  // Количество переменных
            Logger.LogMessage("Запуск метода наименьших квадратов.", Logger.LogLevel.INFO);
            double[,] A = new double[n, m];
            double[] b = new double[n];

            // Разделение на матрицу коэффициентов и вектор свободных членов
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++) A[i, j] = matrix[i, j];
                b[i] = matrix[i, m];
            }
            double[,] A_T = TransposeMatrix(A, n, m);
            double[,] ATA = MultiplyMatrices(A_T, A, m, n, m);
            double[] ATb = MultiplyMatrixVector(A_T, b, m, n);
            Logger.LogMessage("Решение методом наименьших квадратов через Гаусса.", Logger.LogLevel.INFO);

            // Решение методом Гаусса
            object solution = SolveGaussElimination(ATA);
            if (solution is string resultMessage)
            {
                Logger.LogMessage(resultMessage, Logger.LogLevel.ERROR);
                return null;
            }
            else if (solution is double[] resultArray)
            {
                Logger.LogMessage("Решение найдено.", Logger.LogLevel.INFO);
                return resultArray;
            }
            else
            {
                Logger.LogMessage("Не удалось решить систему.", Logger.LogLevel.ERROR);
                return null;
            }
        }

        // Перестановка строк в матрице
        private static void SwapRows(double[,] matrix, int row1, int row2, int numCols)
        {
            for (int j = 0; j < numCols; j++)
            {
                double temp = matrix[row1, j];
                matrix[row1, j] = matrix[row2, j];
                matrix[row2, j] = temp;
            }
        }

        // Транспонирование матрицы
        private static double[,] TransposeMatrix(double[,] matrix, int rows, int cols)
        {
            double[,] transposed = new double[cols, rows];
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    transposed[i, j] = matrix[j, i];
            return transposed;
        }

        // Умножение матриц
        private static double[,] MultiplyMatrices(double[,] A, double[,] B, int aRows, int aCols, int bCols)
        {
            double[,] result = new double[aCols, bCols];
            for (int i = 0; i < aCols; i++)
                for (int j = 0; j < bCols; j++)
                    for (int k = 0; k < aRows; k++)
                        result[i, j] += A[i, k] * B[k, j];
            return result;
        }

        // Умножение матрицы на вектор
        private static double[] MultiplyMatrixVector(double[,] A, double[] b, int rows, int cols)
        {
            double[] result = new double[cols];
            for (int i = 0; i < cols; i++)
            {
                result[i] = 0;
                for (int j = 0; j < rows; j++)
                    result[i] += A[i, j] * b[j];
            }
            return result;
        }
    }
}
