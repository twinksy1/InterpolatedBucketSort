# Interpolated Bucket Sort

## Introduction

The goal of this project was rooted in the desire to explore the exciting realm of sorting algorithms and push the boundaries of what's possible. 
From that journey, a new sorting algorithm—**Interpolated Bucket Sort**—was born. This project is not just about creating an algorithm; 
it's about designing an innovative approach to sorting while rigorously testing its performance, versatility, and robustness against a diverse set of challenges.

## How Interpolated Bucket Sort Works

Interpolated Bucket Sort builds on the concept of bucket sorting, enhanced with interpolation to ensure effective distribution of data into buckets. 
The algorithm works as follows:

1. **Finding the Range:** The algorithm begins by determining the minimum and maximum values in the dataset. These serve as the bounds for normalizing the data.
2. **Normalization:** Each value is normalized to a range between 0 and 1 based on the minimum and maximum values, ensuring all values map to a predictable range.
3. **Bucket Creation and Distribution:** 
    - A series of buckets are dynamically created to hold the normalized data. The bucket index is calculated using an interpolation formula.
    - Values are distributed into these buckets, ensuring a meaningful distribution for sorting.
4. **Recursive Sorting:** 
    - Buckets with more than one element are recursively sorted using the same process.
    - For trivial cases (buckets with one element or uniform values), sorting is skipped.
5. **Merging:** Finally, all sorted buckets are merged into a single, sorted array.

This approach ensures that values are logically organized before they are sorted, making the sorting process more efficient.

## Performance and Comparisons

A significant focus of this project was to evaluate how Interpolated Bucket Sort fares against established \(O(n \log n)\) algorithms like Merge Sort, Heap Sort, and Quick Sort.
The algorithm performed exceptionally well, often outpacing these alternatives in challenging scenarios like:

- Uniformly distributed data
- Nearly sorted datasets
- Large, sparse ranges of values
- Dense duplicates with intermittent outliers
- Skewed and pathological data distributions

Test cases were implemented with array sizes ranging from small (hundreds of elements) to massive datasets with up to 100,000,000 elements. 
The results showcase the algorithm's ability to handle edge cases, making it robust and practical even for extreme conditions.

## Diverse Test Cases

To validate the versatility and robustness of Interpolated Bucket Sort, the project includes a comprehensive suite of test cases:

- **Uniform Datasets:** Arrays where all elements are identical or clustered closely together.
- **Random Distributions:** Arrays with completely random data spanning the full range of integers.
- **Skewed Distributions:** Arrays where values are concentrated near the minimum or maximum.
- **Alternating Extremes:** Arrays that alternate between very high and very low values.
- **Sparse Ranges:** Arrays containing data with large gaps or sparsely populated clusters.
- **Pathological Cases:** Scenarios designed to stress-test sorting algorithms, including heavily skewed or dense duplicate data.

Each of these cases was carefully designed to test the algorithm's resilience and identify its strengths against a variety of input distributions.

## Results at a Glance

For large datasets, Interpolated Bucket Sort consistently demonstrated competitive or superior performance compared to \(O(n \log n)\) algorithms, 
achieving impressive runtime efficiency while maintaining \(O(n)\) space complexity. It excelled in scenarios where traditional algorithms, such as Quick Sort, struggle 
due to poor pivot choices or unbalanced partitions.

## Future Directions

While Interpolated Bucket Sort has demonstrated robustness and efficiency, I believe the dream of achieving a true linear sorting algorithm remains within the realm of 
possibility. The current barrier to this paradigm is the inability to precisely index elements of an array using decimal values. Once this obstacle is overcome, this algorithm
could be further enhanced by eliminating the need for both bucket logic and recursion, simplifying the process while achieving unparalleled efficiency.

## Conclusion

After dedicating countless hours to this project, I came to realize that this algorithm had already been discovered. Here is the [wikipedia](https://en.wikipedia.org/wiki/Interpolation_sort) source.
Though, this project still represents a deep dive into algorithm design and testing. Interpolated Bucket Sort combines traditional sorting concepts with innovative interpolation strategies,
resulting in a resilient and efficient sorting algorithm. Whether you're exploring new approaches to sorting or simply testing the limits of algorithmic performance, this project
is a testament to what can be achieved through curiosity and determination. 
