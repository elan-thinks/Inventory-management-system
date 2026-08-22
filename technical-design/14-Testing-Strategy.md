# 14 - Testing Strategy

**Version:** 1.0 (engine note: PostgreSQL)  

| Level | Focus |
|-------|--------|
| Unit | Services with mocked data access |
| Integration | Real **PostgreSQL**, full service + repository |
| Manual UI | Forms, navigation, validation messages |

Mandatory negative cases include insufficient stock, overlapping Active delegations, inactive product on stock movement, unauthorized adjustment, etc. (see original testing checklist).

Test database: dedicated PostgreSQL database or schema, not production `NovaTechIMS`.
