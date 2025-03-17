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
    }

    static void RunTestCase(int[] values, string testCaseName)
    {
        // Your sorting algorithm
        Stopwatch sw1 = Stopwatch.StartNew();
        int[] sortedList = RecursiveInterpolationSortWithBuckets(values);
        sw1.Stop();

        // Built-in sorting for validation
        int[] expected = values.ToArray(); // Make a copy
        Stopwatch sw2 = Stopwatch.StartNew();
        Array.Sort(expected);
        sw2.Stop();

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
        Console.WriteLine($"Elapsed ticks - csharp:{sw2.ElapsedTicks}, custom algo:{sw1.ElapsedTicks}");
        Console.WriteLine();
    }

    static int[] RecursiveInterpolationSortWithBuckets(int[] values)
    {
        int n = values.Length;

        // Base case: If the array is small enough, return it sorted
        if (n <= 1)
        {
            return values;
        }

        int min = values[0];
        int max = values[0];

        for(int i=0; i<values.Length; i++)
        {
            if(values[i] < min)
            {
                min = values[i];
            }
            if(values[i] > max)
            {
                max = values[i];
            }
        }

        // Handle edge case where all values are the same
        if (min == max)
        {
            return values; // Already sorted
        }

        // Step 2: Create buckets
        List<int>[] buckets = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            buckets[i] = new List<int>();
        }

        // Step 3: Assign values to buckets
        foreach (int value in values)
        {
            // Compute the normalized position, i.e. the min-max interpolation formula
            double normalizedValue = (double)(value - min) / (max - min);
            double rawPosition = normalizedValue * (n - 1);

            // Map to bucket index (clamped to valid range)
            int bucketIndex = (int)Math.Floor(rawPosition);
            bucketIndex = Math.Max(0, Math.Min(bucketIndex, n - 1));

            // Insert value into the corresponding bucket
            buckets[bucketIndex].Add(value);
        }

        // Step 4: Recursively sort each bucket
        for (int i = 0; i < buckets.Length; i++)
        {
            if (buckets[i].Count > 1)
            {
                // Recursive call to sort the bucket
                buckets[i] = RecursiveInterpolationSortWithBuckets(buckets[i].ToArray()).ToList();
            }
        }

        // Step 5: Flatten the buckets into the output array
        List<int> result = new List<int>();
        foreach (var bucket in buckets)
        {
            result.AddRange(bucket);
        }

        return result.ToArray();
    }
}
