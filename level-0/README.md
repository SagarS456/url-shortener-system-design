# Level-0: Naive URL Shortener

## Goal
Build the simplest possible URL shortener that:
- Accepts a long URL
- Generates a short code
- Persists data to disk
- Redirects correctly after restart

No concern is given to scalability, performance, or clean architecture.

---

## Constraints
- Single process
- No concurrency support
- File-based persistence
- Data loss acceptable on crash

These constraints are intentional.
