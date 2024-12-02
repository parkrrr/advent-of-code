using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

var lines = File.ReadAllLines("input.txt");

// load matrix
var matrix = new char[lines.Length, lines[0].Length];
for (var i = 0; i < lines.Length; i++)
{
    for (var j = 0; j < lines[i].Length; j++)
    {
        matrix[i, j] = lines[i][j];
    }
}

var rows = matrix.GetLength(0);
var columns = matrix.GetLength(1);

var validSymbols = new[] { '*', '#', '+', '$', '%', '/', '=', '-', '@', '&' };

var partNumbers = new List<int>();

// iterate
for (var i = 0; i < rows; i++)
{
    for (var j = 0; j < columns; j++)
    {
        /*
         *    i-1,j-1 | i-1  | i-1,j+1   
         *    j-1     | SPOT | j+1
         *    i+1,j-1 | i+1  | i+1,j+1
         */

        // unrolled
        var unrolled = new List<char?>(10);



        unrolled.Add(i > 0 && j > 0 ? matrix[i - 1, j - 1] : null); // top left
        unrolled.Add(i > 0 ? matrix[i - 1, j] : null); // top center
        unrolled.Add(i > 0 && j < columns -2  ? matrix[i - 1, j + 1] : null); // top right

        unrolled.Add(j > 0 ? matrix[i, j - 1] : null); // left
        unrolled.Add(matrix[i, j]); // middle (spot),
        unrolled.Add(j < columns - 1 ? matrix[i, j + 1] : null); // right

        unrolled.Add(i < rows && j > 0 ? matrix[i + 1, j - 1] : null); // bottom left
        unrolled.Add(i < rows ? matrix[i + 1, j] : null); // bottom center,
        unrolled.Add(i < rows && j < columns - 2 ? matrix[i + 1, j + 1] : null); // bottom right

        var spot = matrix[i, j];

        if (matrix[i,j] == '.')
        {
            continue;
        }

        var valid = unrolled.Any(c => c != null && validSymbols.Contains(c.Value));
        if (valid)
        {
            string wholeNumber = string.Empty;

            // walk back and determine the whole number
            if (unrolled[3] != null && unrolled[3] == '.')
            {
                // this is the first digit
                var z = 3;
                do
                {
                    wholeNumber += unrolled[z];
                } while (unrolled[z] != '.');
            }
            else
            {
                // find start of number
                var z = string.Empty;


                var numberStartPos = 0;
                var numberEndPos = 0;

                var line = Enumerable.Range(0,columns).Select(c => matrix[i, c]).ToArray();

                if (j == 0)
                {
                    numberStartPos = 0;
                }
                else
                {
                    var x = j;
                    while (x >= 0 && x <= columns)
                    {
                        if (line[x] == '.' || !char.IsNumber(line[x]))
                        {
                            numberStartPos = x + 1;
                            break;
                        }
                        else if (x == 0)
                        {
                            numberStartPos = 0;
                        }
                        else if (x >= columns)
                        {
                            // this should not happen
                            Debug.Assert(false);
                        }

                        x--;
                    }
                }

                if (j == columns - 1)
                {
                    numberEndPos = columns - 1;
                }
                else
                {
                    var x = j;
                    while (x > 0 && x <= columns)
                    {
                        x++;

                        if (line[x] == '.' || !char.IsNumber(line[x]))
                        {
                            numberEndPos = x;
                            break;
                        }
                    }
                }

                var subString = line[numberStartPos..numberEndPos];
                Console.WriteLine(subString);

            }

            //var partNumber = int.Parse(wholeNumber);
            //if (!partNumbers.Contains(partNumber))
            //{
            //    partNumbers.Add(partNumber);
            //}
        }
    }

    Console.WriteLine();
}

