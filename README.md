## Tiger Logging Tool – Overview

**Tiger** is a static, asynchronous logging utility designed for .NET Framework applications. Its main purpose is to reliably record application events, errors, and exceptions to log files, even in challenging scenarios.



### Typical Usage

> **Important:**  
> The specialized logging methods: 
- Tiger.Debug;
- Tiger.Info; 
- Tiger.Warn; 
- Tiger.Error; 
- Tiger.Exception;

should be preferred against the generic `Tiger.Write(...)` .  


These methods automatically include: the caller's context **class name, method name, line number** in the log entry, enhancing traceability and debugging.


<details>
  <summary><strong>Key Features</strong></summary>



- **Three Log Types:**  
  - **Normal logs:** For general messages and information.
  - **Error logs:** For exceptions and managed errors.
  - **Emergency logs:** Used as a fallback if writing to the main log files fails (e.g., file locked or disk error).

- **Asynchronous Logging:**  
  - Log entries are queued and written by background threads, ensuring that logging does not block the main application flow.

- **Daily Log Rotation:**  
  - Log files are automatically rotated each day, helping to organize logs and prevent files from growing too large.

- **Automatic Cleanup:**  
  - Old log files are deleted after a configurable number of days to save disk space.

- **Robust Error Handling:**  
  - If writing to the main log fails, Tiger attempts to write to an emergency log. If all attempts fail, a special event is triggered so the application can handle data loss gracefully.

- **Event-Driven:**  
  - Tiger exposes events for log processing, emergency situations, and data loss, allowing your application to react or notify users as needed.
</details>

<details>
  <summary><strong>Design Q&A</strong></summary>

### 1. Intended Use Cases
Tiger belongs to Tools: a set of functionalities based on a minimal set of C# libraries, intended for any kind of .NET project.

### 2. Performance Expectations
Tiger is designed to handle up to 100,000 logs added to the queue per second and 8,000 logs written to file per second.

<!-- ...other Q&A items... -->

</details>

<details>
  <summary><strong>Points of Weakness</strong></summary>

1. **Recursive Calls in `SetLogFiles`**
   - The method uses recursion to handle file name conflicts and midnight rollovers. If the system clock does not advance as expected, this could theoretically lead to a stack overflow.

2. **Thread Shutdown Handling**
   - On application exit, the code waits up to 5 seconds for log writer threads to finish. If threads are blocked or the queue is large, some logs may not be flushed or could be lost.

<!-- ...other points... -->

</details>

