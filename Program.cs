using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        RunIntTests();   
    }

    static void RunTestCase<T>(T[] values, string testCaseName, bool useTicks = true) where T : IComparable<T>
    {
        Console.WriteLine(testCaseName);
        // Your sorting algorithm
        Stopwatch sw = Stopwatch.StartNew();
        var interpolatedSort = new Sorters.InterpolatedBucketSort<T>();
        var sortedList = interpolatedSort.Sort(values);
        sw.Stop();
        var interpolateSortElapsed = useTicks ? sw.ElapsedTicks : sw.ElapsedMilliseconds;

        var mergeSort = new Sorters.MergeSort<T>();
        sw = Stopwatch.StartNew();
        _ = mergeSort.Sort(values);
        sw.Stop();
        var mergeSortElapsed = useTicks ? sw.ElapsedTicks : sw.ElapsedMilliseconds;

        var heapSort = new Sorters.HeapSort<T>();
        sw = Stopwatch.StartNew();
        _ = heapSort.Sort(values);
        sw.Stop();
        var heapSortElapsed = useTicks ? sw.ElapsedTicks : sw.ElapsedMilliseconds;

        // Commented out for now since this algorithm slows down too much with some of the heavy test cases
        //var quickSort = new Sorters.QuickSort<T>();
        //sw = Stopwatch.StartNew();
        //// Quick sort isn't very good with super large datasets :(
        //_ = quickSort.Sort(values);
        //sw.Stop();
        //var quickSortElapsed = useTicks ? sw.ElapsedTicks : sw.ElapsedMilliseconds;

        Console.WriteLine($"Elapsed {( useTicks ? "ticks" : "MS" )} - interpolated bucket sort:{interpolateSortElapsed}, merge sort:{mergeSortElapsed}, heap sort:{heapSortElapsed}, quick sort:disabled");

        // Built-in sorting for validation
        T[] expected = values.ToArray();
        Array.Sort(expected);

        CompareResults(sortedList, expected);
    }

    private static void CompareResults<T>(T[] output, T[] expected) where T : IComparable<T>
    {
        // Compare and print the results
        if (output.SequenceEqual(expected))
        {
            // Results match; print in green
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Sorted List: " + string.Join(", ", output));
        }
        else
        {
            // Results differ; highlight mismatches in red
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Sorted List: ");
            for (int i = 0; i < output.Length; i++)
            {
                if (i >= expected.Length || !output[i].Equals(expected[i]))
                {
                    // Highlight mismatched element
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(output[i] + " ");
                }
                else
                {
                    // Correct element
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(output[i] + " ");
                }
            }
            Console.WriteLine();
        }

        // Reset console color
        Console.ResetColor();
        Console.WriteLine();
    }

    private static void RunIntTests()
    {
        // Test cases
        int[] testCase1 = { 10, 20, 5, 15, 25 };  // Random unsorted list
        int[] testCase2 = { 3, 8, 2, 5, 10 };     // Another random list
        int[] testCase3 = { 1, 2, 3, 4, 5 };     // Already sorted list
        int[] testCase4 = { 5, 4, 3, 2, 1 };     // Reverse sorted list
        int[] testCase5 = { 42 };
        int[] testCase6 = { 7, 7, 7, 7, 7 };
        int[] testCase7 = { 1, 2, 2, 1, 3 };
        int[] testCase8 = Enumerable.Range(1, 100).ToArray();
        int[] testCase9 = Enumerable.Range(1, 100).Reverse().ToArray();
        int[] testCase10 = { 42, 17, 8, 99, 23, 56, 3, 77, 12, 88 };
        int[] testCase11 = { -10, -20, -5, -15, -25 };
        int[] testCase12 = { -3, 8, -2, 5, 10 };
        int[] testCase13 = { 1, 100, 2, 99, 3, 98, 4, 97 };
        int[] testCase14 = { 10, 1, 5, 2, 7, 3, 8, 4, 6, 9 };
        // Edge Cases
        int[] testCase15 = { -5 };                     // Single negative value
        int[] testCase16 = { 100, -100 };              // Two elements (reverse)
        int[] testCase17 = { 1, 1, 1, 1 };             // All same values
        int[] testCase18 = { -1000, 1000 };            // Min and Max only

        // Duplicates and Patterns
        int[] testCase19 = { 1, 3, 1, 3, 1, 3, 1, 3 }; // Alternating pattern
        int[] testCase20 = { 5, 5, 5, 10, 10, 10, 1, 1, 1 }; // Blocks of duplicates

        // Negative and Mixed Values
        int[] testCase21 = { -5, -10, -3, -1, -20 };   // All negative values
        int[] testCase22 = { -3, 0, 2, -1, 1, -2, 3 }; // Mixed positive and negative

        // Large Ranges
        int[] testCase23 = { -100000, 100000, 0, -50000, 50000 }; // Sparse distribution

        // Stress Testing with Large Arrays
        int[] testCase24 = Enumerable.Range(1, 10000).ToArray();  // Large increasing array
        int[] testCase25 = Enumerable.Range(1, 10000).Reverse().ToArray(); // Large decreasing array

        // Extreme Collision Scenarios
        int[] testCase26 = { 1, 1, 1, 1, 100, 100, 100, 100 }; // Repeated values clustered
        int[] testCase27 = { 98, 99, 100, 99, 98, 97, 96 };     // All values near max

        // Random Arrays
        int[] testCase28 = { 42, 17, 88, 23, 56, 3, 77, 12 };  // Small random array
        int[] testCase29 = { 1000, -500, 0, 500, -1000, 300, 200, 900 }; // Random wide range
        int[] testCase30 = new int[1000000];
        // Random number generator
        Random random = new Random();

        for (int i = 0; i < testCase30.Length; i++)
        {
            // Generate a random integer between int.MinValue and int.MaxValue
            testCase30[i] = random.Next(int.MinValue, int.MaxValue);
        }

        // Extreme Test Cases
        int[] testCase31 = new int[2000]; // 1000 values near int.Min, 1000 near int.Max
        for (int i = 0; i < 1000; i++)
        {
            testCase31[i] = random.Next(int.MinValue, int.MinValue / 2);
        }
        for (int i = 1000; i < 2000; i++)
        {
            testCase31[i] = random.Next(int.MaxValue / 2, int.MaxValue);
        }

        int[] testCase32 = Enumerable.Range(0, 1000)
            .Select((_, i) => i % 2 == 0 ? int.MinValue : int.MaxValue).ToArray(); // Alternating Min and Max

        int[] testCase33 = Enumerable.Repeat(42, 1000).ToArray(); // Dense duplicates with an outlier
        testCase33[999] = random.Next(int.MinValue, int.MaxValue); // Set the outlier

        int[] testCase34 = Enumerable.Range(1, 1000).ToArray(); // Sorted blocks
        for (int i = 0; i < 10; i++) Array.Reverse(testCase34, i * 100, 100);

        int[] testCase35 = new int[1000]; // Sparse range
        for (int i = 0; i < testCase35.Length; i++)
        {
            testCase35[i] = random.Next(int.MinValue, int.MaxValue) / random.Next(10, 1000);
        }

        int[] testCase36 = new int[2000]; // Zigzag wide range
        for (int i = 0; i < 1000; i++)
        {
            testCase36[2 * i] = i * 1000; // Increasing
            testCase36[2 * i + 1] = int.MaxValue - i * 1000; // Decreasing
        }

        int[] testCase37 = Enumerable.Range(1, 1000).ToArray(); // Nearly sorted with random inserts
        for (int i = 0; i < testCase37.Length / 10; i++)
        {
            int idx1 = random.Next(0, testCase37.Length);
            int idx2 = random.Next(0, testCase37.Length);
            (testCase37[idx1], testCase37[idx2]) = (testCase37[idx2], testCase37[idx1]);
        }

        int[] testCase38 = new int[100000]; // Large dataset simulating floats
        for (int i = 0; i < testCase38.Length; i++)
        {
            testCase38[i] = random.Next(-100000, 100000) * random.Next(1, 1000);
        }

        int[] testCase39 = new int[10000000];

        for (int i = 0; i < testCase39.Length; i++)
        {
            testCase39[i] = random.Next(int.MinValue, int.MaxValue);
        }

        int[] testCase40 = new int[100000000];

        for (int i = 0; i < testCase40.Length; i++)
        {
            testCase40[i] = random.Next(int.MinValue, int.MaxValue);
        }
        // 41. Larger Random Unsorted (10,000,000 elements)
        int[] testCase41 = Enumerable.Range(1, 10000000).OrderBy(_ => random.Next()).ToArray();

        // 42. Larger Random Small Range (500,000 elements)
        int[] testCase42 = Enumerable.Range(1, 500000).Select(i => i % 10).ToArray();

        // 43. Larger Zigzag Wide Range (2,000,000 elements)
        int[] testCase43 = Enumerable.Range(1, 1000000).SelectMany(i => new[] { i, int.MaxValue - i }).ToArray();

        // 44. Larger Repeated Blocks (1,000,000 elements)
        int[] testCase44 = Enumerable.Repeat(Enumerable.Range(1, 1000), 1000).SelectMany(x => x).ToArray();

        // 45. Extreme Alternating Min and Max (10,000,000 elements)
        int[] testCase45 = Enumerable.Range(1, 10000000).Select(i => i % 2 == 0 ? int.MinValue : int.MaxValue).ToArray();

        // 46. Larger Sparse Distribution (10,000,000 elements)
        int[] testCase46 = Enumerable.Range(1, 10000000).Select(_ => random.Next(int.MinValue, int.MaxValue) / random.Next(1, 1000)).ToArray();

        // 47. Dense Dataset with Small Random Noise (10,000,000 elements)
        int[] testCase47 = Enumerable.Repeat(42, 9999000)
                                      .Concat(Enumerable.Range(1, 1000).Select(_ => random.Next(-1000, 1000)))
                                      .ToArray();

        // 48. Larger Nearly Sorted with Noise (5,000,000 elements)
        int[] testCase48 = Enumerable.Range(1, 5000000)
                                      .Select((val, idx) => idx % 1000 == 0 ? val + random.Next(-50, 50) : val)
                                      .ToArray();

        // 49. Larger Simulated Floats (10,000,000 elements)
        int[] testCase49 = Enumerable.Range(1, 10000000).Select(_ => random.Next(-1000000, 1000000) * random.Next(1, 1000)).ToArray();

        // 50. All Negative Large Dataset (5,000,000 elements)
        int[] testCase50 = Enumerable.Range(-5000000, 5000000).ToArray();

        // 51. Wide Range Random Dataset (10,000,000 elements)
        int[] testCase51 = Enumerable.Range(1, 10000000).Select(_ => random.Next(int.MinValue, int.MaxValue)).ToArray();

        // 52. Dense Duplicates with Intermittent High Values (1,000,000 elements)
        int[] testCase52 = Enumerable.Repeat(10, 900000)
                                      .Concat(Enumerable.Repeat(10000, 100000))
                                      .OrderBy(_ => random.Next()).ToArray();

        // 53. Large Sorted Blocks with Noise (10,000,000 elements)
        int[] testCase53 = Enumerable.Range(1, 1000000).Select(i => i * 10)
                                      .Concat(Enumerable.Range(1, 1000000).Select(i => i * 10 + random.Next(-5, 5)))
                                      .ToArray();

        // 54. Uniform Dataset (10,000,000 elements)
        int[] testCase54 = Enumerable.Repeat(999999, 10000000).ToArray();

        // 55. Large Mixed Extremes (10,000,000 elements)
        int[] testCase55 = Enumerable.Range(1, 5000000)
                                      .SelectMany(i => new[] { int.MinValue + i, int.MaxValue - i }).ToArray();

        // 56. Alternating Positive and Negative Dataset (5,000,000 elements)
        int[] testCase56 = Enumerable.Range(-2500000, 5000000).OrderBy(_ => random.Next() % 2).ToArray();

        // 57. Single Large Bucket Scenario (100,000,000 elements, concentrated near zero)
        int[] testCase57 = Enumerable.Repeat(0, 99999999)
                                     .Concat(new[] { int.MaxValue }) // One extreme outlier
                                     .ToArray();

        // 58. Skewed Distribution (100,000,000 elements, close to minValue)
        int[] testCase58 = Enumerable.Range(1, 100000000)
                                     .Select(_ => random.Next(int.MinValue, int.MinValue / 2))
                                     .ToArray();

        // 59. Sparse Gaps (100,000,000 elements, large ranges with gaps)
        int[] testCase59 = Enumerable.Range(1, 100000000)
                                     .Select(i => i % 2 == 0 ? random.Next(-1000000, 1000000) : random.Next(10000000, 20000000))
                                     .ToArray();

        // 60. Alternating Small and Large Values (100,000,000 elements)
        int[] testCase60 = Enumerable.Range(1, 50000000)
                                     .SelectMany(i => new[] { int.MinValue + i, int.MaxValue - i })
                                     .ToArray();

        // 61. Uniformly Random Distribution (100,000,000 elements)
        int[] testCase61 = Enumerable.Range(1, 100000000)
                                     .Select(_ => random.Next(int.MinValue, int.MaxValue))
                                     .ToArray();

        RunTestCase(testCase1, "Test Case 1: Random Unsorted");
        RunTestCase(testCase2, "Test Case 2: Another Random");
        RunTestCase(testCase3, "Test Case 3: Already Sorted");
        RunTestCase(testCase4, "Test Case 4: Reverse Sorted");
        RunTestCase(testCase5, "Test Case 5: Single Element");
        RunTestCase(testCase6, "Test Case 6: Duplicate Elements");
        RunTestCase(testCase7, "Test Case 7: Small Range");
        RunTestCase(testCase8, "Test Case 8: Increasing Large Range");
        RunTestCase(testCase9, "Test Case 9: Decreasing Large Range");
        RunTestCase(testCase10, "Test Case 10: Random Dataset");
        RunTestCase(testCase11, "Test Case 11: Negative Values");
        RunTestCase(testCase12, "Test Case 12: Mixed Positive and Negative Values");
        RunTestCase(testCase13, "Test Case 13: Repeating Patterns");
        RunTestCase(testCase14, "Test Case 14: Zigzag Distribution");
        RunTestCase(testCase15, "Test Case 15: Single Negative Value");
        RunTestCase(testCase16, "Test Case 16: Two Elements (Reverse)");
        RunTestCase(testCase17, "Test Case 17: All Same Values");
        RunTestCase(testCase18, "Test Case 18: Min and Max Only");
        RunTestCase(testCase19, "Test Case 19: Alternating Pattern");
        RunTestCase(testCase20, "Test Case 20: Blocks of Duplicates");
        RunTestCase(testCase21, "Test Case 21: All Negative Values");
        RunTestCase(testCase22, "Test Case 22: Mixed Positive and Negative Values");
        RunTestCase(testCase23, "Test Case 23: Sparse Distribution");
        RunTestCase(testCase24, "Test Case 24: Large Increasing Array", false);
        RunTestCase(testCase25, "Test Case 25: Large Decreasing Array", false);
        RunTestCase(testCase26, "Test Case 26: Repeated Values Clustered");
        RunTestCase(testCase27, "Test Case 27: Values Near Max");
        RunTestCase(testCase28, "Test Case 28: Small Random Array");
        RunTestCase(testCase29, "Test Case 29: Random Wide Range");
        RunTestCase(testCase30, "Test Case 30: 1,000,000 Random numbers from Int.Min to Int.Max", false);
        RunTestCase(testCase31, "Test Case 31: Split Between Int.Min and Int.Max");
        RunTestCase(testCase32, "Test Case 32: Alternating Min and Max");
        RunTestCase(testCase33, "Test Case 33: Dense Duplicates with One Outlier");
        RunTestCase(testCase34, "Test Case 34: Sorted Blocks");
        RunTestCase(testCase35, "Test Case 35: Sparse Range");
        RunTestCase(testCase36, "Test Case 36: Zigzag Wide Range");
        RunTestCase(testCase37, "Test Case 37: Nearly Sorted with Random Inserts");
        RunTestCase(testCase38, "Test Case 38: Simulated Floats (Large Dataset", false);
        RunTestCase(testCase39, "Test Case 39: 10,000,000 Random numbers from Int.Min to Int.Max", false);
        RunTestCase(testCase40, "Test Case 40: 100,000,000 Random numbers from Int.Min to Int.Max", false);
        RunTestCase(testCase41, "Test Case 41: Larger Random Unsorted", false);
        RunTestCase(testCase42, "Test Case 42: Larger Random Small Range");
        RunTestCase(testCase43, "Test Case 43: Larger Zigzag Wide Range", false);
        RunTestCase(testCase44, "Test Case 44: Larger Repeated Blocks", false);
        RunTestCase(testCase45, "Test Case 45: Extreme Alternating Min and Max", false);
        RunTestCase(testCase46, "Test Case 46: Larger Sparse Distribution", false);
        RunTestCase(testCase47, "Test Case 47: Dense Dataset with Small Random Noise", false);
        RunTestCase(testCase48, "Test Case 48: Larger Nearly Sorted with Noise", false);
        RunTestCase(testCase49, "Test Case 49: Larger Simulated Floats", false);
        RunTestCase(testCase50, "Test Case 50: All Negative Large Dataset", false);
        RunTestCase(testCase51, "Test Case 51: Wide Range Random Dataset", false);
        RunTestCase(testCase52, "Test Case 52: Dense Duplicates with Intermittent High Values");
        RunTestCase(testCase53, "Test Case 53: Large Sorted Blocks with Noise", false);
        RunTestCase(testCase54, "Test Case 54: Uniform Dataset", false);
        RunTestCase(testCase55, "Test Case 55: Large Mixed Extremes", false);
        RunTestCase(testCase56, "Test Case 56: Alternating Positive and Negative Dataset", false);
        RunTestCase(testCase57, "Test Case 57: Single Large Bucket Scenario", false);
        RunTestCase(testCase58, "Test Case 58: Skewed Distribution", false);
        RunTestCase(testCase59, "Test Case 59: Sparse Gaps", false);
        RunTestCase(testCase60, "Test Case 60: Alternating Small and Large Values", false);
        RunTestCase(testCase61, "Test Case 61: Uniformly Random Distribution", false);
    }
}
