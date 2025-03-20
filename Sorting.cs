using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorters
{
    internal interface Sorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] array);
    }

    public class InterpolatedBucketSort<T> : Sorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] array)
        {
            return InterpolatedBucketSortImpl(array);
        }

        private static T[] InterpolatedBucketSortImpl(T[] values)
        {
            int n = values.Length;
            if (n <= 1) return values;

            T minValue = values[0];
            T maxValue = values[0];

            // Step 1: Find min and max values
            foreach (var val in values)
            {
                if (val.CompareTo(minValue) < 0) minValue = val;
                if (val.CompareTo(maxValue) > 0) maxValue = val;
            }
            if (minValue.CompareTo(maxValue) == 0) return values; // All values are identical

            // Step 2: Determine dynamic bucket count
            int bucketCount = Math.Max(Math.Min(n / 100, 1000), 10);

            // Step 3: Cache normalized values
            double[] normalizedValues = new double[n];
            for (int i = 0; i < n; i++)
            {
                normalizedValues[i] = Math.Clamp((double)(Convert.ToDouble(values[i]) - Convert.ToDouble(minValue)) /
                                                 (Convert.ToDouble(maxValue) - Convert.ToDouble(minValue)), 0.0, 1.0);
            }

            // Step 4: Create buckets
            int[] bucketSizes = new int[bucketCount];
            for (int i = 0; i < n; i++)
            {
                int bucketIndex = (int)(normalizedValues[i] * bucketCount);
                bucketIndex = Math.Min(bucketIndex, bucketCount - 1);
                bucketSizes[bucketIndex]++;
            }

            T[][] buckets = new T[bucketCount][];
            for (int i = 0; i < bucketCount; i++)
            {
                buckets[i] = new T[bucketSizes[i]];
            }

            int[] bucketCounters = new int[bucketCount];
            for (int i = 0; i < n; i++)
            {
                int bucketIndex = (int)(normalizedValues[i] * bucketCount);
                bucketIndex = Math.Min(bucketIndex, bucketCount - 1);
                buckets[bucketIndex][bucketCounters[bucketIndex]++] = values[i];
            }

            // Step 5: Sort buckets recursively
            T[] sortedValues = new T[n];
            int idx = 0;

            for (int i = 0; i < bucketCount; i++)
            {
                if (bucketSizes[i] > 0)
                {
                    if (bucketSizes[i] == 1)
                    {
                        sortedValues[idx++] = buckets[i][0];
                    }
                    else
                    {
                        T[] sortedBucket = InterpolatedBucketSortImpl(buckets[i]);
                        foreach (var val in sortedBucket)
                        {
                            sortedValues[idx++] = val;
                        }
                    }
                }
            }

            return sortedValues;
        }
    }

    public class MergeSort<T> : Sorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] array)
        {
            if (array.Length <= 1)
                return array;

            int mid = array.Length / 2;
            T[] left = array.Take(mid).ToArray();
            T[] right = array.Skip(mid).ToArray();

            left = Sort(left);
            right = Sort(right);

            return Merge(left, right);
        }

        private T[] Merge(T[] left, T[] right)
        {
            List<T> result = new List<T>();
            int i = 0, j = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i].CompareTo(right[j]) <= 0)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }

            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));

            return result.ToArray();
        }
    }

    public class HeapSort<T> : Sorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] array)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                Heapify(array, i, 0);
            }

            return array;
        }

        private void Heapify(T[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && array[left].CompareTo(array[largest]) > 0)
                largest = left;

            if (right < n && array[right].CompareTo(array[largest]) > 0)
                largest = right;

            if (largest != i)
            {
                (array[i], array[largest]) = (array[largest], array[i]);
                Heapify(array, n, largest);
            }
        }
    }

    public class QuickSort<T> : Sorter<T> where T : IComparable<T>
    {
        public T[] Sort(T[] array)
        {
            if (array == null || array.Length <= 1)
            {
                return array; // Already sorted or empty
            }

            QuickSortImpl(array, 0, array.Length - 1);
            return array;
        }

        private void QuickSortImpl(T[] array, int low, int high)
        {
            // Create a stack to simulate recursion
            Stack<(int Low, int High)> stack = new Stack<(int, int)>();

            // Push the initial range onto the stack
            stack.Push((low, high));

            while (stack.Count > 0)
            {
                // Pop the range to process
                var (start, end) = stack.Pop();

                if (start < end)
                {
                    // Partition the array and get the pivot index using Median of Three
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

        private int MedianOfThreePartition(T[] array, int low, int high)
        {
            // Calculate the middle index
            int mid = low + (high - low) / 2;

            // Find the median of the first, middle, and last elements
            if (array[low].CompareTo(array[mid]) > 0)
            {
                (array[low], array[mid]) = (array[mid], array[low]);
            }
            if (array[low].CompareTo(array[high]) > 0)
            {
                (array[low], array[high]) = (array[high], array[low]);
            }
            if (array[mid].CompareTo(array[high]) > 0)
            {
                (array[mid], array[high]) = (array[high], array[mid]);
            }

            // Use the median as the pivot by swapping it with the last element
            (array[mid], array[high]) = (array[high], array[mid]);

            // Perform partitioning using the pivot
            T pivot = array[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (array[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    (array[i], array[j]) = (array[j], array[i]); // Swap elements
                }
            }

            // Place the pivot in the correct position
            (array[i + 1], array[high]) = (array[high], array[i + 1]);
            return i + 1; // Return the pivot index
        }
    }
}
