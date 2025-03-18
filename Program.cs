using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
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

        // Fails this test case, too large :(
        //int[] testCase40 = new int[100000000];

        //for (int i = 0; i < testCase40.Length; i++)
        //{
        //    testCase40[i] = random.Next(int.MinValue, int.MaxValue);
        //}

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
        RunTestCase(testCase24, "Test Case 24: Large Increasing Array");
        RunTestCase(testCase25, "Test Case 25: Large Decreasing Array");
        RunTestCase(testCase26, "Test Case 26: Repeated Values Clustered");
        RunTestCase(testCase27, "Test Case 27: Values Near Max");
        RunTestCase(testCase28, "Test Case 28: Small Random Array");
        RunTestCase(testCase29, "Test Case 29: Random Wide Range");
        RunTestCase(testCase30, "Test Case 30: 1,000,000 Random numbers from Int.Min to Int.Max");
        RunTestCase(testCase31, "Test Case 31: Split Between Int.Min and Int.Max");
        RunTestCase(testCase32, "Test Case 32: Alternating Min and Max");
        RunTestCase(testCase33, "Test Case 33: Dense Duplicates with One Outlier");
        RunTestCase(testCase34, "Test Case 34: Sorted Blocks");
        RunTestCase(testCase35, "Test Case 35: Sparse Range");
        RunTestCase(testCase36, "Test Case 36: Zigzag Wide Range");
        RunTestCase(testCase37, "Test Case 37: Nearly Sorted with Random Inserts");
        RunTestCase(testCase38, "Test Case 38: Simulated Floats (Large Dataset)");
        RunTestCase(testCase39, "Test Case 39: 10,000,000 Random numbers from Int.Min to Int.Max");
        //RunTestCase(testCase40, "Test Case 40: 100,000,000 Random numbers from Int.Min to Int.Max");
    }

    static void RunTestCase(int[] values, string testCaseName)
    {
        Console.WriteLine(testCaseName);
        // Your sorting algorithm
        Stopwatch sw = Stopwatch.StartNew();
        var interpolatedSort = new Sorters.InterpolatedBucketSort();
        var sortedList = interpolatedSort.Sort(values);
        sw.Stop();
        var interpolateSortElapsedTicks = sw.ElapsedTicks;

        var mergeSort = new Sorters.MergeSort();
        sw = Stopwatch.StartNew();
        _ = mergeSort.Sort(values);
        sw.Stop();
        var mergeSortElapsedTicks = sw.ElapsedTicks;

        var heapSort = new Sorters.HeapSort();
        sw = Stopwatch.StartNew();
        _ = heapSort.Sort(values);
        sw.Stop();
        var heapSortElapsedTicks = sw.ElapsedTicks;

        var quickSort = new Sorters.QuickSort();
        sw = Stopwatch.StartNew();
        // Quick sort isn't very good with super large datasets :(
        _ = quickSort.Sort(values);
        sw.Stop();
        var quickSortElapsedTicks = sw.ElapsedTicks;

        Console.WriteLine($"Elapsed ticks - interpolated bucket sort:{interpolateSortElapsedTicks}, merge sort:{mergeSortElapsedTicks}, heap sort:{heapSortElapsedTicks}, quick sort:{quickSortElapsedTicks}");

        // Built-in sorting for validation
        int[] expected = values.ToArray();
        Array.Sort(expected);

        Console.WriteLine(testCaseName);
        //Console.WriteLine("Original List: " + string.Join(", ", values));

        // Compare and print the results
        if (sortedList.SequenceEqual(expected))
        {
            // Results match; print in green
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Sorted List: " + string.Join(", ", sortedList));
        }
        else
        {
            // Results differ; highlight mismatches in red
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Sorted List: ");
            for (int i = 0; i < sortedList.Length; i++)
            {
                if (i >= expected.Length || sortedList[i] != expected[i])
                {
                    // Highlight mismatched element
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(sortedList[i] + " ");
                }
                else
                {
                    // Correct element
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(sortedList[i] + " ");
                }
            }
            Console.WriteLine();
        }

        // Reset console color
        Console.ResetColor();
        Console.WriteLine();
    }
}
