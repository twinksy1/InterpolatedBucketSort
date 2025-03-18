using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorters
{
    internal interface Sorter
    {
        public int[] Sort(int[] array);
    }

    public class InterpolatedBucketSort : Sorter
    {
        public int[] Sort(int[] array)
        {
            return RecursiveInterpolationSortWithBuckets(array);
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

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }
                if (values[i] > max)
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

    public class MergeSort : Sorter
    {
        public int[] Sort(int[] array)
        {
            if (array.Length <= 1)
                return array;

            int mid = array.Length / 2;

            // Divide the array into two halves
            int[] left = array.Take(mid).ToArray();
            int[] right = array.Skip(mid).ToArray();

            // Recursively sort both halves
            left = Sort(left);
            right = Sort(right);

            // Merge the sorted halves
            return Merge(left, right);
        }

        private int[] Merge(int[] left, int[] right)
        {
            List<int> result = new List<int>();
            int i = 0, j = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }

            // Add remaining elements from left or right
            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));

            return result.ToArray();
        }
    }

    public class HeapSort : Sorter
    {
        public int[] Sort(int[] array)
        {
            int n = array.Length;

            // Build the max-heap
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            // Extract elements from the heap
            for (int i = n - 1; i >= 0; i--)
            {
                // Move current root to the end
                (array[0], array[i]) = (array[i], array[0]);

                // Heapify the reduced heap
                Heapify(array, i, 0);
            }

            return array;
        }

        private void Heapify(int[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            // Check if left child is larger than root
            if (left < n && array[left] > array[largest])
                largest = left;

            // Check if right child is larger than root
            if (right < n && array[right] > array[largest])
                largest = right;

            // Swap and recursively heapify
            if (largest != i)
            {
                (array[i], array[largest]) = (array[largest], array[i]);
                Heapify(array, n, largest);
            }
        }
    }

    public class QuickSort : Sorter
    {
        public int[] Sort(int[] array)
        {
            if (array == null || array.Length <= 1)
            {
                return array; // Already sorted or empty
            }

            QuickSortImpl(array, 0, array.Length - 1);
            return array;
        }

        private void QuickSortImpl(int[] array, int low, int high)
        {
            // Create a stack to simulate recursion
            Stack<(int Low, int High)> stack = new Stack<(int, int)>();

            // Push the initial range onto the stack
            stack.Push((low, high));

            while (stack.Count > 0)
            {
                // Pop the range to process
                var (start, end) = stack.Pop();

                // Only proceed if the range is valid
                if (start < end)
                {
                    // Partition the array and get the pivot index
                    int pivotIndex = MedianOfThreePartition(array, start, end);

                    // Push the sub-range for elements left of the pivot
                    if (pivotIndex - 1 > start)
                    {
                        stack.Push((start, pivotIndex - 1));
                    }

                    // Push the sub-range for elements right of the pivot
                    if (pivotIndex + 1 < end)
                    {
                        stack.Push((pivotIndex + 1, end));
                    }
                }
            }
        }

        private int MedianOfThreePartition(int[] array, int low, int high)
        {
            // Calculate the middle index
            int mid = low + (high - low) / 2;

            // Find the median of the first, middle, and last elements
            if (array[low] > array[mid])
            {
                (array[low], array[mid]) = (array[mid], array[low]);
            }
            if (array[low] > array[high])
            {
                (array[low], array[high]) = (array[high], array[low]);
            }
            if (array[mid] > array[high])
            {
                (array[mid], array[high]) = (array[high], array[mid]);
            }

            // Use the median as the pivot by swapping it with the last element
            (array[mid], array[high]) = (array[high], array[mid]);

            // Perform partitioning using the pivot
            int pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    // Swap array[i] and array[j]
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }

            // Place the pivot in the correct position
            (array[i + 1], array[high]) = (array[high], array[i + 1]);
            return i + 1; // Return the pivot index
        }
    }

}
