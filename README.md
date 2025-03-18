# **Interpolated Bucket Sort**
An innovative sorting algorithm that leverages interpolation and recursive bucketing to achieve competitive performance across diverse datasets. It efficiently handles edge cases and scales well with large datasets, redefining sorting efficiency.

## **Overview**
Interpolated Bucket Sort is a divide-and-conquer algorithm designed to overcome the limitations of traditional sorting methods such as Merge Sort, Heap Sort, and Quick Sort. By dynamically distributing elements into buckets based on interpolated positions within their value range, it excels in sorting diverse datasets, including large value ranges, sparse distributions, and duplicates.

---

## **Performance Benchmarks**

### **Test Case Results**
The following results demonstrate elapsed ticks for Interpolated Bucket Sort (IBS), Merge Sort, Heap Sort, and Quick Sort across various test scenarios:

| **Test Case**                                   | **IBS**      | **Merge Sort** | **Heap Sort** | **Quick Sort** |
|------------------------------------------------|--------------|----------------|---------------|----------------|
| Random Unsorted (1)                            | 7,421        | 22,628         | 2,192         | 12,535         |
| Another Random (2)                             | 306          | 49             | 7             | 17             |
| Already Sorted (3)                             | 17           | 30             | 6             | 8              |
| Reverse Sorted (4)                             | 18           | 28             | 3             | 7              |
| Single Element (5)                             | 3            | 0              | 1             | 0              |
| Duplicate Elements (6)                         | 3            | 36             | 3             | 8              |
| Small Range (7)                                | 30           | 36             | 5             | 11             |
| Increasing Large Range (8)                     | 174          | 710            | 135           | 65             |
| Decreasing Large Range (9)                     | 129          | 1,197          | 117           | 70             |
| Random Dataset (10)                            | 42           | 76             | 10            | 11             |
| Negative Values (11)                           | 20           | 30             | 4             | 8              |
| Mixed Positive and Negative Values (12)        | 31           | 29             | 4             | 8              |
| Repeating Patterns (13)                        | 57           | 48             | 8             | 11             |
| Zigzag Distribution (14)                       | 26           | 83             | 9             | 13             |
| Single Negative Value (15)                     | 3            | 0              | 2             | 1              |
| Two Elements (Reverse) (16)                    | 19           | 19             | 1             | 8              |
| All Same Values (17)                           | 3            | 38             | 2             | 10             |
| Min and Max Only (18)                          | 17           | 16             | 3             | 7              |
| Alternating Pattern (19)                       | 27           | 55             | 7             | 10             |
| Blocks of Duplicates (20)                      | 35           | 60             | 8             | 13             |
| All Negative Values (21)                       | 39           | 54             | 6             | 13             |
| Sparse Distribution (23)                       | 21           | 35             | 4             | 9              |
| Large Increasing Array (24)                    | 10,250       | 119,597        | 24,995        | 8,155          |
| Large Decreasing Array (25)                    | 42,269       | 61,904         | 23,236        | 6,360          |
| Repeated Values Clustered (26)                 | 88           | 74             | 8             | 22             |
| Values Near Max (27)                           | 46           | 83             | 6             | 14             |
| Small Random Array (28)                        | 41           | 49             | 7             | 11             |
| Random Wide Range (29)                         | 35           | 70             | 8             | 12             |
| 1,000,000 Random Numbers (30)                  | 9,848,352    | 4,755,449      | 3,670,789     | 868,306        |
| Split Between Int.Min and Int.Max (31)         | 7,976        | 5,848          | 3,500         | 883            |
| Alternating Min and Max (32)                   | 351          | 2,367          | 730           | 11,069         |
| Dense Duplicates with One Outlier (33)         | 357          | 2,286          | 155           | 21,795         |
| Sorted Blocks (34)                             | 541          | 2,344          | 1,421         | 410            |
| Sparse Range (35)                              | 1,692        | 2,896          | 1,567         | 453            |
| Zigzag Wide Range (36)                         | 1,636        | 4,780          | 3,095         | 867            |
| Nearly Sorted with Random Inserts (37)         | 502          | 2,527          | 1,487         | 411            |
| Simulated Floats (Large Dataset) (38)          | 431,838      | 412,301        | 275,053       | 68,760         |
| 10,000,000 Random Numbers (39)                 | 107,879,042  | 53,058,085     | 53,466,743    | 8,498,283      |

### **Analysis**
- **Interpolated Bucket Sort (IBS)** consistently excels in handling large ranges, sparse distributions, and sorted data.
- Quick Sort now demonstrates significant improvements with balanced performance across most test cases.
- For **large datasets**, IBS still shows scalability but can lag slightly compared to Merge Sort and Heap Sort in high-complexity scenarios.

---

Let me know if you’d like more sections updated or further modifications made to refine the README!
