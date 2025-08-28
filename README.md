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


<details>
  <summary><strong>Tiger Logging System – Design Q&A</summary>




### 1. Intended Use Cases
Tiger belongs to Tools: a set of functionalities based on a minimal set of C# libraries, intended for any kind of .NET project.

### 2. Performance Expectations
Tiger is designed to handle up to 100,000 logs added to the queue per second and 8,000 logs written to file per second.

### 3. Thread Safety Guarantees
No specific guarantees or known limitations regarding thread safety have been documented yet.

### 4. Error Recovery Philosophy
- Normal log messages are managed by the developer and written to 'N' files.
- Exception log messages are automatically managed from the exception object and written to 'E' files.
- If an issue occurs while writing an N log, an E log is created.
- If an issue occurs while writing an E log, an X log is created.
- If writing an X log fails, an event is raised with relevant info; after that, data is considered lost.

### 5. Customization and Extensibility
Tiger is open source, so it is up to the developer to customize the tool as needed.

### 6. Security Considerations
There are no built-in mechanisms for handling sensitive data in logs (e.g., masking, encryption); this is left to the developer.

### 7. Configuration Management
There is no recommended way to configure Tiger (e.g., log retention, file paths, log levels) beyond what is hardcoded. It is open source and can be modified as needed.

### 8. Testing and Validation
No specific strategies or tools have been used to test Tiger’s reliability, especially in failure scenarios.

### 9. Known Limitations
No additional limitations or edge cases have been documented yet.

### 10. Future Plans
No specific plans for additional features, platforms, or integrations at this time.

  
</details>



### Known Limitations and Areas for Improvement
Section of ToDos actions to improve the application






<details>
  <summary><strong>Tiger Logging System – Points of Weakness
  
  ## Tiger Logging System – Points of Weakness
  </strong></summary>

1. **Recursive Calls in `SetLogFiles`**
   - The method uses recursion to handle file name conflicts and midnight rollovers. If the system clock does not advance as expected, this could theoretically lead to a stack overflow.

2. **Thread Shutdown Handling**
   - On application exit, the code waits up to 5 seconds for log writer threads to finish. If threads are blocked or the queue is large, some logs may not be flushed or could be lost.

3. **Error Handling in Log Writing**
   - If both the main and emergency log files fail, the only fallback is the `OnDataLost` event. There is no further persistence mechanism (e.g., in-memory backup, retry logic, or alternate storage).

4. **Buffer Flushing Logic**
   - The batching logic in `ProcessLogQueue` is complex and may not always flush the buffer optimally, especially if the queue is empty but the buffer still contains entries.

5. **Potential for Log Loss**
   - If the application crashes abruptly (e.g., process kill), logs in the queue or buffer may not be written to disk.

6. **Directory Creation**
   - The code relies on `path.ToDirectory().Create();` (likely an extension method). If this fails, it may not be handled gracefully.

7. **No Log File Size Management**
   - Log rotation is based on date, not file size. Large log files could accumulate if the application is long-running.

8. **No Asynchronous File I/O**
   - All file writes are synchronous, which could block the background thread if disk I/O is slow.

9. **No Configuration for Log Paths**
   - Log file paths are hardcoded to a "Logs" subdirectory under the base directory. There’s no support for custom paths or configuration.
</details>








## Tiger Logging System – Points of Weakness

